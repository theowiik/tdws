using System.Collections.Generic;
using System.Linq;
using Godot;
using tdws.Scripts.Actors;
using tdws.Scripts.Services;

namespace tdws.Scripts.Room
{
  public sealed class Room : TileMap
  {
    private readonly IList<Door> _doors;
    private          YSort       _enemies;

    public Room()
    {
      _doors = new List<Door>();
    }

    /// <summary>
    ///   Returns a list of the rooms doors.
    /// </summary>
    /// <returns>A list of the rooms doors.</returns>
    public IEnumerable<Door> GetDoors()
    {
      return _doors;
    }

    /// <summary>
    ///   Returns a list of the rooms enemies.
    /// </summary>
    /// <returns>A list of the rooms enemies.</returns>
    public IEnumerable<AbstractEnemy> GetEnemies()
    {
      return NodeService.GetChildrenOfType<AbstractEnemy>(_enemies);
    }

    /// <summary>
    ///   Returns the spawn point.
    /// </summary>
    /// <returns>The spawn point.</returns>
    public Vector2 GetSpawnPoint()
    {
      return GetNode<Position2D>("Spawn").GlobalPosition;
    }

    /// <summary>
    ///   Checks if all enemies are dead.
    /// </summary>
    /// <returns>True if all enemies are dead. False otherwise.</returns>
    public bool AllEnemiesAreDead()
    {
      if (GetEnemies().Count() == 0)
        return true;

      foreach (AbstractActor enemy in GetEnemies())
        if (enemy.GetHealth() > 0)
          return false; // Atleast one enemy is alive

      return true;
    }

    public override void _Ready()
    {
      _enemies = GetNode<YSort>("Enemies");
      AddDoors();
      AddEnemies();
    }

    /// <summary>
    ///   Adds doors as children to the room, and adds them to the doors list.
    /// </summary>
    private void AddDoors()
    {
      var possibleDoorPositions = NodeService.GetChildrenOfType<Position2D>(GetNode("PossibleDoorPositions"));
      var doorPositions         = ListService.SelectNRandom(possibleDoorPositions, 3);

      foreach (var doorPosition in doorPositions)
      {
        var door = NodeService.InstanceNotNull<Door>("res://Scenes/World/Door.tscn");
        _doors.Add(door);
        AddChild(door);

        var tileCoordinate = WorldToMap(doorPosition.GlobalPosition);
        var instancePos    = MapToWorld(tileCoordinate);
        var tileWidth      = (int) CellSize.x;
        instancePos.x       += tileWidth / 2;
        instancePos.y       += tileWidth / 2;
        door.GlobalPosition =  instancePos;
      }
    }

    /// <summary>
    ///   Adds enemies as children to the room, and adds them to the enemies list.
    /// </summary>
    private void AddEnemies()
    {
      var possibleEnemyPositions = NodeService.GetChildrenOfType<Position2D>(GetNode("PossibleEnemyPositions"));
      var enemyPositions         = ListService.SelectNRandom(possibleEnemyPositions, 3);

      foreach (var enemyPosition in enemyPositions)
      {
        var skeleton = ActorFactory.CreateSkeleton();
        _enemies.AddChild(skeleton);
        skeleton.Position = enemyPosition.Position;
      }
    }
  }
}