using Godot;
using tdws.actors.stats;
using tdws.objects;

namespace tdws.actors.monsters.monster
{
  /// <summary>
  ///   A abstract monster.
  /// </summary>
  public abstract class Monster : KinematicBody2D, IDamageable, IDamageSource
  {
    private PackedScene _deathEffect;

    /// <summary>
    ///   The target destination.
    /// </summary>
    private KinematicBody2D _target;

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

    public int GetDamage()
    {
      return 10;
    }

    private void _on_Area2D_body_entered(object body)
    {
      GD.Print("ENTER!");

      if (body is KinematicBody2D body2D)
        _target = body2D;
    }

    public override void _Ready()
    {
      stats = GetNode("Stats") as Stats;
      _deathEffect = GD.Load("res://src/particles/DeathEffect.tscn") as PackedScene;
    }

    public override void _PhysicsProcess(float delta)
    {
      if (_target == null) return;

      GD.Print("MOVE!!!!");
      var toTarget = GetGlobalPosition().DirectionTo(_target.GetGlobalPosition());
      MoveAndSlide(toTarget.Normalized() * 100);
    }
  }
}