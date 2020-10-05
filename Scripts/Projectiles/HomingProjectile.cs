using tdws.Scripts.Actors;

namespace tdws.Scripts.Projectiles
{
  /// <summary>
  ///   A homing projectile.
  /// </summary>
  public class HomingProjectile : AbstractProjectile
  {
    /// <summary>
    ///   This constant is multiplied when turning towards a target.
    ///   0 will not turn it at all, 1 will turn it strongly.
    /// </summary>
    private const float TurnMultiplier = 0.3f;

    private AbstractEnemy _target;

    protected override void OverrideProperties()
    {
    }

    public override void _Process(float delta)
    {
      if (_target == null) return;

      var desiredDirection = _target.GlobalPosition - GlobalPosition;
      Direction += desiredDirection.Normalized() * TurnMultiplier;
      Direction =  Direction.Normalized();
    }

    /// <summary>
    ///   Stops travelling towards its target. Gets called when a body leaves
    ///   the detection area.
    /// </summary>
    /// <param name="body">The body that left the detection area.</param>
    private void OnDetectionAreaExited(object body)
    {
      if (body == _target)
        _target = null;
    }

    /// <summary>
    ///   Sets the detected body as a target if it is a enemy.
    ///   Gets called when a body enters the detection area.
    /// </summary>
    /// <param name="body"></param>
    public void OnDetectionAreaEntered(object body)
    {
      if (body is AbstractEnemy enemy) _target = enemy;
    }
  }
}