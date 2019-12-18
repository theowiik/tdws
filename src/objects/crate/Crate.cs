using Godot;
using tdws.actors;
using tdws.projectile_shooters;

namespace tdws.objects.crate
{
  /// <summary>
  ///   The Crate class holds a projectile shooter.
  /// </summary>
  public class Crate : Area2D
  {
    public IProjectileShooter ProjectileShooter { get; private set; }

    public override void _Ready()
    {
      ProjectileShooter = ProjectileShooterFactory.CreateShotgun();
    }

    public void OnBodyEntered(object body)
    {
      // Find this hard to read. But the formatter wants it this way.
      if (!(body is ICanPickup canPickup)) return;

      var projectileShooter = ProjectileShooterFactory.CreateAlienGun();
      canPickup.PickupProjectileShooter(projectileShooter);
      QueueFree();
    }
  }
}