namespace tdws.Scripts.Room
{
  public class DungeonRegion : AbstractRegion
  {
    public DungeonRegion()
    {
      PathToRooms = "res://Scenes/Rooms/Dungeons";
    }

    public override AbstractRegion GetNextRegion()
    {
      return new DungeonRegion();
    }
  }
}