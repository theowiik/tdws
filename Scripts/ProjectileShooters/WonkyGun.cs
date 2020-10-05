using tdws.Scripts.Projectiles;

namespace tdws.Scripts.ProjectileShooters
{
  public class WonkyGun : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      Projectile         = ProjectileFactory.CreateWonkyProjectile();
      ProjectilesPerShot = 1;
    }
  }
}