using tdws.objects.projectiles;
using tdws.projectile_shooters.projectile_shooter;

namespace tdws.projectile_shooters.alien_gun
{
  public class AlienGun : ProjectileShooter
  {
    protected override void OverrideProperties()
    {
      Projectile = ProjectileFactory.CreateHomingProjectile();
      ProjectilesPerShot = 2;
      MaxOffsetAngle = 10;
      SecondsBetweenShots = 0.7f;
    }
  }
}