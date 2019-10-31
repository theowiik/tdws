using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class Player : AbstractActor
{
  private Vector2 _velocity;
  private int _movementSpeed;
  private Holster _holster;

  /// <summary>
  /// The node that projectiles should be attached to.
  /// </summary>
  private Node2D _projectileShooterHolder;

  public override void _Ready()
  {
    _velocity = new Vector2();
    _projectileShooterHolder = GetNode("ProjectileShooterHolder") as Node2D;
    _holster = GetNode("Holster") as Holster;
    _movementSpeed = 300;
  }

  public override void _PhysicsProcess(float delta)
  {
    _velocity = GetMovementInputVector() * _movementSpeed;
    _velocity = MoveAndSlide(_velocity);
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
