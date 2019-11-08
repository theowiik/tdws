using System;
using Godot;
using tdws.actors;

namespace tdws.objects.projectiles.projectile
{
  /// <summary>
  ///   The Projectile class represents a projectile.
  /// </summary>
  public abstract class Projectile : Area2D, IProjectile
  {
    private Vector2 _direction;
    private int _speed;

    public int GetDamage()
    {
      return 10;
    }

    // TODO: It is not implemented! Rethink some things.
    public void Move(Vector2 velocity)
    {
      throw new NotImplementedException();
    }

    public void Destroy()
    {
      QueueFree();
    }

    public override void _Ready()
    {
      _speed = 600;
      _direction = new Vector2();
    }

    public override void _PhysicsProcess(float delta)
    {
      var transform = Transform;
      transform.origin += _direction * _speed * delta;
      SetTransform(transform);
      RotationLoop();
    }

    /// <summary>
    ///   Rotates the projectile to look the way it is traveling.
    /// </summary>
    private void RotationLoop()
    {
      SetGlobalRotation(_direction.Angle());
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

      _direction = direction.Normalized();
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

    private void _on_Projectile_body_entered(object body)
    {
      if (body is IDamageable damageable)
      {
        damageable.TakeDamage(this);
        Destroy();
      }
    }
  }
}