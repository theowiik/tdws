using Godot;
using System;

/// <summary>
/// The Projectile class represents a projectile.
/// </summary>
public abstract class Projectile : Area2D
{
  private int speed;
  private Vector2 direction;

  public override void _Ready()
  {
    speed = 5_000;
    direction = new Vector2();
  }

  public override void _PhysicsProcess(float delta)
  {
    Transform2D transform = this.Transform;
    transform.origin += direction * speed * delta;
    SetTransform(transform);
  }

  /// <summary>
  /// Sets the direction vector of the projectile.
  /// </summary>
  ///
  /// <param name="direction">
  /// The direction the projectile should have.
  /// </param>
  ///
  /// <exception cref="NullReferenceException">
  /// If the provided vector is null.
  /// </exception>
  public void SetDirection(Vector2 direction)
  {
    if (direction == null)
    {
      throw new NullReferenceException("Direction can not be null.");
    }

    this.direction = direction;
  }

  /// <summary>
  /// Sets the speed of the projectile.
  /// </summary>
  ///
  /// <param name="speed">
  /// The new speed of the projecile.
  /// </param>
  public void SetSpeed(int speed)
  {
    this.speed = speed;
  }

  public void _on_Timer_timeout()
  {
    Destroy();
  }

  /// <summary>
  /// Destroys the projectile.
  /// </summary>
  private void Destroy()
  {
    QueueFree();
  }
}
