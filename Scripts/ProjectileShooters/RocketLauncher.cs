using tdws.Scripts.Projectiles;

namespace tdws.Scripts.ProjectileShooters
{
  /// <summary>
  ///   A rocket launcher.
  /// </summary>
  public class RocketLauncher : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      ProjectilesPerShot    = 1;
      SecondsBetweenShots   = 0.3f;
      MaxOffsetAngle        = 2;
      Ammo                  = 100;
      MagSize               = 100;
      ProjectileShooterName = "Rocket Launcher";
      Projectile            = ProjectileFactory.CreateRocket();
    }
  }
}