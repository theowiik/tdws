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

      var roomScene = GD.Load("res://src/levels/Room.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);

      // add doors START
      var possibleDoorPositions = room.GetNode("PossibleDoorPositions").GetChildren();
      var doorScene = GD.Load("res://src/objects/door/Door.tscn") as PackedScene;
      foreach (Position2D doorPosition in possibleDoorPositions)
      {
        var door = doorScene.Instance() as Node2D;
        AddChild(door);
        var instancePos = room.WorldToMap(doorPosition.GlobalPosition);
        door.SetGlobalPosition(instancePos);
      }

      // add doors END
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