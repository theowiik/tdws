using tdws.Scripts.Projectiles;

namespace tdws.Scripts.ProjectileShooters
{
  public class AlienGun : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      Projectile = ProjectileFactory.CreateHomingProjectile();
      ProjectilesPerShot = 2;
      MaxOffsetAngle = 10;
      SecondsBetweenShots = 0.7f;
      ProjectileShooterName = "Alien Gun";
    }
  }
}
