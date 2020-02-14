using tdws.Scripts;

namespace tdws.objects.projectiles
{
  public interface IProjectile : IDamageSource
  {
    /// <summary>
    ///   Destroys the projectile.
    /// </summary>
    void Destroy();
  }
}