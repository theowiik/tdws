using System;
using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class Player : KinematicBody2D
{
  private Vector2 velocity;
  private int movementSpeed;
  private PackedScene projectile;

  public override void _Ready()
  {
    velocity = new Vector2();
    movementSpeed = 300;
    projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
  }

  public override void _Process(float delta)
  {
    velocity = getMovementInputVector() * movementSpeed;
    velocity = MoveAndSlide(velocity);

    if (Input.IsActionPressed("shoot"))
    {
      shoot();
    }
  }

  /// <summary>
  /// Creates a new projectile with the direction pointing towards the mouse.
  /// </summary>
  private void shoot()
  {
    Projectile proj = projectile.Instance() as Projectile;
    GetParent().AddChild(proj);
    proj.SetPosition(Transform.origin);
    proj.setDirection(playerToMouseVec());
  }

  /// <summary>
  /// Returns the direction vector from the player to the mouse. Returns a null vector if
  /// the global mouse position is null.
  /// </summary>
  ///
  /// <returns>
  /// The direction vector from the player to the mouse.
  /// </returns>
  private Vector2 playerToMouseVec()
  {
    Vector2 mousePos = GetGlobalMousePosition();
    if (mousePos == null) return new Vector2();

    return new Vector2(
      mousePos.x - Transform.origin.x,
      mousePos.y - Transform.origin.y
    ).Normalized();
  }

  /// <summary>
  /// Returns the unit vector of the input direction.
  /// </summary>
  /// <returns>The unit vector of the input direction.</returns>
  private Vector2 getMovementInputVector()
  {
    const int composant = 1;
    var inputVec = new Vector2();

    if (Input.IsActionPressed("up")) inputVec.y -= composant;
    if (Input.IsActionPressed("down")) inputVec.y += composant;
    if (Input.IsActionPressed("right")) inputVec.x += composant;
    if (Input.IsActionPressed("left")) inputVec.x -= composant;

    return inputVec.Normalized();
  }
}
