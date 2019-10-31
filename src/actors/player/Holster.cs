using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The Holster is used for managing the projectile shooters
/// the player is holding.
/// </summary>
public class Holster : Node
{
  private IProjectileShooter[] _projectileShooters;
  private int _inventoryIndex;
  private int _maxInventorySize;

  public override void _Ready()
  {
    _inventoryIndex = 0;
    _maxInventorySize = 5;
    _projectileShooters = new IProjectileShooter[_maxInventorySize];

    // Add a weapon at the start...
    IProjectileShooter proj = ProjectileShooterFactory.createAssaultRifle();
    Add(proj);
  }

  public override void _Process(float delta)
  {
    bool next = Input.IsActionJustReleased("weapon_next");
    bool previous = Input.IsActionJustReleased("weapon_previous");

    if (next) NextWeapon();
    if (previous) PreviousWeapon();
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
  /// Returns null if there is no projectile shooter being held.
  /// </summary>
  ///
  /// <returns>
  /// The projectile shooter that is held.
  /// Returns null if there is no projectile shooter being held.
  /// </returns>
  public IProjectileShooter GetHolding()
  {
    bool tooSmall = _inventoryIndex < 0;
    bool tooLarge = _inventoryIndex >= _projectileShooters.Length;
    if (tooSmall || tooLarge) return null;

    return _projectileShooters[_inventoryIndex];
  }

  /// <summary>
  /// Adds a projectile shooter to the inventory at the first free spot.
  /// Does nothing if all spots are occupied.
  /// </summary>
  ///
  /// <param name="projectileShooter">
  /// The projectile shooter that should be added.
  /// </param>
  /// 
  /// <exception cref="NullReferenceException">
  /// If the provided projectile shooter is null.
  /// </exception>
  public void Add(IProjectileShooter projectileShooter)
  {
    if (projectileShooter == null) throw new NullReferenceException("Projectile shooter cannot be null.");

    for (int i = 0; i < _projectileShooters.Length; i++)
      if (_projectileShooters[i] == null)
      {
        _projectileShooters[i] = projectileShooter;
        break;
      }
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
