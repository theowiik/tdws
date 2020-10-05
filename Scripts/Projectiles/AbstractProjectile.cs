using Godot;
using tdws.Scripts.Actors;

namespace tdws.Scripts.Projectiles
{
  /// <summary>
  ///   The Projectile class represents a abstract projectile.
  /// </summary>
  public abstract class AbstractProjectile : Area2D, IProjectile
  {
    [Signal]
    public delegate void ProjectileHit(PackedScene packedScene);

    protected int Speed;

    public AbstractProjectile()
    {
      Direction = new Vector2();
    }

    public Vector2       Direction   { get; set; }
    public AbstractActor ActorSource { get; set; }

    /// <summary>
    ///   The scene to instance when the projectile hits a target.
    /// </summary>
    protected PackedScene HitScene { get; set; }

    public int GetDamage()
    {
      return 10;
    }

    public AbstractActor GetActorSource()
    {
      return ActorSource;
    }

    public bool HasActorSource()
    {
      return ActorSource != null;
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
      Speed = 400;
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
      GlobalRotation = Direction.Angle();
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
      Transform        =  transform;
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
        damageable.TakeDamage(this);

      if (HitScene != null)
        EmitSignal(nameof(ProjectileHit), HitScene, GlobalPosition);

      if (body is RigidBody2D pushable)
        pushable.ApplyImpulse(Vector2.Zero, Direction * 50);

      Destroy();
    }
  }
}