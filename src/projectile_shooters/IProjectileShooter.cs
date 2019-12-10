using tdws.actors.abstract_actor;

namespace tdws.projectile_shooters
{
  public interface IProjectileShooter
  {
    /// <summary>
    ///   Shoots a projectile and decreases ammo and other relevant stuff.
    /// </summary>
    /// <param name="actorSource">
    ///   The actor that is responsible for the projectile shot.
    /// </param>
    void Shoot(AbstractActor actorSource);

    /// <summary>
    ///   Shoots a projectile and decreases ammo and other relevant stuff.
    /// </summary>
    void Shoot();

    /// <summary>
    ///   Adds projectiles.
    /// </summary>
    /// <param name="actor">
    ///   The actor that is responsible for the projectiles.
    ///   Can be null if there is no responsible actor.
    /// </param>
    void AppendProjectiles(AbstractActor actor);

    /// <summary>
    ///   Reload the projectile shooter.
    /// </summary>
    void Reload();
  }
}