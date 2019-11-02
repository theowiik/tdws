using Godot;

/// <summary>
/// The Motion class is a abstract state that represents motion.
/// </summary>
public abstract class Motion : Node, IState
{
  public abstract void Enter();
  public abstract void Exit();
  public abstract void HandleInput(InputEvent @event);
  public abstract void Update();

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
