using System.Collections.Generic;
using Godot;
using tdws.actors.player;
using tdws.objects.door;

namespace tdws.core
{
  /// <summary>
  ///   Manages rooms and levels.
  /// </summary>
  public class LevelLoader : Node
  {
    private PlayerController _player;

    public override void _Ready()
    {
      _player = GetNode<PlayerController>("Player");
    }

    public void nextRoom()
    {
      // do something
      var room = ...;
      AddChild(room);
    }

    /// <summary>
    ///   Returns a list of the doors on the current room.
    /// </summary>
    /// <returns>
    ///   a list of the doors on the current room.
    /// </returns>
    public List<Door> getDoors()
    {
      return new List<Door>();
    }
  }
}