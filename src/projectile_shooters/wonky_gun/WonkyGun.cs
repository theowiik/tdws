using Godot;
using tdws.projectile_shooters.projectile_shooter;

namespace tdws.projectile_shooters.wonky_gun
{
  public class WonkyGun : ProjectileShooter
  {
    protected override void OverrideProperties()
    {
      Projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
    }
  }
}