using System;
using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public sealed class Player : KinematicBody2D
{
  private Vector2 _velocity;
  private int _movementSpeed;
  private InventoryManager _inventoryManager;

  public override void _Ready()
  {
    _velocity = new Vector2();
    _movementSpeed = 300;
    _inventoryManager = new InventoryManager();
  }

  public override void _Process(float delta)
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
}
