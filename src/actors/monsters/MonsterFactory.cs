using Godot;
using tdws.actors.monsters.abstract_monster;

namespace tdws.actors.monsters
{
  /// <summary>
  ///   Creates monsters
  /// </summary>
  public sealed class MonsterFactory
  {
    /// <summary>
    ///   Creates and returns a skeleton.
    /// </summary>
    /// <returns>
    ///   A skeleton.
    /// </returns>
    public static AbstractMonster CreateSkeleton()
    {
      var packedScene = GD.Load("res://src/actors/monsters/skeleton/Skeleton.tscn") as PackedScene;
      var skeleton = packedScene.Instance() as AbstractMonster;
      return skeleton;
    }
  }
}