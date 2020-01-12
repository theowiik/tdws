using System;
using System.ComponentModel;
using Godot;
using tdws.actors.abstract_actor;
using tdws.actors.monsters;
using tdws.actors.player;
using tdws.core;
using tdws.engine.world_generator;
using tdws.interfacee;
using tdws.objects.coin;
using tdws.objects.door;
using tdws.projectile_shooters.abstract_projectile_shooter;
using Object = Godot.Object;

namespace tdws
{
  /// <summary>
  ///   The Game class is the main class.
  /// </summary>
  public class Game : Node2D
  {
    private Camera _camera;
    private PackedScene _coinScene;
    private Sprite _crosshair;
    private HUD _hud;
    private RoomLoader _roomLoader;
    private AbstractActor _player;
    private Vector2 _spawnPoint;

    /// <summary>
    ///   Loads the crosshair scene and adds it as a child.
    /// </summary>
    private void InitCrosshair()
    {
      var crosshairScene = GD.Load("res://src/Crosshair.tscn") as PackedScene;
      _crosshair = crosshairScene.Instance() as Sprite;
      AddChild(_crosshair);
    }

    /// <summary>
    ///   Generates the world.
    /// </summary>
    private void GenerateWorld()
    {
      var worldGenerator = new WorldGenerator();
      var world = worldGenerator.GenerateWorld();
      worldGenerator.WorldToScene(world, this);
      _spawnPoint = worldGenerator.SpawnPoint;
    }

    public override void _Ready()
    {
      GenerateWorld();
      InitCrosshair();
      _hud = GetNode("CanvasLayer/HUD") as HUD;

      // Camera
      _camera = GetNode("Camera") as Camera;

      // Hide the cursor
      Input.SetMouseMode(Input.MouseMode.Hidden);

      // ...
      SpawnEnemy();
      SpawnPlayer();
      AddCameraToPlayer();

      // HUD
      _player.Connect(nameof(AbstractActor.HealthChanged), _hud, nameof(HUD.HealthChanged));
      _player.Connect(nameof(AbstractActor.ChatAdded), _hud, nameof(HUD.AddChat));

      // Coins
      _player.Connect(nameof(AbstractActor.CoinDropped), this, nameof(OnCoinDropped));
      _player.Connect(nameof(AbstractActor.CoinsChanged), _hud, nameof(HUD.OnCoinsChanged));
      _coinScene = GD.Load("res://src/objects/coin/Coin.tscn") as PackedScene;

      // Projectile signal
      _player.Connect(nameof(PlayerController.ProjectileShooterChanged), this, nameof(OnProjectileShooterChanged));

      _roomLoader = GetNode("RoomLoader") as RoomLoader;
    }

    public void NextRoom()
    {
      // ...

      foreach (var door in _roomLoader.GetDoors())
      {
        door.Connect("DoorEntered", this, nameof(OnDoorEntered));
      }
    }

    private void OnDoorEntered()
    {
      NextRoom();
    }

    /// <summary>
    ///   Gets called when coins are dropped.
    ///   Adds coins to the scene and shoots them out at a random direction.
    /// </summary>
    /// <param name="amount">
    ///   The amount of coins to drop.
    /// </param>
    /// <param name="position">
    ///   The coordinate to drop the coins at.
    /// </param>
    private void OnCoinDropped(int amount, Vector2 position)
    {
      for (var i = 0; i < amount; i++)
        if (_coinScene.Instance() is Coin coin)
        {
          CallDeferred("add_child", coin); // Come on Godot.. >:(
          coin.GlobalPosition = position;
          var randomVector = new Vector2((float) GD.RandRange(-1, 1), (float) GD.RandRange(-1, 1)).Normalized() * 100;
          coin.ApplyImpulse(Vector2.Zero, randomVector);
        }
    }

    /// <summary>
    ///   Gets called when the players projectile shooter has been changed.
    ///   Connects the...
    ///   Does nothing if the projectile shooter is null.
    /// </summary>
    /// <param name="projectileShooter">
    ///   The new projectile shooter.
    /// </param>
    private void OnProjectileShooterChanged(Object projectileShooter)
    {
      if (projectileShooter == null) return;

      var alreadyConnected = projectileShooter.IsConnected(nameof(AbstractProjectileShooter.ProjectileAdded), this,
        nameof(AddChildNode));

      if (alreadyConnected) return;

      projectileShooter.Connect(
        nameof(AbstractProjectileShooter.ProjectileAdded),
        this,
        nameof(AddChildNode)
      );
    }

    /// <summary>
    ///   Spawns the player. Creates one if it does not exist.
    /// </summary>
    private void SpawnPlayer()
    {
      if (_player == null) _player = CreatePlayer();

      _player.GlobalPosition = _spawnPoint;
      AddChild(_player);
    }

    /// <summary>
    ///   Add the camera to the player node. Player and camera must exist.
    /// </summary>
    /// <exception cref="System.NullReferenceException">
    ///   If player or camera is null.
    /// </exception>
    private void AddCameraToPlayer()
    {
      if (_camera == null || _player == null)
        throw new NullReferenceException("Camera and/or player is null");

      _camera.GetParent().RemoveChild(_camera); // fix..
      _player.AddChild(_camera);
      _camera.SetPosition(Vector2.Zero);
    }

    /// <summary>
    ///   Creates a new player.
    /// </summary>
    /// <returns>
    ///   The player.
    /// </returns>
    /// <exception cref="System.Exception">
    ///   If the player scene could not be found.
    /// </exception>
    /// <exception cref="System.Exception">
    ///   If the player scene is a not a actor.
    /// </exception>
    private AbstractActor CreatePlayer()
    {
      var player = GD.Load("res://src/actors/player/Player.tscn") as PackedScene;
      if (player == null) throw new Exception("Player scene could not be loaded.");

      var p = player.Instance() as AbstractActor;
      if (p == null) throw new Exception("Player scene is not a actor.");

      return p;
    }

    public override void _Process(float delta)
    {
      CrosshairLoop();
    }

    /// <summary>
    ///   Sets the crosshair's position to the mouse coordinate.
    /// </summary>
    private void CrosshairLoop()
    {
      _crosshair.SetGlobalPosition(GetGlobalMousePosition());
    }

    public override void _Input(InputEvent @event)
    {
      if (@event.IsActionPressed("ui_cancel"))
        GetTree().Quit();

      if (@event.IsActionPressed("toggle_fullscreen"))
        ToggleFullscreen();
    }

    /// <summary>
    ///   Adds a child to the game scene. Does nothing if the node is null.
    /// </summary>
    /// <param name="node">
    ///   The node to add.
    /// </param>
    private void AddChildNode(Node node)
    {
      if (node == null) return;

      AddChild(node);
    }

    /// <summary>
    ///   Gets called when a enemy dies.
    /// </summary>
    private void OnDied()
    {
      SpawnEnemy();
    }

    /// <summary>
    ///   Toggles the fullscreen.
    /// </summary>
    private static void ToggleFullscreen()
    {
      OS.WindowFullscreen = !OS.IsWindowFullscreen();
    }

    /// <summary>
    ///   Spawns a random enemy at a random location
    /// </summary>
    private void SpawnEnemy()
    {
      var skeleton = MonsterFactory.CreateSkeleton();
      CallDeferred("add_child", skeleton);
      skeleton.SetGlobalPosition(new Vector2(20, 20));
      skeleton.Connect(nameof(AbstractActor.CoinDropped), this, nameof(OnCoinDropped));
      skeleton.Connect(nameof(AbstractActor.Died), this, nameof(OnDied));
    }
  }
}