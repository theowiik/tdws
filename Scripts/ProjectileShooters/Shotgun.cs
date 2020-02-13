namespace tdws.Scripts.ProjectileShooters
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
