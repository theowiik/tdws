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
      GD.Print("indsaiaasdoid");
      RemoveAllChildren();

      var roomScene = GD.Load("res://src/levels/Room.tscn") as PackedScene;
      var room = roomScene.Instance() as TileMap;
      AddChild(room);

      room.SetGlobalPosition(
        new Vector2(
          (float) GD.RandRange(0, 30),
          (float) GD.RandRange(0, 30))
      );
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