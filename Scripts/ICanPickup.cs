using tdws.Scripts.ProjectileShooters;

namespace tdws.Scripts
{
  /// <summary>
  ///   Represents objects that can pick up coins and projectile shooters.
  /// </summary>
  public interface ICanPickup
  {
    /// <summary>
    ///   Picks up a projectile shooter. Does nothing if the given projectile shooter is null.
    /// </summary>
    /// <param name="projectileShooter">
    ///   The projectile shooter that should be picked up.
    /// </param>
    void PickupProjectileShooter(IProjectileShooter projectileShooter);

    /// <summary>
    ///   Picks up coins.
    /// </summary>
    /// <param name="amount">
    ///   The amount of coins.
    /// </param>
    void PickupCoins(int amount);
  }
}