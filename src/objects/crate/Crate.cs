using Godot;
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

    /// <summary>
    /// Picks up the projectile shooter.
    /// </summary>
    public void PickUp()
    {
    }
  }
}