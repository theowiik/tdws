using Godot;

/// <summary>
/// The Walk class is used for handling walking movement.
/// </summary>
public sealed class Walk : OnGround
{
  private const int _MaxWalkSpeed = 300;

  public override void Enter()
  {
    Speed = 0.0;
  }

  public override void Exit()
  {
    // do something
  }

  public override void Update()
  {
    Vector2 inputDirection = GetMovementInputVector();
    _velocity = inputDirection * _MaxWalkSpeed;
    ((PlayerController)Owner).Move(_velocity);
  }
}
