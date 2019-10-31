using Godot;

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
  public static IProjectileShooter createAssaultRifle()
  {
    IProjectileShooter projectileShooter = GD.Load("res://src/projectile_shooters/ProjectileShooter.tscn") as IProjectileShooter;
    return projectileShooter;
  }
}
