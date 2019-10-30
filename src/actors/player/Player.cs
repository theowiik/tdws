using System;
using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class Player : KinematicBody2D
{
  private Vector2 velocity;
  private int movementSpeed;

  public override void _Ready()
  {
    velocity = new Vector2();
    movementSpeed = 300;
  }

  public override void _Process(float delta)
  {
    velocity = getMovementInputVector() * movementSpeed;
    velocity = MoveAndSlide(velocity);
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
