using tdws.actors.player;

namespace tdws.objects.projectiles.projectile
{
  public interface IProjectile : IMovable
  {
    /// <summary>
    ///   Destroys the projectile.
    /// </summary>
    void Destroy();
  }
}