using System.IO;
using Godot;
using tdws.Scripts.Services;

namespace tdws.Scripts.ProjectileShooters
{
  /// <summary>
  ///   The ProjectileShooterFactory is used for creating IProjectileShooter implementations.
  /// </summary>
  public static class ProjectileShooterFactory
  {
    public static IProjectileShooter CreateAssaultRifle()
    {
      return NodeService.InstanceNotNull<AbstractProjectileShooter>("res://Scenes/ProjectileShooters/AssaultRifle.tscn");
    }

    public static IProjectileShooter CreateShotgun()
    {
      return NodeService.InstanceNotNull<AbstractProjectileShooter>("res://Scenes/ProjectileShooters/Shotgun.tscn");
    }

    public static IProjectileShooter CreateWonkyGun()
    {
      return NodeService.InstanceNotNull<AbstractProjectileShooter>("res://Scenes/ProjectileShooters/WonkyGun.tscn");
    }

    public static IProjectileShooter CreateAlienGun()
    {
      return NodeService.InstanceNotNull<AbstractProjectileShooter>("res://Scenes/ProjectileShooters/AlienGun.tscn");
    }
  }
}