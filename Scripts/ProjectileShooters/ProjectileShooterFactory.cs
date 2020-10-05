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
      return NodeService.InstanceNotNull<AbstractProjectileShooter>(
        "res://Scenes/ProjectileShooters/AssaultRifle.tscn");
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

    public static IProjectileShooter CreateRocketLauncher()
    {
      return NodeService.InstanceNotNull<AbstractProjectileShooter>(
        "res://Scenes/ProjectileShooters/RocketLauncher.tscn");
    }

    public static IProjectileShooter CreateGodGun()
    {
      return NodeService.InstanceNotNull<AbstractProjectileShooter>("res://Scenes/ProjectileShooters/GodGun.tscn");
    }
  }
}