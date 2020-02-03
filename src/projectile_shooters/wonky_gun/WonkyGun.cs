using Godot;
using tdws.projectile_shooters.abstract_projectile_shooter;

namespace tdws.projectile_shooters.wonky_gun
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
