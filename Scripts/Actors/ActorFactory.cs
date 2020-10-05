using tdws.Scripts.Services;

namespace tdws.Scripts.Actors
{
  /// <summary>
  ///   Creates monsters
  /// </summary>
  public static class ActorFactory
  {
    /// <summary>
    ///   Creates and returns a skeleton.
    /// </summary>
    /// <returns>A skeleton.</returns>
    public static AbstractEnemy CreateSkeleton()
    {
      return NodeService.InstanceNotNull<AbstractEnemy>("res://Scenes/Actors/Skeleton.tscn");
    }

    public static Player CreatePlayer()
    {
      return NodeService.InstanceNotNull<Player>("res://Scenes/Actors/Player.tscn");
    }
  }
}