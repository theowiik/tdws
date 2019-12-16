using System;
using Godot;
using tdws.actors.abstract_actor;
using tdws.actors.monsters;
using tdws.actors.player;
using tdws.engine.world_generator;
using tdws.interfacee;
using tdws.objects.projectiles.abstract_projectile;
using tdws.projectile_shooters;
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
    private Sprite _crosshair;
    private HUD _hud;
    private AbstractActor _player;
    private Vector2 _spawnPoint;

    public override void _Ready()
    {
      // Dungeon
      var worldGenerator = new WorldGenerator();
      var world = worldGenerator.GenerateWorld();
      worldGenerator.WorldToScene(world, this);
      _spawnPoint = worldGenerator.SpawnPoint;

      // Camera
      _camera = GetNode("Camera") as Camera;

      // Init crosshair
      var crosshairScene = GD.Load("res://src/Crosshair.tscn") as PackedScene;
      _crosshair = crosshairScene.Instance() as Sprite;
      AddChild(_crosshair);

      // Hide the cursor
      Input.SetMouseMode(Input.MouseMode.Hidden);

      // ...
      SpawnEnemy();
      SpawnPlayer();
      AddCameraToPlayer();

      // HUD
      _hud = GetNode("HUD") as HUD;
      _player.Connect(nameof(AbstractActor.HealthChanged), _hud, nameof(HUD.HealthChanged));
      _player.Connect(nameof(AbstractActor.ChatAdded), _hud, nameof(HUD.AddChat));

      // Projectile signal
      _player.Connect(nameof(PlayerController.ProjectileShooterChanged), this, nameof(OnProjectileShooterChanged));
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
    /// <exception cref="NullReferenceException">
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
    /// <exception cref="Exception">
    ///   If the player scene could not be found.
    /// </exception>
    /// <exception cref="Exception">
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
      AddChild(skeleton);
      skeleton.SetGlobalPosition(new Vector2(10, 10));
    }
  }
}