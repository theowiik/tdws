using System.Collections.Generic;
using Godot;
using tdws.Scripts.Services;

namespace tdws.Scripts.Room
{
  /// <summary>
  ///   Represents a region that has rooms. Could be for example "forest" or "hell".
  /// </summary>
  public abstract class AbstractRegion
  {
    /// <summary>
    ///   The path to the folder that holds the room scenes.
    /// </summary>
    protected string PathToRooms;

    /// <summary>
    ///   Returns a list of all the rooms as strings.
    /// </summary>
    /// <returns>A list of all the rooms as strings.</returns>
    private IList<string> GetRoomNames()
    {
      var rooms = new List<string>();

      var dir = new Directory();
      dir.Open(PathToRooms);
      dir.ListDirBegin();

      while (true)
      {
        var file = dir.GetNext();

        if (file.Empty())
          break;

        if (!file.BeginsWith("."))
          rooms.Add(file);
      }

      dir.ListDirEnd();
      return rooms;
    }

    /// <summary>
    ///   Returns a random room that is in the region.
    /// </summary>
    /// <returns></returns>
    public IRoom GetRandomRoom()
    {
      var roomNames = GetRoomNames();

      int randIndex = (int)(GD.Randi() % (roomNames.Count - 1));
      var roomName = roomNames[randIndex];
      var roomPath = PathToRooms + "/" + roomName;

      return NodeService.InstanceNotNull<IRoom>(roomPath);
    }

    /// <summary>
    ///   Creates and returns a new region that is the next region.
    /// </summary>
    /// <returns>The next region.</returns>
    public abstract AbstractRegion GetNextRegion();
  }
}