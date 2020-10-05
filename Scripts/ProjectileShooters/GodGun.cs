namespace tdws.Scripts.ProjectileShooters
{
  public class GodGun : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      ProjectilesPerShot    = 3;
      MaxOffsetAngle        = 0;
      SecondsBetweenShots   = 0.05f;
      ProjectileShooterName = "God Gun";
      Ammo                  = 99999999;
    }
  }
}