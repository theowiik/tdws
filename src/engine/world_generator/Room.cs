namespace tdws.engine.world_generator
{
  /// <summary>
  ///   A room.
  /// </summary>
  public class Room
  {
    public Room(bool spawn = false)
    {
      Spawn = spawn;
    }

    public bool Spawn { get; }
  }
}