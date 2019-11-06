using Godot;
using tdws.actors.stats;

namespace tdws.actors.monsters.monster
{
  /// <summary>
  ///   A abstract monster.
  /// </summary>
  public abstract class Monster : KinematicBody2D
  {
    protected Stats stats;

    public override void _Ready()
    {
      stats = GetNode("Stats") as Stats;
    }
  }
}