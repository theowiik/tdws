using System;
using System.Collections.Generic;
using tdws.utils;

namespace tdws.engine.world_generator
{
  /// <summary>
  ///   Util methods for generating dungeons.
  /// </summary>
  public static class WorldGenerator
  {
    /// <summary>
    ///   Generates a dungeon.
    /// </summary>
    public static void GenerateWorld()
    {
      var rootRoom = new Room(0, 0);
      
    }

    private static void PrintDungeon(IEnumerable<IEnumerable<Room>> rooms)
    {
      foreach (var room in rooms)
      {
        Console.Write(room.ToString());
      }
    }
  }
}