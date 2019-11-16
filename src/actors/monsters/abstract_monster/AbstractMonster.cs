using Godot;
using tdws.actors.abstract_actor;
using tdws.objects;

namespace tdws.actors.monsters.abstract_monster
{
  public abstract class AbstractMonster : AbstractActor, IDamageable, IDamageSource
  {
    private PackedScene _deathEffect;

    /// <summary>
    ///   The target destination.
    /// </summary>
    private KinematicBody2D _target;

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

    public int GetDamage()
    {
      return 10;
    }

    /// <summary>
    ///   Gets called when a body enters the detection area.
    /// </summary>
    /// <param name="body">
    ///   The body that entered the area.
    /// </param>
    private void OnDetectionAreaExited(object body)
    {
      if (body == _target)
        _target = null;
    }

    /// <summary>
    ///   Gets called when a body exits the detection area.
    /// </summary>
    /// <param name="body">
    ///   The body that exited the area.
    /// </param>
    private void OnDetectionAreaEntered(object body)
    {
      if (body is KinematicBody2D body2D)
        _target = body2D;
    }

    public override void _Ready()
    {
      _deathEffect = GD.Load("res://src/particles/DeathEffect.tscn") as PackedScene;
    }

    public override void _PhysicsProcess(float delta)
    {
      if (_target == null) return;

      var toTarget = GetGlobalPosition().DirectionTo(_target.GetGlobalPosition());
      MoveAndSlide(toTarget.Normalized() * 100);
    }
  }
}
