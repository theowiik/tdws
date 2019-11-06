namespace tdws.actors.player.states.motion.on_ground
{
  /// <summary>
  ///   The Walk class is used for handling walking movement.
  /// </summary>
  public sealed class Walk : OnGround
  {
    private const int MaxWalkSpeed = 125;

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
      Velocity = inputDirection * MaxWalkSpeed;
      Movable.Move(Velocity);
    }
  }
}