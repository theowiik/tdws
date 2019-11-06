using Godot;

namespace tdws.objects.projectiles.projectile
{
  public class ProjectileFactory
  {
    /// <summary>
    ///   Creates and returns a standard bullet.
    /// </summary>
    /// <returns>
    ///   A standard bullet scene.
    /// </returns>
    public static PackedScene CreateBullet()
    {
      PackedScene packedScene = GD.Load("res://src/objects/projectiles/projectile/Projectile.tscn") as PackedScene;
      return packedScene;
    }
  }
}