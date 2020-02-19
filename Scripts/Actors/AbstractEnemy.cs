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
    private KinematicBody2D _chasing;

    public AbstractEnemy()
    {
      _chasing = null;
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
      if (damageSource.HasActorSource())
      {
        _chasing = damageSource.GetActorSource();
      }

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
      _chasing = null;
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

    public override void _PhysicsProcess(float delta)
    {
      if (!isChasing()) return;

      var toTarget = GlobalPosition.DirectionTo(_chasing.GlobalPosition);
      var _velocity = MoveAndSlide(toTarget.Normalized() * 100);
      PlayAnimation(DirectionService.VelocityToDirection(_velocity));
    }
  }
}
