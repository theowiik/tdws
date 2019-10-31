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

  public override void _Ready()
  {
    _inventoryIndex = 0;
    _projectileShooters = new List<IProjectileShooter>(10);
  }

  public override void _Process(float delta)
  {
    if (Input.IsActionPressed("weapon_next")) GD.Print("do something ...");
    if (Input.IsActionPressed("weapon_previous")) GD.Print("do something else ...");
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
