using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using tdws.Scripts.Actors;
using tdws.Scripts.Services;

namespace tdws.Scripts
{
  /// <summary>
  ///   Manages rooms and levels.
  /// </summary>
  public class RoomLoader : Node
  {
    private AbstractActor _player;
    private IRoom _room;

    public void SetPlayer(AbstractActor player)
    {
      _player = player ?? throw new ArgumentNullException(nameof(player), "Player can not be null");
    }

    public void NextRoom()
    {
      RemoveAllChildren();
      _room = GetRandomRoom();

      AddChild((Room) _room); // Hmm...
      _player.GlobalPosition = _room.GetSpawnPoint();
    }

    private static IRoom GetRandomRoom()
    {
      return NodeService.InstanceNotNull<Room>("res://Scenes/Rooms/Dungeons/Dungeon1.tscn");
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