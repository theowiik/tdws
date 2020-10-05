using Godot;

namespace tdws.Scripts.Actors
{
  /// <summary>
  ///   The base class all actors inherit from.
  /// </summary>
  public abstract class AbstractActor : RigidBody2D, IDamageable, IKnockbackable
  {
    [Signal]
    public delegate void ChatAdded(string msg);

    [Signal]
    public delegate void CoinDropped(int amount, Vector2 position);

    [Signal]
    public delegate void CoinsChanged(int coins);

    [Signal]
    public delegate void Died();

    [Signal]
    public delegate void HealthChanged(int hp);

    private AudioStreamPlayer _damagePlayer;
    protected AnimationPlayer AnimationPlayer;
    protected Stats Stats;

    protected AbstractActor()
    {
      Inertia = 10;
      Stats = new Stats(100, 100);
    }

    protected int Inertia { get; }

    public void TakeDamage(IDamageSource damageSource)
    {
      if (Stats.IsDead()) return;

      Stats.TakeDamage(damageSource.GetDamage());
      HandleDamage(damageSource);
      EmitHealthChanged();
      _damagePlayer.Play();

      if (Stats.IsDead()) Die();
    }

    public void Die()
    {
      EmitSignal(nameof(CoinDropped), 3, GlobalPosition);
      EmitSignal(nameof(Died));
      QueueFree();
    }

    public override void _Ready()
    {
      AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
      _damagePlayer = GetNode<AudioStreamPlayer>("DamagePlayer");
      GetNodes();
    }

    /// <summary>
    ///   Retrieve child nodes for specific implementations of a actor.
    /// </summary>
    protected abstract void GetNodes();

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
    public void EmitHealthChanged()
    {
      EmitSignal(nameof(HealthChanged), Stats.Hp);
    }

    /// <summary>
    ///   Plays the appropriate animation for the actor given a direction enum.
    /// </summary>
    /// <param name="direction">
    ///   The direction the actor is facing.
    /// </param>
    protected void PlayAnimation(Directions direction)
    {
      switch (direction)
      {
        case Directions.Up:
          AnimationPlayer.Play("walk_up");
          break;
        case Directions.Right:
          AnimationPlayer.Play("walk_right");
          break;
        case Directions.Down:
          AnimationPlayer.Play("walk_down");
          break;
        case Directions.Left:
          AnimationPlayer.Play("walk_left");
          break;
        default:
          AnimationPlayer.Play("idle_down");
          break;
      }
    }

    public int GetHealth()
    {
      return Stats.Hp;
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

    public virtual void Knockback(Vector2 vector)
    {
      ApplyCentralImpulse(vector);
    }
  }
}
