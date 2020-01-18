using System;
using System.Collections.Generic;
using Godot;
using tdws.actors.abstract_actor;
using tdws.actors.monsters;
using tdws.actors.monsters.abstract_monster;
using tdws.objects.door;
using tdws.services;

namespace tdws.core
{
  /// <summary>
  ///   Manages rooms and levels.
  /// </summary>
  public class RoomLoader : Node
  {
    private IList<Door> _doors;
    private IList<AbstractMonster> _enemies;
    private AbstractActor _player;

    public override void _Ready()
    {
      _doors = new List<Door>();
      _enemies = new List<AbstractMonster>();
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
      var roomScene = GD.Load("res://src/levels/RoomTemplate.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);
      AddDoors(room);
      AddEnemies(room);
      var spawnPoint = room.GetNode("Spawn") as Position2D;
      _player.SetGlobalPosition(spawnPoint.Position);
    }

    private void AddEnemies(TileMap room)
    {
      var possibleEnemyPositions = room.GetNode("PossibleEnemyPositions").GetChildren();
      var enemyPositions = ListService.SelectNRandom(possibleEnemyPositions, 3);
      foreach (Position2D enemyPosition in enemyPositions)
      {
        var skeleton = MonsterFactory.CreateSkeleton();
        _enemies.Add(skeleton);
        CallDeferred("add_child", skeleton);
        skeleton.SetPosition(enemyPosition.Position);
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
      var possibleDoorPositions = room.GetNode("PossibleDoorPositions").GetChildren();
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
        door.SetGlobalPosition(instancePos);
      }
    }

    public IEnumerable<AbstractMonster> GetEnemies()
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