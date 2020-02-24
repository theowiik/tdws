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
    private const int PushForce = 500;
    private bool _hasExploded;
    private Area2D _explosionArea;
    private bool _hasEnteredTree;
    private int _frames;
    private Timer _lifetimeTimer;

    public Explosion()
    {
      _hasExploded = false;
      _hasEnteredTree = false;
    }

    public void OnLifetimeTimerTimeout()
    {
      QueueFree();
    }

    public override void _Ready()
    {
      _explosionArea = GetNode<Area2D>("ExplosionArea");
      _lifetimeTimer = GetNode<Timer>("LifetimeTimer");
    }

    public override void _EnterTree()
    {
      _hasEnteredTree = true;
    }

    public override void _Process(float delta)
    {
      if (_hasExploded) return;

      // The area2d does not detect overlapping bodies at first frame.
      // TODO: Fix
      if (_frames < 1)
      {
        _frames += 1;
        return;
      }

      foreach (RigidBody2D rigidBody in _explosionArea.GetOverlappingBodies())
        Explode(rigidBody);

      _hasExploded = true;
    }

    /// <summary>
    ///   Damages the body if it is damagable. Pushes the object back.
    /// </summary>
    /// <param name="rigidBody">The body to explode.</param>
    private void Explode(RigidBody2D body)
    {
      GD.Print("exploding!");

      if (body is IDamageable damageable)
      {
        GD.Print("something took damage from the explosion");
        damageable.TakeDamage(this);
      }

      if (body is RigidBody2D physicsBody2D)
      {
        var dirToBody = GlobalPosition.DirectionTo(physicsBody2D.GlobalPosition).Normalized();
        physicsBody2D.ApplyImpulse(Vector2.Zero, dirToBody * PushForce);
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