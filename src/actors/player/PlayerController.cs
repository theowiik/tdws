using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class PlayerController : AbstractActor
{
  private Vector2 _velocity;
  private int _movementSpeed;
  private Holster _holster;
  private AnimationPlayer _animationPlayer;

  /// <summary>
  /// The node that projectiles should be attached to.
  /// </summary>
  private Node2D _projectileShooterHolder;

  public override void _Ready()
  {
    _velocity = new Vector2();
    _projectileShooterHolder = GetNode("ProjectileShooterHolder") as Node2D;
    _animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
    _holster = GetNode("Holster") as Holster;
    _movementSpeed = 300;
  }

  public override void _PhysicsProcess(float delta)
  {
    _velocity = GetMovementInputVector() * _movementSpeed;
    _velocity = MoveAndSlide(_velocity);
    PlayAnimation();
  }

  /// <summary>
  /// Plays the animation that corresponds to the players velocity.
  /// </summary>
  private void PlayAnimation() {
    if (_velocity.x > 0 )
      _animationPlayer.Play("walk_right");

    if (_velocity.x < 0 )
      _animationPlayer.Play("walk_left");

    if (_velocity.y > 0 )
      _animationPlayer.Play("walk_down");

    if (_velocity.y < 0 )
      _animationPlayer.Play("walk_up");

    if (_velocity.Length() == 0)
      _animationPlayer.Play("idle_down");
  }

  /// <summary>
  /// Returns the unit vector of the input direction.
  /// </summary>
  /// <returns>The unit vector of the input direction.</returns>
  private Vector2 GetMovementInputVector()
  {
    const int composant = 1;
    var inputVector = new Vector2();

    if (Input.IsActionPressed("up")) inputVector.y -= composant;
    if (Input.IsActionPressed("down")) inputVector.y += composant;
    if (Input.IsActionPressed("right")) inputVector.x += composant;
    if (Input.IsActionPressed("left")) inputVector.x -= composant;

    return inputVector.Normalized();
  }

  /// <summary>
  /// Removes all child nodes from the _projectileShooterHolder node.
  /// </summary>
  private void UnequipProjectileShooters()
  {
    foreach (Node child in _projectileShooterHolder.GetChildren())
      _projectileShooterHolder.RemoveChild(child);
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
