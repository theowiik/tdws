using System;

/// <summary>
/// The PlayerStateMachine is the state machine for the player.
/// Controlls the movement and ...
/// </summary>
public class PlayerStateMachine : StateMachine
{
  private IState _walkState;
  private IState _jumpState;

  public override void _Ready()
  {
    IMovable movable = Owner as IMovable;
    _walkState = new Walk(movable);
    _jumpState = new Jump(movable);

    _state = _walkState;
  }
}
