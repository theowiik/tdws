using System;
using Godot;

/// <summary>
/// The Player charachter.
/// </summary>
public class Player : KinematicBody2D
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
    var inputVec = new Vector2();
    bool up = Input.IsActionPressed("up");
    bool down = Input.IsActionPressed("down");
    bool right = Input.IsActionPressed("right");
    bool left = Input.IsActionPressed("left");

    if (up ^ down)
    {
      inputVec.y = up ? -1 : 1;
    }

    if (left ^ right)
    {
      inputVec.x = left ? -1 : 1;
    }

    return inputVec.Normalized();
  }
}
