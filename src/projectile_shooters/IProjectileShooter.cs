public interface IProjectileShooter
{
  /// <summary>
  /// Shoots a projectile and decreases ammo and other relevant stuff.
  /// </summary>
  void shoot();

  /// <summary>
  /// Appends a projectile.
  /// </summary>
  void appendProjectile();

  /// <summary>
  /// Reload the projectile shooter.
  /// </summary>
  void reload();
}
