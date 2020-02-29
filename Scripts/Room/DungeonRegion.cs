namespace tdws.Scripts.Room
{
  public class DungeonRegion : Region
  {
    public DungeonRegion()
    {
      PathToRooms = "res://Scenes/Rooms/Dungeons";
    }

    public override Region GetNextRegion()
    {
      return new DungeonRegion();
    }
  }
}