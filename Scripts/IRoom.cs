using System.Collections.Generic;
using Godot;
using tdws.objects.door;
using tdws.Scripts;

namespace tdws.levels
{
  public interface IRoom
  {
    /// <summary>
    ///   Returns a list of the possible 
    /// </summary>
    /// <returns></returns>
    IEnumerable<Door> GetDoors();

    IEnumerable<AbstractEnemy> GetEnemies();

    /// <summary>
    ///   Returns the spawn point.
    /// </summary>
    /// <returns>The spawn point.</returns>
    Vector2 GetSpawnPoint();
  }
}