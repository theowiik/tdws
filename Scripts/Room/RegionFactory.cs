namespace tdws.Scripts.Room
{
  public static class RegionFactory
  {
    private const string PathToRooms = "res://Scenes/Rooms";

    public static Region CreateDungeon()
    {
      return new Region(PathToRooms + "/Dungeons", RegionFactory.CreateForest);
    }

    public static Region CreateForest()
    {
      return new Region(PathToRooms + "/Forest", RegionFactory.CreateForest);
    }
  }
}