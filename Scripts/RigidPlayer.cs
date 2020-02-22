using Godot;
using tdws.Scripts.Services;

namespace tdws.Scripts
{
  public class RigidPlayer : RigidBody2D
  {
    private const int MaxWalkSpeed = 166;
    private Vector2 _velocity;

    public RigidPlayer()
    {
      _velocity = Vector2.Zero;
    }

    public override void _Process(float delta)
    {
      var desiredVector = GetMovementInputVector();
      LinearVelocity = desiredVector * MaxWalkSpeed;

      if (Input.IsActionJustPressed("debug"))
      {
        var instance = NodeService.InstanceNotNull<Explosion>("res://Scenes/Explosion.tscn");
        GetParent().AddChild(instance);
        instance.GlobalPosition = GlobalPosition;
      }
    }

    private static Vector2 GetMovementInputVector()
    {
      const int component = 1;
      var inputVector = new Vector2();

      if (Input.IsActionPressed("up")) inputVector.y -= component;
      if (Input.IsActionPressed("down")) inputVector.y += component;
      if (Input.IsActionPressed("right")) inputVector.x += component;
      if (Input.IsActionPressed("left")) inputVector.x -= component;

      return inputVector.Normalized();
    }
  }
}