using Godot;
using tdws.actors.stats;
using tdws.objects;

namespace tdws.actors.monsters.monster
{
  /// <summary>
  ///   A abstract monster.
  /// </summary>
  public abstract class Monster : KinematicBody2D, IDamagable
  {
    protected Stats stats;

    public void TakeDamage(IDamageSource damageSource)
    {
      GD.Print("oof! Took " + damageSource.GetDamage() + " damage!");
    }

    public override void _Ready()
    {
      stats = GetNode("Stats") as Stats;
    }
  }
}