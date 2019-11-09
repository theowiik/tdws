using System.IO;
using Godot;

namespace tdws.projectile_shooters
{
  /// <summary>
  ///   The ProjectileShooterFactory is used for creating IProjectileShooter implementations.
  /// </summary>
  public static class ProjectileShooterFactory
  {
    /// <summary>
    ///   Creates and returns a basic automatic rifle.
    /// </summary>
    /// <returns>
    ///   Creates and returns a basic automatic rifle.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    ///   If the assault rifle scene is not found.
    /// </exception>
    public static IProjectileShooter CreateAssaultRifle()
    {
      if (!(GD.Load("res://src/projectile_shooters/assault_rifle/AssaultRifle.tscn") is PackedScene
        projectileShooter))
        throw new FileNotFoundException("Could not find AssaultRifle.tscn");

      return projectileShooter.Instance() as IProjectileShooter;
    }

    /// <summary>
    ///   Creates and returns a basic shotgun.
    /// </summary>
    /// <returns>
    ///   Creates and returns a basic shotgun.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    ///   If the shotgun scene is not found.
    /// </exception>
    public static IProjectileShooter CreateShotgun()
    {
      if (!(GD.Load("res://src/projectile_shooters/shotgun/Shotgun.tscn") is PackedScene
        projectileShooter))
        throw new FileNotFoundException("Could not find Shotgun.tscn");

      return projectileShooter.Instance() as IProjectileShooter;
    }

    /// <summary>
    ///   Creates and returns a wonky gun.
    /// </summary>
    /// <returns>
    ///   A wonky gun.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    ///   If the projectile shooter scene is not found.
    /// </exception>
    public static IProjectileShooter CreateWonkyGun()
    {
      if (!(GD.Load("res://src/projectile_shooters/wonky_gun/WonkyGun.tscn") is PackedScene
        projectileShooter))
        throw new FileNotFoundException("Could not find WonkyGun.tscn");

      return projectileShooter.Instance() as IProjectileShooter;
    }
  }
}