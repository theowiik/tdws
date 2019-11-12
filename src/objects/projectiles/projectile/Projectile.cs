using System;
using Godot;
using tdws.actors;

namespace tdws.objects.projectiles.projectile
{
  /// <summary>
  ///   The Projectile class represents a abstract projectile.
  /// </summary>
  public abstract class Projectile : Area2D, IProjectile
  {
    protected Vector2 Direction;
    protected int Speed;

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
      InitStandardValues();
      OverrideProperties();
    }

    /// <summary>
    ///   Override specific properties of a projectile shooter, such as the mag size and damage.
    /// </summary>
    protected abstract void OverrideProperties();

    /// <summary>
    ///   Sets all instance variables to standard values.
    /// </summary>
    private void InitStandardValues()
    {
      Speed = 600;
      Direction = new Vector2();
    }

    public override void _PhysicsProcess(float delta)
    {
      Move(delta);
      RotationLoop();
    }

    /// <summary>
    ///   Rotates the projectile to look the way it is traveling.
    /// </summary>
    private void RotationLoop()
    {
      SetGlobalRotation(Direction.Angle());
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

      Direction = direction.Normalized();
    }

    /// <summary>
    ///   Sets the speed of the projectile.
    /// </summary>
    /// <param name="speed">
    ///   The new speed of the projectile.
    /// </param>
    public void SetSpeed(int speed)
    {
      Speed = speed;
    }

    /// <summary>
    ///   Gets called when the projectile has existed its life span.
    /// </summary>
    public void OnTimerTimeout()
    {
      Destroy();
    }

    /// <summary>
    ///   Moves the projectile.
    /// </summary>
    /// <param name="delta">
    ///   The time difference since the last process call.
    /// </param>
    protected virtual void Move(float delta)
    {
      var transform = Transform;
      transform.origin += Direction * Speed * delta;
      SetTransform(transform);
    }

    /// <summary>
    ///   Gets called when a projectile hits a body.
    /// </summary>
    /// <param name="body">
    ///   The body that got hit.
    /// </param>
    private void OnProjectileBodyEntered(object body)
    {
      if (body is IDamageable damageable)
      {
        damageable.TakeDamage(this);
        Destroy();
      }
    }
  }
}