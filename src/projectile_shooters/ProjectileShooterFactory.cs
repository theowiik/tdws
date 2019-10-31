using Godot;
using System.IO;

/// <summary>
/// The ProjectileShooterFactory is used for creating IProjectileShooter implementations.
/// </summary>
public sealed class ProjectileShooterFactory
{
  /// <summary>
  /// Creates and returns a basic automatic rifle.
  /// </summary>
  ///
  /// <returns>
  /// Creates and returns a basic automatic rifle.
  /// </returns>
  ///
  /// <exception cref="FileNotFoundException">
  /// If the projectile shooter scene is not found.
  /// </exception>
  public static IProjectileShooter createAssaultRifle()
  {
    PackedScene projectileShooter = GD.Load("res://src/projectile_shooters/projectile_shooter/ProjectileShooter.tscn") as PackedScene;
    if (projectileShooter == null)
      throw new FileNotFoundException("Could not find ProjectileShooter.tscn");

    return projectileShooter.Instance() as IProjectileShooter;
  }
}
