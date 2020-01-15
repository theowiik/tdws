using System;
using System.Collections.Generic;
using Godot;
using tdws.actors.player;
using tdws.objects.door;
using Array = Godot.Collections.Array;

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
      var doorScene = GD.Load("res://src/objects/door/Door.tscn") as PackedScene;
      var possibleDoorPositions = room.GetNode("PossibleDoorPositions").GetChildren();
      var doorPositions = SelectRandomPositions(possibleDoorPositions, 3);

      foreach (Position2D doorPosition in doorPositions)
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

    private IEnumerable<object> SelectRandomPositions(Array possibleDoorPositions, int amount)
    {
      var shuffledList = ShuffleList(possibleDoorPositions);
      var nSelected = 0;
      var output = new List<object>();
      foreach (var doorPos in shuffledList)
      {
        output.Add(doorPos);
        nSelected++;

        if (nSelected >= amount)
          break;
      }

      // TODO: Throw error if nSelected != amount?

      return output;
    }

    /// <summary>
    ///   Shuffles a list.
    /// </summary>
    /// <param name="inputList">The list to shuffle.</param>
    /// <typeparam name="TE">The type of objects the list contains.</typeparam>
    /// <returns>A shuffled list.</returns>
    private static IList<TE> ShuffleList<TE>(IList<TE> inputList)
    {
      var randomList = new List<TE>();

      var r = new Random();
      while (inputList.Count > 0)
      {
        var randomIndex = r.Next(0, inputList.Count);
        randomList.Add(inputList[randomIndex]);
        inputList.RemoveAt(randomIndex);
      }

      return randomList;
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