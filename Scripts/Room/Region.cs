using System.Collections.Generic;
using Godot;
using tdws.Scripts.Services;
using System;

namespace tdws.Scripts.Room
{
  /// <summary>
  ///   Represents a region that has rooms. Could be for example "forest" or "hell".
  /// </summary>
  public sealed class Region
  {
    /// <summary>
    ///   The path to the folder that holds the room scenes.
    /// </summary>
    private string PathToRooms;

    private Func<Region> _regionChooser;

    private Region(string pathToRooms, Func<Region> regionChooser)
    {
      PathToRooms = Objects.RequireNonNull(pathToRooms);
      _regionChooser = Objects.RequireNonNull(regionChooser);
    }

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

      int randIndex = (int)(GD.Randi() % roomNames.Count);
      var roomName = roomNames[randIndex];
      var roomPath = PathToRooms + "/" + roomName;

      return NodeService.InstanceNotNull<IRoom>(roomPath);
    }

    /// <summary>
    ///   Creates and returns a new region that is the next region.
    /// </summary>
    /// <returns>The next region.</returns>
    public Region GetNextRegion()
    {
      return _regionChooser();
    }

    #region Factory
    public static class Factory
    {
      private const string PathToRooms = "res://Scenes/Rooms";

      public static Region CreateStartDungeon()
      {
        return CreateDungeon();
      }

      public static Region CreateDungeon()
      {
        return new Region(PathToRooms + "/Dungeons", Region.Factory.CreateForest);
      }

      public static Region CreateForest()
      {
        return new Region(PathToRooms + "/Forest", Region.Factory.CreateForest);
      }
    }
    #endregion
  }
}