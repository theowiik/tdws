using System.IO;
using Godot;

namespace tdws.projectile_shooters
{
  /// <summary>
  ///   The ProjectileShooterFactory is used for creating IProjectileShooter implementations.
  /// </summary>
  public sealed class ProjectileShooterFactory
  {
    /// <summary>
    ///   Creates and returns a basic automatic rifle.
    /// </summary>
    /// <returns>
    ///   Creates and returns a basic automatic rifle.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    ///   If the projectile shooter scene is not found.
    /// </exception>
    public static IProjectileShooter CreateAssaultRifle()
    {
      if (!(GD.Load("res://src/projectile_shooters/projectile_shooter/ProjectileShooter.tscn") is PackedScene
        projectileShooter))
        throw new FileNotFoundException("Could not find ProjectileShooter.tscn");

      return projectileShooter.Instance() as IProjectileShooter;
    }

    /// <summary>
    ///   Creates and returns a basic shotgun.
    /// </summary>
    /// <returns>
    ///   Creates and returns a basic shotgun.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    ///   If the projectile shooter scene is not found.
    /// </exception>
    public static IProjectileShooter CreateShotgun()
    {
      if (!(GD.Load("res://src/projectile_shooters/projectile_shooter/ProjectileShooter.tscn") is PackedScene
        projectileShooter))
        throw new FileNotFoundException("Could not find ProjectileShooter.tscn");

      return projectileShooter.Instance() as IProjectileShooter;
    }
  }
}