namespace tdws.Scripts
{
  /// <summary>
  ///   The PlayerStateMachine is the state machine for the player.
  ///   Controls the movement and ...
  /// </summary>
  public class PlayerStateMachine : StateMachine
  {
    private IState _jumpState;
    private IState _walkState;

    public override void _Ready()
    {
      var movable = Owner as IMovable;
      _walkState = new Walk(movable);
      _jumpState = new Jump(movable);

      State = _walkState;
    }
  }
}