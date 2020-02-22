using Godot;
using tdws.Scripts.Actors;

namespace tdws.Scripts
{
  /// <summary>
  ///   A explosion that damages actors that are in the explosions range.
  /// </summary>
  public class Explosion : Node2D, IDamageSource
  {
    private const int Damage = 10;

    public void OnExplosionAreaEntered(object body)
    {
      if (body is IDamageable damageable)
        damageable.TakeDamage(this);
    }

    public int GetDamage()
    {
      return Damage;
    }

    public AbstractActor GetActorSource()
    {
      return null;
    }

    public bool HasActorSource()
    {
      return false;
    }
  }
}