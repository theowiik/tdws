using Godot;
using tdws.Scripts.ProjectileShooters;

namespace tdws.Scripts.Services
{
  public class WonkyGun : AbstractProjectileShooter
  {
    protected override void OverrideProperties()
    {
      Projectile = GD.Load("res://src/objects/projectiles/wonky_projectile/WonkyProjectile.tscn") as PackedScene;
      ProjectilesPerShot = 1;
    }
  }
}
