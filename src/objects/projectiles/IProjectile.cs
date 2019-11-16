using tdws.actors.player;

namespace tdws.objects.projectiles
{
  public interface IProjectile : IMovable, IDamageSource
  {
    /// <summary>
    ///   Destroys the projectile.
    /// </summary>
    void Destroy();
  }
}