using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using tdws.levels;
using tdws.objects.door;
using tdws.Scripts.Services;

namespace tdws.Scripts
{
  /// <summary>
  ///   Manages rooms and levels.
  /// </summary>
  public class RoomLoader : Node
  {
    private ILevel _level;
    private IList<Door> _doors;
    private IList<AbstractEnemy> _enemies;
    private AbstractActor _player;

    public override void _Ready()
    {
      _doors = new List<Door>();
      _enemies = new List<AbstractEnemy>();
    }

    public void SetPlayer(AbstractActor player)
    {
      _player = player ?? throw new ArgumentNullException(nameof(player), "Player can not be null");
    }

    public void NextRoom()
    {
      RemoveAllChildren();
      _doors.Clear();
      _enemies.Clear();

      // Add room
      var roomScene = GD.Load("res://src/levels/dungeon/Dungeon1.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);
      AddDoors(room);
      AddEnemies(room);
      var spawnPoint = room.GetNode("Spawn") as Position2D;
      _player.GlobalPosition = spawnPoint.Position;
    }

    private IRoom GetRandomRoom()
    {

      return null;
    }

    private void AddEnemies(TileMap room)
    {
      var possibleEnemyPositions = room.GetNode("PossibleEnemyPositions").GetChildren().Cast<Position2D>().ToList();
      var enemyPositions = ListService.SelectNRandom(possibleEnemyPositions, 3);
      foreach (Position2D enemyPosition in enemyPositions)
      {
        var skeleton = ActorFactory.CreateSkeleton();
        _enemies.Add(skeleton);
        CallDeferred("add_child", skeleton);
        skeleton.Position = enemyPosition.Position;
      }
    }

    /// <summary>
    ///   Adds door scenes to some of the possible door positions of the rooms scene.
    /// </summary>
    /// <param name="room">
    ///   The room to add doors to.
    /// </param>
    private void AddDoors(TileMap room)
    {
      var doorScene = GD.Load("res://src/objects/door/Door.tscn") as PackedScene;
      var possibleDoorPositions = room.GetNode("PossibleDoorPositions").GetChildren().Cast<Position2D>().ToList();
      var doorPositions = ListService.SelectNRandom(possibleDoorPositions, 3);

      foreach (Position2D doorPosition in doorPositions)
      {
        var door = doorScene.Instance() as Door;
        _doors.Add(door);
        AddChild(door);

        var tileCoordinate = room.WorldToMap(doorPosition.GlobalPosition);
        var instancePos = room.MapToWorld(tileCoordinate);
        // TODO: How to get the width of a tile in code.
        instancePos.x += 8;
        instancePos.y += 8;
        door.GlobalPosition = instancePos;
      }
    }

    public IEnumerable<AbstractEnemy> GetEnemies()
    {
      return _enemies;
    }

    private void RemoveAllChildren()
    {
      foreach (Node child in GetChildren()) child.QueueFree();
    }

    /// <summary>
    ///   Returns a list of the doors on the current room.
    /// </summary>
    /// <returns>
    ///   A list of the doors on the current room.
    /// </returns>
    public IEnumerable<Door> GetDoors()
    {
      return _doors;
    }
  }
}
