using System;
using System.Collections.Generic;
using Godot;
using tdws.actors.player;
using tdws.objects.door;

namespace tdws.core
{
  /// <summary>
  ///   Manages rooms and levels.
  /// </summary>
  public class RoomLoader : Node
  {
    private PlayerController _player;

    public override void _Ready()
    {
//      _player = GetNode("Player");
    }

    public void NextRoom()
    {
      RemoveAllChildren();

      // Add room
      var roomScene = GD.Load("res://src/levels/Room.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);

      // Add doors
      var possibleDoorPositions = room.GetNode("PossibleDoorPositions").GetChildren();
      var doorScene = GD.Load("res://src/objects/door/Door.tscn") as PackedScene;
      foreach (Position2D doorPosition in possibleDoorPositions)
      {
        var door = doorScene.Instance() as Node2D;
        AddChild(door);

        var tileCoordinate = room.WorldToMap(doorPosition.GlobalPosition);
        var instancePos = room.MapToWorld(tileCoordinate);
        // TODO: How to get the width of a tile in code.
        instancePos.x += 8;
        instancePos.y += 8;
        door.SetGlobalPosition(instancePos);
      }
    }

    private void RemoveAllChildren()
    {
      foreach (Node child in GetChildren())
      {
        child.QueueFree();
      }
    }

    /// <summary>
    ///   Returns a list of the doors on the current room.
    /// </summary>
    /// <returns>
    ///   a list of the doors on the current room.
    /// </returns>
    public List<Door> GetDoors()
    {
      return new List<Door>();
    }
  }
}