using tdws.actors.player.states.motion.in_air;
using tdws.actors.player.states.motion.on_ground;
using tdws.utils.state;

namespace tdws.actors.player
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