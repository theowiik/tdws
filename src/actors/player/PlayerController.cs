using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class PlayerController : AbstractActor, IMovable
{
  private Holster _holster;
  private AnimationPlayer _animationPlayer;
  private StateMachine _stateMachine;

  /// <summary>
  /// The node that projectiles should be attached to.
  /// </summary>
  private Node2D _projectileShooterHolder;

  public override void _Ready()
  {
    _projectileShooterHolder = GetNode("ProjectileShooterHolder") as Node2D;
    _animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
    _holster = GetNode("Holster") as Holster;
    _stateMachine = GetNode("PlayerStateMachine") as StateMachine;
    _stateMachine.Start();
  }

  public override void _PhysicsProcess(float delta) { }

  void IMovable.Move(Vector2 velocity)
  {
    MoveAndSlide(velocity);
  }
}
