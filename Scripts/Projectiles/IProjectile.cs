namespace tdws.Scripts.Projectiles
{
  public interface IProjectile : IDamageSource
  {
    /// <summary>
    ///   Destroys the projectile.
    /// </summary>
    void Destroy();
  }
}