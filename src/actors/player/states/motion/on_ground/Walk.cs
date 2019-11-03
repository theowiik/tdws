using Godot;

/// <summary>
/// The Walk class is used for handling walking movement.
/// </summary>
public sealed class Walk : OnGround
{
  private const int _MaxWalkSpeed = 300;

  public Walk(IMovable movable) : base(movable) { }

  public override void Enter()
  {
    Speed = 0.0;
  }

  public override void Exit() { }

  public override void Update(float delta)
  {
    Vector2 inputDirection = GetMovementInputVector();
    _velocity = inputDirection * _MaxWalkSpeed;
    _movable.Move(_velocity);
  }
}
