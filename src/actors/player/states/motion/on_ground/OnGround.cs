using Godot;

namespace tdws.actors.player.states.motion.on_ground
{
  /// <summary>
  ///   The OnGround class is the base class that all on ground states extends.
  /// </summary>
  public abstract class OnGround : Motion
  {
    protected Vector2 Velocity;

    protected OnGround(IMovable movable) : base(movable)
    {
      Speed = 0.0;
      Velocity = new Vector2();
    }

    public double Speed { get; set; }

    public override void HandleInput(InputEvent @event)
    {
      if (@event.IsActionPressed("jump"))
      {
        // Jump !
      }
    }
  }
}