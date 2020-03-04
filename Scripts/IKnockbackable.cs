using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   Represents something that can be knocked back.
  /// </summary>
  public interface IKnockbackable
  {
    /// <summary>
    ///   Knockbacks the object with the given vector.
    /// </summary>
    /// <param name="vector"></param>
    void Knockback(Vector2 vector);
  }
}