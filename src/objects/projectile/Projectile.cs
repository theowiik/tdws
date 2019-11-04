using System;
using Godot;

namespace tdws.objects.projectile
{
  /// <summary>
  ///   The Projectile class represents a projectile.
  /// </summary>
  public abstract class Projectile : Area2D
  {
    private Vector2 _direction;
    private int _speed;

    public override void _Ready()
    {
      _speed = 5_000;
      _direction = new Vector2();
    }

    public override void _PhysicsProcess(float delta)
    {
      var transform = Transform;
      transform.origin += _direction * _speed * delta;
      SetTransform(transform);
    }

    /// <summary>
    ///   Sets the direction vector of the projectile.
    /// </summary>
    /// <param name="direction">
    ///   The direction the projectile should have.
    /// </param>
    /// <exception cref="NullReferenceException">
    ///   If the provided vector is null.
    /// </exception>
    public void SetDirection(Vector2 direction)
    {
      if (direction == null) throw new NullReferenceException("Direction can not be null.");

      _direction = direction;
    }

    /// <summary>
    ///   Sets the speed of the projectile.
    /// </summary>
    /// <param name="speed">
    ///   The new speed of the projectile.
    /// </param>
    public void SetSpeed(int speed)
    {
      _speed = speed;
    }

    public void _on_Timer_timeout()
    {
      Destroy();
    }

    /// <summary>
    ///   Destroys the projectile.
    /// </summary>
    private void Destroy()
    {
      QueueFree();
    }
  }
}