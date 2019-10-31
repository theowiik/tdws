using Godot;
using System;

/// <summary>
/// The Game class is the main class.
/// </summary>
public class Game : Node2D
{
  public override void _Input(InputEvent @event)
  {
    // Close if ui_cancel is pressed.
    if (@event.IsActionPressed("ui_cancel")) GetTree().Quit();
  }
}
