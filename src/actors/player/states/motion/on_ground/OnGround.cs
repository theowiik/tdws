using Godot;

/// <summary>
/// The OnGround class is the base class that all on ground states extends.
/// </summary>
public abstract class OnGround : Motion
{
  public double Speed { get; set; }
  protected Vector2 _velocity;

  protected OnGround(IMovable movable) : base(movable)
  {
    Speed = 0.0;
    _velocity = new Vector2();
  }

  public override void HandleInput(InputEvent @event)
  {
    if (@event.IsActionPressed("jump"))
    {
      GD.Print("Jump!");
    }
  }
}
