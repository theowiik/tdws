using Godot;

namespace tdws.objects.projectiles
{
  public static class ProjectileFactory
  {
    /// <summary>
    ///   Creates and returns a standard bullet.
    /// </summary>
    /// <returns>
    ///   A standard bullet scene.
    /// </returns>
    public static PackedScene CreateBullet()
    {
      var packedScene = GD.Load("res://src/objects/projectiles/bullet/Bullet.tscn") as PackedScene;
      return packedScene;
    }

    public static PackedScene CreateHomingProjectile()
    {
      return GD.Load("res://src/objects/projectiles/homing_projectile/HomingProjectile.tscn") as PackedScene;
    }
  }
}
