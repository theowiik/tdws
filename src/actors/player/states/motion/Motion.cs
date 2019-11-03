using System;
using Godot;

/// <summary>
/// The Motion class is a abstract state that represents motion.
/// </summary>
public abstract class Motion : IState
{
  protected IMovable _movable;

  public abstract void Enter();
  public abstract void Exit();
  public abstract void HandleInput(InputEvent @event);
  public abstract void Update(float delta);

  /// <summary>
  /// Creates and returns a new motion state.
  /// </summary>
  ///
  /// <param name="movable">
  /// The object to move.
  /// </param>
  ///
  /// <exception cref="ArgumentNullException">
  /// If the provided movable is null.
  /// </exception>
  protected Motion(IMovable movable)
  {
    if (movable == null)
      throw new ArgumentNullException("movable cannot be null");

    _movable = movable;
  }

  /// <summary>
  /// Returns the unit vector of the input direction from the user.
  /// </summary>
  ///
  /// <returns>
  /// The unit vector of the input direction.
  /// </returns>
  protected Vector2 GetMovementInputVector()
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
