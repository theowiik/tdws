using Godot;

namespace tdws.actors.player
{
  /// <summary>
  ///   The IMovable interface represents objects that can move.
  /// </summary>
  public interface IMovable
  {
    /// <summary>
    ///   Moves the movable object with the given velocity.
    /// </summary>
    /// <param name="velocity">
    ///   The velocity to move the movable object with.
    /// </param>
    void Move(Vector2 velocity);
  }
}
