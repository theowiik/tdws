using tdws.projectile_shooters.abstract_projectile_shooter;

namespace tdws.projectile_shooters.assault_rifle
{
  /// <summary>
  ///   A assault rifle.
  /// </summary>
  public class AssaultRifle : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      SecondsBetweenShots = 0.1f;
      ProjectilesPerShot = 1;
      MagSize = 31;
      Ammo = 300;
      ProjectileShooterName = "Assault Rifle";
      MaxOffsetAngle = 5;
    }
  }
}
