using Godot;

/// <summary>
/// The OnGround class is the base class that all on ground states extends.
/// </summary>
public class OnGround : Motion
{
  // TODO: Refactor to private double and add a setter?
  public double Speed { get; set; }
  private Vector2 _velocity;

  public override void _Ready()
  {
    Speed = 0.0;
    _velocity = new Vector2();
  }

  public void HandleInput(InputEvent @event)
  {
    if (@event.IsActionPressed("jump"))
    {
      GD.Print("Jump!");
    }
  }
}
