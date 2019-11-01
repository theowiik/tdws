using Godot;

/// <summary>
/// The Game class is the main class.
/// </summary>
public class Game : Node2D
{
  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("ui_cancel"))
      GetTree().Quit();
  }
}
