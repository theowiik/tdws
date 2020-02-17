using Godot;

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

    private readonly Timer _chaseTimer;

    /// <summary>
    ///   The target destination.
    /// </summary>
    private KinematicBody2D _target;

    protected AbstractEnemy()
    {
      _chaseTimer = new Timer();
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
        _target = damageSource.GetActorSource();

      _chaseTimer.Start(ChaseTime);
    }

    /// <summary>
    ///   Gets called when a body enters the detection area.
    /// </summary>
    /// <param name="body">
    ///   The body that entered the area.
    /// </param>
    private void OnDetectionAreaExited(object body)
    {
      if (body == _target)
        _chaseTimer.Start(ChaseTime);
    }

    /// <summary>
    ///   Gets called when the chase timer runs out.
    /// </summary>
    public void OnChaseTimerTimeout()
    {
      _target = null;
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
        _target = body2D;
      }
    }

    public override void _PhysicsProcess(float delta)
    {
      if (_target == null) return;

      var toTarget = GlobalPosition.DirectionTo(_target.GlobalPosition);
      MoveAndSlide(toTarget.Normalized() * 100);
    }
  }
}
