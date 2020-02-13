using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   The IState interface is a interface for all states.
  /// </summary>
  public interface IState
  {
    /// <summary>
    ///   Initializes the state.
    /// </summary>
    void Enter();

    /// <summary>
    ///   Disables the state.
    /// </summary>
    void Exit();

    /// <summary>
    ///   The entry point for when new events occur.
    /// </summary>
    void HandleInput(InputEvent @event);

    /// <summary>
    ///   Updates the state.
    /// </summary>
    void Update(float delta);
  }
}