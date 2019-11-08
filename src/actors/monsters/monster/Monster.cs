using Godot;
using tdws.actors.stats;
using tdws.objects;

namespace tdws.actors.monsters.monster
{
  /// <summary>
  ///   A abstract monster.
  /// </summary>
  public abstract class Monster : KinematicBody2D, IDamageable
  {
    protected Stats stats;

    public void TakeDamage(IDamageSource damageSource)
    {
      GD.Print("oof! Took " + damageSource.GetDamage() + " damage!");

      stats.TakeDamage(damageSource.GetDamage());

      if (stats.IsDead()) Die();
    }

    public void Die()
    {
      QueueFree();
      
      if (!(_deathEffect.Instance() is Particles2D particles))
        return;
      
      particles.SetGlobalPosition(GetGlobalPosition());
      GetParent().AddChild(particles);
    }

    public override void _Ready()
    {
      stats = GetNode("Stats") as Stats;
    }
  }
}