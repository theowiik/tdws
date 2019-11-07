using tdws.projectile_shooters.projectile_shooter;

namespace tdws.projectile_shooters.assault_rifle
{
  /// <summary>
  ///   A assault rifle.
  /// </summary>
  public class AssaultRifle : ProjectileShooter
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