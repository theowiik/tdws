using System.Collections.Generic;
using Godot;

/// <summary>
/// The InventoryManager is used for managing inventories.
/// </summary>
sealed class InventoryManager
{
  private List<IProjectileShooter> _projectileShooters;
  private int _inventoryIndex;

  public InventoryManager()
  {
    _inventoryIndex = 0;
    _projectileShooters = new List<IProjectileShooter>(10);
  }

  /// <summary>
  /// Returns the projectile shooter that is held.
  /// </summary>
  ///
  /// <returns>
  /// The projectile shooter that is held.
  /// </returns>
  public IProjectileShooter GetHolding() {
    return _projectileShooters[_inventoryIndex];
  }

  /// <summary>
  /// Adds a projectile shooter to the inventory.
  /// </summary>
  ///
  /// <param name="projectileShooter">
  /// The projectile shooter that should be added.
  /// </param>
  public void Add(IProjectileShooter projectileShooter) {
    _projectileShooters.Add(projectileShooter);
  }

  /// <summary>
  /// Removes and returns the projectile shooter that is held.
  /// </summary>
  ///
  /// <returns>
  /// The projectile shooter that was removed. Null if no projectile shooter was removed.
  /// </returns>
  public IProjectileShooter RemoveHolding() {
    var removedProjectileShooter = _projectileShooters[_inventoryIndex];
    _projectileShooters[_inventoryIndex] = null;
    return removedProjectileShooter;
  }
}
