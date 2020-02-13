using System;
using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   Direction related services.
  /// </summary>
  public class DirectionService
  {
    /// <summary>
    ///   Converts a vector to a direction enum.
    /// </summary>
    /// <param name="velocity">
    ///   The velocity to convert.
    /// </param>
    /// <returns>
    ///   A direction enum.
    /// </returns>
    /// <exception cref="NullReferenceException">
    ///   If the provided vector is null.
    /// </exception>
    public static Directions VelocityToDirection(Vector2 velocity)
    {
      if (velocity == null) throw new NullReferenceException("velocity cannot be null");
      var zeroX = velocity.x == 0;
      var zeroY = velocity.y == 0;

      if (zeroX && zeroY) return Directions.None;

      // Horizontal animations are prioritized over vertical ones.
      if (velocity.x > 0) return Directions.Right;
      if (velocity.x < 0) return Directions.Left;

      // Only use vertical animations if a horizontal button is NOT pressed.
      if (velocity.y > 0 && zeroX) return Directions.Down;
      if (velocity.y < 0 && zeroX) return Directions.Up;

      return Directions.None;
    }
  }
}