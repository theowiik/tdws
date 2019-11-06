using System;
using Godot;

namespace tdws.utils
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
      
      if (!zeroX && zeroY) // It is only a horizontal component
      {
        if (velocity.x > 0) return Directions.Left;
        if (velocity.x < 0) return Directions.Right;
      }
      else if (!zeroY && zeroX) // It is only a vertical component
      {
        if (velocity.y > 0) return Directions.Up;
        if (velocity.y < 0) return Directions.Down;
      }

      return null;
    }
  }
}