using System.Collections.Generic;
using Godot;
using tdws.actors.monsters.abstract_monster;
using tdws.objects.door;

namespace tdws.core
{
  public interface IRoom
  {
    IList<Door> GetDoors();

    IList<AbstractMonster> GetEnemies();

    Vector2 GetSpawnPoint();
  }
}