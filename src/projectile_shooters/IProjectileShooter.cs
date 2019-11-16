namespace tdws.projectile_shooters
{
  public interface IProjectileShooter
  {
    /// <summary>
    ///   Shoots a projectile and decreases ammo and other relevant stuff.
    /// </summary>
    void Shoot();

    /// <summary>
    ///   Appends a projectile.
    /// </summary>
    void AppendProjectile();

    /// <summary>
    ///   Reload the projectile shooter.
    /// </summary>
    void Reload();
  }
}