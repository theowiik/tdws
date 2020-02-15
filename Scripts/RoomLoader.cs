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
      var roomScene = GD.Load("res://Scenes/Rooms/Dungeons/Dungeon1.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);
      var spawnPoint = room.GetNode("Spawn") as Position2D;
      _player.GlobalPosition = spawnPoint.Position;
    }

    private IRoom GetRandomRoom()
    {

      return null;
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
