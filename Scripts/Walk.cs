using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   The Walk class is used for handling walking movement.
  /// </summary>
  public sealed class Walk : OnGround
  {
    private const int MaxWalkSpeed = 125;
    private const int MaxSprintSpeed = 175;

    public Walk(IMovable movable) : base(movable)
    {
    }

    public override void Enter()
    {
      Speed = 0.0;
    }

    public override void Exit()
    {
    }

    public override void Update(float delta)
    {
      var inputDirection = GetMovementInputVector();
      var speed = Input.IsActionPressed("sprint") ? MaxSprintSpeed : MaxWalkSpeed;
      Velocity = inputDirection * speed;
      Movable.Move(Velocity);
    }
  }
}