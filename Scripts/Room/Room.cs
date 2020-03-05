using System.Collections.Generic;
using System.Linq;
using Godot;
using tdws.Scripts.Actors;
using tdws.Scripts.Services;

namespace tdws.Scripts.Room
{
  public class Room : TileMap, IRoom
  {
    private readonly IList<Door> _doors;
    private readonly IList<AbstractEnemy> _enemies;

    public Room()
    {
      _doors = new List<Door>();
      _enemies = new List<AbstractEnemy>();
    }

    public IEnumerable<Door> GetDoors()
    {
      return _doors;
    }

    public IEnumerable<AbstractEnemy> GetEnemies()
    {
      return _enemies;
    }

    public Vector2 GetSpawnPoint()
    {
      return GetNode<Position2D>("Spawn").GlobalPosition;
    }

    public override void _Ready()
    {
      AddDoors();
      AddEnemies();
    }

    /// <summary>
    ///   Adds doors as children to the room, and adds them to the doors list.
    /// </summary>
    private void AddDoors()
    {
      var possibleDoorPositions = NodeService.GetChildrenOfType<Position2D>(GetNode("PossibleDoorPositions"));
      var doorPositions = ListService.SelectNRandom(possibleDoorPositions, 3);

      foreach (var doorPosition in doorPositions)
      {
        var door = NodeService.InstanceNotNull<Door>("res://Scenes/World/Door.tscn");
        _doors.Add(door);
        AddChild(door);

        var tileCoordinate = WorldToMap(doorPosition.GlobalPosition);
        var instancePos = MapToWorld(tileCoordinate);
        int tileWidth = (int)CellSize.x;
        instancePos.x += tileWidth / 2;
        instancePos.y += tileWidth / 2;
        door.GlobalPosition = instancePos;
      }
    }

    /// <summary>
    ///   Adds enemies as children to the room, and adds them to the enemies list.
    /// </summary>
    private void AddEnemies()
    {
      var possibleEnemyPositions = NodeService.GetChildrenOfType<Position2D>(GetNode("PossibleEnemyPositions"));
      var enemyPositions = ListService.SelectNRandom(possibleEnemyPositions, 3);

      foreach (var enemyPosition in enemyPositions)
      {
        var skeleton = ActorFactory.CreateSkeleton();
        _enemies.Add(skeleton);
        CallDeferred("add_child", skeleton);
        skeleton.Position = enemyPosition.Position;
      }
    }

    public bool AllEnemiesAreDead()
    {
      if (_enemies.Count() == 0)
        return true;

      foreach (var enemy in _enemies) // Check if there exists a enemy.
        if (enemy != null)
          return false;

      return true;
    }
  }
}
