using System.Collections.Generic;
using Godot;
using tdws.actors.player;
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
    private PlayerController _player;

    public override void _Ready()
    {
//      _player = GetNode("Player");
      _doors = new List<Door>();
    }

    public void NextRoom()
    {
      RemoveAllChildren();
      _doors.Clear();

      // Add room
      var roomScene = GD.Load("res://src/levels/Room.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);
      room.SetGlobalPosition(new Vector2((float) GD.RandRange(0, 30), (float) GD.RandRange(0, 30)));

      AddDoors(room);
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
    public IList<Door> GetDoors()
    {
      return _doors;
    }
  }
}