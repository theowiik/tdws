using Godot;
using tdws.objects;

namespace tdws.actors.abstract_actor
{
  /// <summary>
  ///   The base class all actors inherit from.
  /// </summary>
  public abstract class AbstractActor : KinematicBody2D, IDamageable
  {
    private readonly PackedScene _deathEffect;
    protected Stats Stats;

    protected AbstractActor()
    {
      Inertia = 10;
      Stats = new Stats(100, 100);
      _deathEffect = GD.Load("res://src/particles/DeathEffect.tscn") as PackedScene;
    }

    protected int Inertia { get; }

    public void TakeDamage(IDamageSource damageSource)
    {
      Stats.TakeDamage(damageSource.GetDamage());

      if (Stats.IsDead()) Die();
    }

    public void Die()
    {
      QueueFree();

      if (!(_deathEffect.Instance() is Particles2D particles))
        return;

      particles.SetGlobalPosition(GetGlobalPosition());
      GetParent().AddChild(particles);
    }
  }
}