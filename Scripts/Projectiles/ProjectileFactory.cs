using Godot;
using tdws.Scripts.Services;

namespace tdws.Scripts.Projectiles
{
  /// <summary>
  ///   Creates projectiles.
  /// </summary>
  public static class ProjectileFactory
  {
    /// <summary>
    ///   <returns>The scene file for a bullet.</returns>
    /// </summary>
    public static PackedScene CreateBullet()
    {
      return NodeService.LoadNotNull<PackedScene>("res://Scenes/Projectiles/Bullet.tscn");
    }

    /// <summary>
    ///   <returns>The scene file for a homing projectile.</returns>
    /// </summary>
    public static PackedScene CreateHomingProjectile()
    {
      return NodeService.LoadNotNull<PackedScene>("res://Scenes/Projectiles/HomingProjectile.tscn");
    }

    /// <summary>
    ///   <returns>The scene file for a homing projectile.</returns>
    /// </summary>
    public static PackedScene CreateWonkyProjectile()
    {
      return NodeService.LoadNotNull<PackedScene>("res://Scenes/Projectiles/WonkyProjectile.tscn");
    }

    /// <summary>
    ///   <returns>The scene file for a rocket.</returns>
    /// </summary>
    public static PackedScene CreateRocket()
    {
      return NodeService.LoadNotNull<PackedScene>("res://Scenes/Projectiles/Rocket.tscn");
    }
  }
}