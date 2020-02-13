using System;
using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   The Motion class is a abstract state that represents motion.
  /// </summary>
  public abstract class Motion : IState
  {
    protected readonly IMovable Movable;

    /// <summary>
    ///   Creates and returns a new motion state.
    /// </summary>
    /// <param name="movable">
    ///   The object to move.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   If the provided movable is null.
    /// </exception>
    protected Motion(IMovable movable)
    {
      Movable = movable ?? throw new ArgumentNullException(nameof(movable), "cannot be null.");
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void HandleInput(InputEvent @event);
    public abstract void Update(float delta);

    /// <summary>
    ///   Returns the unit vector of the input direction from the user.
    /// </summary>
    /// <returns>
    ///   The unit vector of the input direction.
    /// </returns>
    protected static Vector2 GetMovementInputVector()
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
}