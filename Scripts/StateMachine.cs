using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   Abstract state machine.
  ///   Delegates _PhysicsProcess and _Input to specific states.
  ///   Is also used for selecting which state to be in.
  /// </summary>
  public abstract class StateMachine : Node
  {
    protected IState State;

    public override void _PhysicsProcess(float delta)
    {
      if (HasState())
        State.Update(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
      if (HasState())
        State.HandleInput(@event);
    }

    /// <summary>
    ///   Enters the current state, does nothing if there is no selected state.
    /// </summary>
    public void Start()
    {
      if (HasState()) State.Enter();
    }

    /// <summary>
    ///   Checks if there is a selected state.
    /// </summary>
    /// <returns>
    ///   True if there is a selected state.
    ///   False if there is not selected state.
    /// </returns>
    private bool HasState()
    {
      return State != null;
    }
  }
}