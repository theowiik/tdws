using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class PlayerController : AbstractActor
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

  /// <summary>
  /// Removes all child nodes from the _projectileShooterHolder node.
  /// </summary>
  private void UnequipProjectileShooters()
  {
    foreach (Node child in _projectileShooterHolder.GetChildren())
      _projectileShooterHolder.RemoveChild(child);
  }

  /// <summary>
  /// Moves the player with the given velocity.
  /// </summary>
  ///
  /// <param name="velocity">
  /// The velocity to be added to the player.
  /// </param>
  public void Move(Vector2 velocity)
  {
    MoveAndSlide(velocity);
  }

  /// <summary>
  /// Unequips all projectile shooters and equips the projectile shooter that is currently held.
  /// </summary>
  private void EquipHoldingProjectileShooter()
  {
    UnequipProjectileShooters();
    var holding = _holster.GetHolding() as Node;
    _projectileShooterHolder.AddChild(holding);
  }
}
