using Godot;
using tdws.Scripts.ProjectileShooters;

namespace tdws.Scripts
{
  /// <summary>
  ///   The Crate class holds a projectile shooter.
  /// </summary>
  public class Crate : Area2D
  {
    private AudioStreamPlayer _lootPlayer;
    private bool              _pickedUp;

    public Crate()
    {
      _pickedUp = false;
    }

    private IProjectileShooter ProjectileShooter { get; set; }

    public override void _Ready()
    {
      ProjectileShooter = ProjectileShooterFactory.CreateShotgun();
      _lootPlayer       = GetNode<AudioStreamPlayer>("LootPlayer");
    }

    private void OnLootPlayerFinished()
    {
      QueueFree();
    }

    /// <summary>
    ///   Hides the crate.
    /// </summary>
    private void Hide()
    {
      GetNode("Sprite").QueueFree();
    }

    public void OnBodyEntered(object body)
    {
      if (_pickedUp) return;

      if (body is ICanPickup canPickup)
      {
        _pickedUp = true;
        var projectileShooter = ProjectileShooterFactory.CreateAlienGun();
        canPickup.PickupProjectileShooter(projectileShooter);
        ((AudioStreamPlayer) GetNode("LootPlayer")).Play();
        Hide();
      }
    }
  }
}