using System;
using Godot;
using tdws.projectile_shooters;

namespace tdws.actors.player.holster
{
  /// <summary>
  ///   The Holster is used for managing the projectile shooters
  ///   the player is holding.
  /// </summary>
  public class Holster : Node
  {
    private int _inventoryIndex;
    private int _lastEquippedIndex;
    private int _maxInventorySize;
    private IProjectileShooter[] _projectileShooters;

    public override void _Ready()
    {
      _inventoryIndex = 0;
      _maxInventorySize = 5;
      _projectileShooters = new IProjectileShooter[_maxInventorySize];

      // Add a weapons at the start...
      Add(ProjectileShooterFactory.CreateAssaultRifle());
      Add(ProjectileShooterFactory.CreateShotgun());
      Add(ProjectileShooterFactory.CreateWonkyGun());
    }

    /// <summary>
    ///   Sets the _inventoryIndex to the given index.
    ///   Makes sure it is in a legal range.
    /// </summary>
    /// <param name="i">
    ///   The index to select.
    /// </param>
    public void Select(int i)
    {
      _inventoryIndex = i;
      SetClosestLegalInventoryIndex();
    }

    /// <summary>
    ///   Increases the _inventoryIndex value.
    ///   Sets the _inventoryIndex to 0 if it is greater or equal to the max inventory size.
    /// </summary>
    public void NextWeapon()
    {
      _inventoryIndex++;
      SetClosestLegalInventoryIndex();
    }

    /// <summary>
    ///   Decreases the _inventoryIndex value.
    ///   Sets the _inventoryIndex to the max inventory size if it is smaller than 0.
    /// </summary>
    public void PreviousWeapon()
    {
      _inventoryIndex--;
      SetClosestLegalInventoryIndex();
    }

    /// <summary>
    ///   Sets the _inventoryIndex to the closest legal value.
    ///   ... why doesn't C# have regular modulo?
    /// </summary>
    private void SetClosestLegalInventoryIndex()
    {
      if (_inventoryIndex < 0)
        _inventoryIndex = _maxInventorySize - 1;

      if (_inventoryIndex >= _maxInventorySize)
        _inventoryIndex = 0;
    }

    /// <summary>
    ///   Returns the projectile shooter that is held.
    ///   Returns null if there is no projectile shooter being held.
    /// </summary>
    /// <returns>
    ///   The projectile shooter that is held.
    ///   Returns null if there is no projectile shooter being held.
    /// </returns>
    public IProjectileShooter GetHolding()
    {
      var tooSmall = _inventoryIndex < 0;
      var tooLarge = _inventoryIndex >= _projectileShooters.Length;
      if (tooSmall || tooLarge) return null;

      return _projectileShooters[_inventoryIndex];
    }

    /// <summary>
    ///   Adds a projectile shooter to the inventory at the first free spot.
    ///   Does nothing if all spots are occupied.
    /// </summary>
    /// <param name="projectileShooter">
    ///   The projectile shooter that should be added.
    /// </param>
    /// <exception cref="NullReferenceException">
    ///   If the provided projectile shooter is null.
    /// </exception>
    public void Add(IProjectileShooter projectileShooter)
    {
      if (projectileShooter == null) throw new NullReferenceException("Projectile shooter cannot be null.");

      for (var i = 0; i < _projectileShooters.Length; i++)
        if (_projectileShooters[i] == null)
        {
          _projectileShooters[i] = projectileShooter;
          break;
        }
    }

    /// <summary>
    ///   Removes and returns the projectile shooter that is held.
    /// </summary>
    /// <returns>
    ///   The projectile shooter that was removed. Null if no projectile shooter was removed.
    /// </returns>
    public IProjectileShooter RemoveHolding()
    {
      var removedProjectileShooter = _projectileShooters[_inventoryIndex];
      _projectileShooters[_inventoryIndex] = null;
      return removedProjectileShooter;
    }
  }
}
