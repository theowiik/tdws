using Godot;

/// <summary>
/// TODO: Add doc!
/// </summary>
public class Motion : IState
{
  public void Enter()
  {
    throw new System.NotImplementedException();
  }

  public void Exit()
  {
    throw new System.NotImplementedException();
  }

  public void HandleInput(InputEvent @event)
  {
    throw new System.NotImplementedException();
  }

  public void Update()
  {
    throw new System.NotImplementedException();
  }

  /// <summary>
  /// Returns the unit vector of the input direction from the user.
  /// </summary>
  ///
  /// <returns>
  /// The unit vector of the input direction.
  /// </returns>
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
