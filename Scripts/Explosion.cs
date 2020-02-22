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

      if (body is RigidBody2D physicsBody2D)
      {
        var dirToBody = GlobalPosition.DirectionTo(physicsBody2D.GlobalPosition).Normalized();
        physicsBody2D.ApplyImpulse(Vector2.Zero, dirToBody * 400);
      }
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