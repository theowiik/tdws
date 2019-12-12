using System.Media;
using Godot;
using tdws.objects;

namespace tdws.actors.abstract_actor
{
  /// <summary>
  ///   The base class all actors inherit from.
  /// </summary>
  public abstract class AbstractActor : KinematicBody2D, IDamageable
  {
    [Signal]
    public delegate void ChatAdded(string msg);

    [Signal]
    public delegate void HealthChanged(int hp);

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
      HandleDamage(damageSource);
      EmitHealthChanged();

      if (Stats.IsDead()) Die();
    }

    public void Die()
    {
//      QueueFree();

      if (_deathEffect.Instance() is Particles2D particles)
      {
        particles.SetGlobalPosition(GetGlobalPosition());
        GetParent().AddChild(particles);
      }
    }

    /// <summary>
    ///   Handle class specific damage.
    ///   TODO: How to avoid using this?
    /// </summary>
    /// <param name="damageSource">
    ///   The damage source.
    /// </param>
    protected abstract void HandleDamage(IDamageSource damageSource);

    /// <summary>
    ///   Emits the HealthChanged signal.
    /// </summary>
    private void EmitHealthChanged()
    {
      EmitSignal(nameof(HealthChanged), Stats.Hp);
    }

    /// <summary>
    ///   Emits the ChatAdded signal. Does nothing if the provided message is null.
    /// </summary>
    /// <param name="msg">
    ///   The message to print.
    /// </param>
    protected void EmitChatAdded(string msg)
    {
      if (msg == null) return;

      EmitSignal(nameof(ChatAdded), msg);
    }
  }
}