using System.Linq;

public class PlayerStateMachine : StateMachine
{
  public override void _Ready()
  {
    IState walkState = GetNode("Walk") as IState;
    _states.Add(walkState);
    _state = _states.First();
  }
}
