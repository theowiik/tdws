using System;
using Godot;
using tdws.actors.abstract_actor;
using tdws.actors.monsters;

namespace tdws
{
  /// <summary>
  ///   The Game class is the main class.
  /// </summary>
  public class Game : Node2D
  {
    private Sprite _crosshair;
    private AbstractActor _player;

    public override void _Ready()
    {
      // Init crosshair
      var crosshairScene = GD.Load("res://src/Crosshair.tscn") as PackedScene;
      _crosshair = crosshairScene.Instance() as Sprite;
      AddChild(_crosshair);

      // Hide the cursor
      Input.SetMouseMode(Input.MouseMode.Hidden);

      // Test
      SpawnEnemy();

      // Change scene
//      GetTree().ChangeScene("res://src/levels/Arena.tscn");

      SpawnPlayer();
    }

    /// <summary>
    ///   Spawns the player. Creates one if it does not exist.
    /// </summary>
    private void SpawnPlayer()
    {
      if (_player == null) _player = CreatePlayer();

      _player.GlobalPosition = new Vector2(30, 30);
      AddChild(_player);
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