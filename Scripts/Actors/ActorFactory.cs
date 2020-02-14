using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   Creates monsters
  /// </summary>
  public static class ActorFactory
  {
    /// <summary>
    ///   Creates and returns a skeleton.
    /// </summary>
    /// <returns>
    ///   A skeleton.
    /// </returns>
    public static AbstractEnemy CreateSkeleton()
    {
      var packedScene = GD.Load<AbstractEnemy>("res://src/actors/monsters/skeleton/Skeleton.tscn");
      var skeleton = packedScene.Instance() as AbstractEnemy;
      return skeleton;
    }
  }
}