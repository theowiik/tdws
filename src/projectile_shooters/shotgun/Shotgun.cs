using tdws.projectile_shooters.abstract_projectile_shooter;

namespace tdws.projectile_shooters.shotgun
{
  /// <summary>
  ///   A shotgun.
  /// </summary>
  public class Shotgun : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      ProjectilesPerShot = 5;
      SecondsBetweenShots = 0.8f;
      MaxOffsetAngle = 7;
      Ammo = 30;
      MagSize = 3;
      ProjectileShooterName = "Shotgun";
    }
  }
}
