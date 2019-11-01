using System.Collections.Generic;
using Godot;

/// <summary>
/// Abstract state machine.
/// Delegates _PhysicsProcess and _Input to specific states.
/// Is also used for selecting which state to be in.
/// </summary>
public abstract class StateMachine : Node
{
  private IState _state;
  private List<IState> _states;

  public override void _Ready()
  {
    _states.Add(GetChild(0) as IState);
    _state = _states[0];
  }

  public override void _PhysicsProcess(float delta)
  {
    if (HasState())
      _state.Update();
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    if (HasState())
      _state.HandleInput(@event);
  }

  /// <summary>
  /// Enters the current state, does nothing if there is no selected state.
  /// </summary>
  public void Start()
  {
    if (!HasState()) return;

    _state.Enter();
  }

  /// <summary>
  /// Checks if there is a selected state.
  /// </summary>
  ///
  /// <returns>
  /// True if there is a selected state.
  /// False if there is not selected state.
  /// </returns>
  private bool HasState()
  {
    return _state != null;
  }
}
