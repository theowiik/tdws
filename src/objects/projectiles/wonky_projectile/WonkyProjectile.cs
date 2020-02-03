using Godot;
using tdws.objects.projectiles.abstract_projectile;

namespace tdws.objects.projectiles.wonky_projectile
{
  /// <summary>
  ///   A wonky projectile that travels in a weird and unpredictable way.
  /// </summary>
  public class WonkyProjectile : AbstractProjectile
  {
    /// <summary>
    ///   The amount of time in seconds it takes for the projectile to change its direction.
    /// </summary>
    private const float NewDirTime = 0.2f;

    /// <summary>
    ///   The radians to rotate the projectile every frame.
    /// </summary>
    private const float RotateAmount = 0.1f;

    private float _elapsedTime;

    protected override void OverrideProperties()
    {
      Speed = 400;
    }

    protected override void Move(float delta)
    {
      _elapsedTime += delta;
      if (_elapsedTime > NewDirTime)
      {
        SwitchDirection();
        _elapsedTime = 0;
      }

      RotateDirection();

      var transform = Transform;
      transform.origin += Direction * Speed * delta;
      SetTransform(transform);
    }

    /// <summary>
    ///   Rotates the direction of the projectile in a random direction with
    ///   the RotateAmount constant.
    /// </summary>
    private void RotateDirection()
    {
      var rotate = RotateAmount;
      if (GD.RandRange(0, 1) >= 0.5f) rotate *= -1;

      Direction = Direction.Rotated(rotate).Normalized();
    }

    /// <summary>
    ///   Sets the direction to a random new direction.
    /// </summary>
    private void SwitchDirection()
    {
      Direction = new Vector2(
        (float) GD.RandRange(-1, 1),
        (float) GD.RandRange(-1, 1)
      ).Normalized();
    }
  }
}
