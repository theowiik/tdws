using System;
using System.Collections.Generic;
using Godot;
using tdws.Scripts.Actors;
using tdws.Scripts.Services;

namespace tdws.Scripts.Room
{
  /// <summary>
  ///   Manages rooms and levels.
  /// </summary>
  public class RoomLoader : Node
  {
    private AbstractActor _player;
    private IRoom _room;
    private Region _region;

    public RoomLoader()
    {
      _region = new DungeonRegion();
    }

    public void SetPlayer(AbstractActor player)
    {
      _player = Objects.RequireNonNull(player);
    }

    public void NextRoom()
    {
      RemoveAllChildren();
      _room = GetRandomRoom();

      AddChild((Room)_room); // Hmm...
      _player.GlobalPosition = _room.GetSpawnPoint();
    }

    private IRoom GetRandomRoom()
    {
      // return NodeService.InstanceNotNull<Room>("res://Scenes/Rooms/Dungeons/Dungeon1.tscn");
      return _region.GetRandomRoom();
    }

    public IEnumerable<AbstractEnemy> GetEnemies()
    {
      return _room.GetEnemies();
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
      return _room.GetDoors();
    }
  }
}
