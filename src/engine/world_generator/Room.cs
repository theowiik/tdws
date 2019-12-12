namespace tdws.engine.world_generator
{
  /// <summary>
  ///   A room.
  /// </summary>
  public class Room
  {
    /// <param name="x">
    ///   the x coordinate
    /// </param>
    /// <param name="y">
    ///   the y coordinate
    /// </param>
    public Room(int x, int y)
    {
      X = x;
      Y = y;
    }

    public Room()
    {
      new Room(1, 1);
    }

    public Room RoomAbove { get; set; }
    public Room RoomRight { get; set; }
    public Room RoomDown { get; set; }
    public Room RoomLeft { get; set; }

    private int X { get; }
    private int Y { get; }

    public override string ToString()
    {
      return "hey!";
    }
  }
}