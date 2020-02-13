using System.Collections.Generic;
namespace tdws.levels
{
  /// <summary>
  ///   A game level. Like "Dungeon", "Forest" or "Hell".
  /// </summary>
  public interface ILevel
  {
    /// <summary>
    ///   Returns a list of all the possible rooms a for a level.
    /// </summary>
    /// <returns></returns>
    IList<IRoom> GetPossibleRooms();
  }
}