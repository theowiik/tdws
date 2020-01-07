using System.Collections.Generic;
using System.Text;
using Godot;
using tdws.objects.door;

namespace tdws.engine.world_generator
{
  /// <summary>
  ///   Util methods for generating dungeons.
  /// </summary>
  public class WorldGenerator
  {
    public WorldGenerator()
    {
      SpawnPoint = new Vector2();
    }

    public Vector2 SpawnPoint { get; private set; }

    /// <summary>
    ///   Generates a dungeon.
    /// </summary>
    /// <returns>
    ///   Returns the dungeon as a matrix.
    /// </returns>
    public IEnumerable<IEnumerable<Room>> GenerateWorld()
    {
      var dungeon = new List<List<Room>>();

      var firstRow = new List<Room> {new Room(), null, new Room(), null, null};
      var secondRow = new List<Room> {new Room(), new Room(true)};
      var thirdRow = new List<Room> {null, null, new Room(), new Room()};

      dungeon.Add(firstRow);
      dungeon.Add(secondRow);
      dungeon.Add(thirdRow);

      return dungeon;
    }

    /// <summary>
    ///   Creates a dungeon by converting a matrix of rooms to actual
    ///   tile map rooms and adds them as children to the parent.
    /// </summary>
    /// <param name="world">
    ///   The matrix of rooms.
    /// </param>
    /// <param name="parent">
    ///   The node that contains the dungeon.
    /// </param>
    public void WorldToScene(IEnumerable<IEnumerable<Room>> world, Node2D parent)
    {
      var baseRoom = GD.Load("res://src/levels/Room.tscn") as PackedScene;

      const int width = 14;
      const int height = 11;
      const int tileSizeWidth = 16;
      var rowIndex = 0;

      foreach (var row in world)
      {
        var colIndex = 0;

        foreach (var room in row)
        {
          AddRoom(room, baseRoom, parent, colIndex, rowIndex, tileSizeWidth, width, height);
          colIndex++;
        }

        rowIndex++;
      }
    }

    /// <summary>Adds a room.</summary>
    /// <param name="room">The room to add</param>
    /// <param name="baseRoom">The scene of the room to add.</param>
    /// <param name="parent">The parent to add the rooms to.</param>
    /// <param name="colIndex">The column index.</param>
    /// <param name="rowIndex">The row index.</param>
    /// <param name="width">The width of the room in tiles.</param>
    /// <param name="height">The height of the room in tiles.</param>
    /// <param name="tileSizeWidth">The width of one tile in pixels.</param>
    private void AddRoom(
      Room room,
      PackedScene baseRoom,
      Node parent,
      int colIndex,
      int rowIndex,
      int tileSizeWidth,
      int width, int height)
    {
      if (room == null) return;

      var x = colIndex * tileSizeWidth * width;
      var y = rowIndex * tileSizeWidth * height;

      var tileMap = baseRoom.Instance() as TileMap;
      AddDoorsToRoomScene(tileMap);
      tileMap.SetGlobalPosition(new Vector2(x, y));
      parent.AddChild(tileMap);
      parent.MoveChild(tileMap, 0);

      // Set spawn point
      if (room.Spawn) SpawnPoint = new Vector2(x, y);
    }

    /// <summary>
    ///   Adds doors to the room.
    ///   TODO: Place the doors in a sensible way.
    /// </summary>
    /// <param name="room">
    ///   The room to add doors in.
    /// </param>
    private void AddDoorsToRoomScene(Node room)
    {
      var doorScene = GD.Load("res://src/objects/door/Door.tscn") as PackedScene;
      var doorInstance = doorScene.Instance() as Door;
      room.AddChild(doorInstance);
      doorInstance.SetPosition(new Vector2(30, 30));
    }

    public void PrintDungeon(IEnumerable<IEnumerable<Room>> rooms)
    {
      foreach (var row in rooms)
      {
        var sb = new StringBuilder();

        foreach (var room in row)
        {
          // X represents a room. O represents null.
          var x = room == null ? "O " : "X ";
          sb.Append(x);
        }

        GD.Print(sb);
      }
    }
  }
}