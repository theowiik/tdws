using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The Holster is used for managing the projectile shooters
/// the player is holding.
/// </summary>
public class Holster : Node
{
  private List<IProjectileShooter> _projectileShooters;
  private int _inventoryIndex;
  private int _maxInventorySize;

  public override void _Ready()
  {
    _inventoryIndex = 0;
    _maxInventorySize = 5;
    _projectileShooters = new List<IProjectileShooter>(10);
  }

  public override void _Process(float delta)
  {
    if (Input.IsActionJustReleased("weapon_next")) NextWeapon();
    if (Input.IsActionJustReleased("weapon_previous")) PreviousWeapon();

    GD.Print(_inventoryIndex);
  }

  /// <summary>
  /// Increases the _inventoryIndex value.
  /// Sets the _inventoryIndex to 0 if it is greater or equal to the max inventory size.
  /// </summary>
  private void NextWeapon()
  {
    _inventoryIndex++;
    setClosestLegalInventoryIndex();
  }

  /// <summary>
  /// Decreases the _inventoryIndex value.
  /// Sets the _inventoryIndex to the max inventory size if it is smaller than 0.
  /// </summary>
  private void PreviousWeapon()
  {
    _inventoryIndex--;
    setClosestLegalInventoryIndex();
  }

  /// <summary>
  /// Sets the _inventoryIndex to the closest legal value.
  /// 
  /// ... why doesn't C# have regular modulo?
  /// </summary>
  private void setClosestLegalInventoryIndex()
  {
    if (_inventoryIndex < 0)
      _inventoryIndex = (_maxInventorySize - 1);

    if (_inventoryIndex >= _maxInventorySize)
      _inventoryIndex = 0;
  }

  /// <summary>
  /// Returns the projectile shooter that is held.
  /// </summary>
  ///
  /// <returns>
  /// The projectile shooter that is held.
  /// </returns>
  public IProjectileShooter GetHolding()
  {
    return _projectileShooters[_inventoryIndex];
  }

  /// <summary>
  /// Adds a projectile shooter to the inventory.
  /// </summary>
  ///
  /// <param name="projectileShooter">
  /// The projectile shooter that should be added.
  /// </param>
  public void Add(IProjectileShooter projectileShooter)
  {
    _projectileShooters.Add(projectileShooter);
  }

  /// <summary>
  /// Removes and returns the projectile shooter that is held.
  /// </summary>
  ///
  /// <returns>
  /// The projectile shooter that was removed. Null if no projectile shooter was removed.
  /// </returns>
  public IProjectileShooter RemoveHolding()
  {
    var removedProjectileShooter = _projectileShooters[_inventoryIndex];
    _projectileShooters[_inventoryIndex] = null;
    return removedProjectileShooter;
  }
}
