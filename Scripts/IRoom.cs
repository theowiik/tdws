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
    IList<Door> GetDoors();

    IList<AbstractMonster> GetEnemies();

    /// <summary>
    ///   Returns the spawn point.
    /// </summary>
    /// <returns>The spawn point.</returns>
    Vector2 GetSpawnPoint();
  }
}