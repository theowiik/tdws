public interface IProjectileShooter
{
  /// <summary>
  /// Creates a new projectile with the direction pointing towards the mouse.
  /// </summary>
  void shoot();

  /// <summary>
  /// Reload the projectile shooter.
  /// </summary>
  void reload();
}
