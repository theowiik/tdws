using System;
using Godot;
using tdws.Scripts.Services;

namespace tdws.Scripts.Actors
{
  /// <summary>
  ///   The base all enemies inherit from.
  /// </summary>
  public abstract class AbstractEnemy : AbstractActor, IDamageSource
  {
    /// <summary>
    ///   The amount of time the enemy will chase their target (in seconds) while they are outside their range.
    /// </summary>
    private const int ChaseTime = 3;

    private Timer _chaseTimer;

    /// <summary>
    ///   The target destination.
    /// </summary>
    private AbstractActor _chasing;

    /// <summary>
    ///   A boolean to show if the enemy is currently being knocked back by a explosion, and thus can not move.
    /// </summary>
    private bool _isBeingKnockedback;

    public AbstractEnemy()
    {
      _chasing            = null;
      _isBeingKnockedback = false;
    }

    public int GetDamage()
    {
      return 10;
    }

    public AbstractActor GetActorSource()
    {
      return this;
    }

    public bool HasActorSource()
    {
      return true;
    }

    protected override void GetNodes()
    {
      _chaseTimer = GetNode<Timer>("ChaseTimer");
    }

    /// <summary>
    ///   Checks if the _chaseTimer is running.
    /// </summary>
    /// <returns>
    ///   True if it is running. False otherwise.
    /// </returns>
    private bool IsChasing()
    {
      return !_chaseTimer.IsStopped();
    }

    protected override void HandleDamage(IDamageSource damageSource)
    {
      if (damageSource.HasActorSource()) _chasing = damageSource.GetActorSource();

      _chaseTimer.Start(ChaseTime);
    }

    /// <summary>
    ///   Gets called when a body exits the detection area.
    ///   Starts the chase timer if the body that left the detection area is
    ///   the same as the one being chased.
    /// </summary>
    /// <param name="body">
    ///   The body that entered the area.
    /// </param>
    private void OnDetectionAreaExited(object body)
    {
      if (body == _chasing)
        _chaseTimer.Start(ChaseTime);
    }

    /// <summary>
    ///   Gets called when the chase timer runs out.
    /// </summary>
    public void OnChaseTimerTimeout()
    {
      _chasing       = null;
      LinearVelocity = Vector2.Zero;
    }

    /// <summary>
    ///   Damages the thing that entered the enemies damage area.
    /// </summary>
    /// <param name="body">
    ///   The body that entered the area.
    /// </param>
    public void OnDamageAreaEntered(object body)
    {
      if (body is IDamageable damageable) damageable.TakeDamage(this);
    }

    /// <summary>
    ///   Gets called when a body exits the detection area.
    /// </summary>
    /// <param name="body">
    ///   The body that exited the area.
    /// </param>
    private void OnDetectionAreaEntered(object body)
    {
      if (body is AbstractActor body2D)
      {
        _chaseTimer.Stop();
        _chasing = body2D;
      }
    }

    /// <summary>
    ///   Checks if a enemy is chasing the player.
    /// </summary>
    /// <returns>True if it is chasing false otherwise.</returns>
    private bool isChasing()
    {
      return _chasing != null;
    }

    public override void Knockback(Vector2 vector)
    {
      ApplyCentralImpulse(vector);
      _isBeingKnockedback = true;
    }

    public override void _PhysicsProcess(float delta)
    {
      var d = 2f;

      if (_isBeingKnockedback)
        if (LinearVelocity.Length() <= d)
          _isBeingKnockedback = false;
        else
          return;

      if (!isChasing()) return;

      try
      {
        var toTarget = GlobalPosition.DirectionTo(_chasing.GlobalPosition);
        LinearVelocity = toTarget.Normalized() * 100;
        PlayAnimation(DirectionService.VelocityToDirection(LinearVelocity));
      }
      catch (ObjectDisposedException e)
      {
        _chasing = null;
      }
    }
  }
}