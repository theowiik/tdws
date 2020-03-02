using Godot;
using tdws.Scripts.Actors;
using tdws.Scripts.ProjectileShooters;
using tdws.Scripts.Projectiles;
using tdws.Scripts.Room;
using tdws.Scripts.Services;
using Object = Godot.Object;

namespace tdws.Scripts
{
  /// <summary>
  ///   The Game class is the main class.
  /// </summary>
  public sealed class Game : Node2D
  {
    private Camera2D _camera;
    private PackedScene _coinScene;
    private Sprite _crosshair;
    private int _enemiesKilled;
    private HUD _hud;
    private AbstractActor _player;
    private RoomLoader _roomLoader;

    public Game()
    {
      _camera = new Camera2D();
      _camera.Current = true;
      _camera.SmoothingEnabled = true;

      _player = ActorFactory.CreatePlayer();
    }

    public override void _Ready()
    {
      _crosshair = GetNode<Sprite>("Crosshair");
      _hud = GetNode<HUD>("CanvasLayer/HUD");

      // Hide the cursor
      Input.SetMouseMode(Input.MouseMode.Hidden);

      // Player
      SpawnPlayer();
      _player.AddChild(_camera);

      // HUD
      _player.Connect(nameof(AbstractActor.HealthChanged), _hud, nameof(HUD.HealthChanged));
      _player.Connect(nameof(AbstractActor.ChatAdded), _hud, nameof(HUD.AddChat));

      // Coins
      _player.Connect(nameof(AbstractActor.CoinDropped), this, nameof(OnCoinDropped));
      _player.Connect(nameof(AbstractActor.CoinsChanged), _hud, nameof(HUD.OnCoinsChanged));
      _coinScene = NodeService.LoadNotNull<PackedScene>("res://Scenes/Objects/Coin.tscn");

      // Projectile signal
      _player.Connect(nameof(Player.ProjectileShooterChanged), this, nameof(OnProjectileShooterChanged));

      // Room loader
      _roomLoader = GetNode<RoomLoader>("RoomLoader");
      _roomLoader.SetPlayer(_player);
      NextRoom();

      SpawnBoss();
    }

    private void RoomLoadStarted()
    {
      // show transition sprite
    }

    private void RoomLoadFinished()
    {
      // hide transition sprite
    }

    private void SpawnBoss()
    {
      var skelly = ActorFactory.CreateSkeleton();
      var text = new Label();
      text.Text = "boss :)";
      skelly.AddChild(text);

      var healthBar = NodeService.InstanceNotNull<HealthBar>("res://Scenes/HealthBar.tscn");
      skelly.Connect(nameof(AbstractActor.HealthChanged), healthBar, nameof(HealthBar.OnHealthChanged));

      AddChild(skelly);
      AddChild(healthBar);
    }

    /// <summary>
    ///   Loads the next room.
    /// </summary>
    private void NextRoom()
    {
      RoomLoadStarted();
      _roomLoader.NextRoom();

      foreach (var door in _roomLoader.GetDoors())
        door.Connect("DoorEntered", this, nameof(OnDoorEntered));

      foreach (var monster in _roomLoader.GetEnemies())
      {
        monster.Connect(nameof(AbstractActor.CoinDropped), this, nameof(OnCoinDropped));
        monster.Connect(nameof(AbstractActor.Died), this, nameof(OnDied));
      }

      RoomLoadFinished();
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
          var randomVector = new Vector2((float)GD.RandRange(-1, 1), (float)GD.RandRange(-1, 1)).Normalized() * 100;
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

      var alreadyConnected = projectileShooter.IsConnected(nameof(AbstractProjectileShooter.ProjectileAdded), this, nameof(AddProjectile));

      if (alreadyConnected) return;

      projectileShooter.Connect(
        nameof(AbstractProjectileShooter.ProjectileAdded),
        this,
        nameof(AddProjectile)
      );
    }

    /// <summary>
    ///   Spawns the player.
    /// </summary>
    private void SpawnPlayer()
    {
      AddChild(_player);
    }

    private void AllEnemiesKilled()
    {
      foreach (var door in _roomLoader.GetDoors())
        door.Enterable();
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
      _crosshair.GlobalPosition = GetGlobalMousePosition();
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

      CallDeferred("add_child", node);
    }

    /// <summary>
    ///   Instances a packed scene at a specific position.
    ///   The packed scenes root node must extend Node2D.
    /// </summary>
    /// <param name="packedScene">The scene to instance.</param>
    /// <param name="position">The position to instance at.</param>
    private void InstancePackedScene(PackedScene packedScene, Vector2 position)
    {
      var instance = packedScene.Instance();

      if (instance is Node2D node2D)
      {
        AddChildNode(node2D);
        node2D.GlobalPosition = position;
      }
    }

    private void AddProjectile(AbstractProjectile projectile)
    {
      AddChildNode(projectile);
      projectile.Connect(nameof(AbstractProjectile.ProjectileHit), this, nameof(InstancePackedScene));
    }

    /// <summary>
    ///   Gets called when a enemy dies.
    /// </summary>
    private void OnDied()
    {
      _enemiesKilled++;

      if (_enemiesKilled >= 2)
        AllEnemiesKilled();
    }

    /// <summary>
    ///   Toggles the fullscreen.
    /// </summary>
    private static void ToggleFullscreen()
    {
      OS.WindowFullscreen = !OS.WindowFullscreen;
    }

    /// <summary>
    ///   Spawns a random enemy at a random location
    /// </summary>
    private void SpawnEnemy()
    {
      var skeleton = ActorFactory.CreateSkeleton();
      CallDeferred("add_child", skeleton);
      skeleton.GlobalPosition = new Vector2(20, 20);
      skeleton.Connect(nameof(AbstractActor.CoinDropped), this, nameof(OnCoinDropped));
      skeleton.Connect(nameof(AbstractActor.Died), this, nameof(OnDied));
    }
  }
}
