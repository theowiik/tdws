using System;
using Godot;
using tdws.utils;
using tdws.utils.state;

namespace tdws.actors.player
{
  /// <summary>
  ///   The Player character.
  /// </summary>
  public sealed class PlayerController : AbstractActor, IMovable
  {
    private AnimationPlayer _animationPlayer;
    private Holster _holster;

    /// <summary>
    ///   The node that projectiles should be attached to.
    /// </summary>
    private Node2D _projectileShooterHolder;

    private StateMachine _stateMachine;

    /// <summary>
    ///   The latest velocity the player had.
    ///   Used for playing the correct animation.
    /// </summary>
    private Vector2 _velocity;

    void IMovable.Move(Vector2 velocity)
    {
      _velocity = MoveAndSlide(velocity);
    }

    public override void _Ready()
    {
      _projectileShooterHolder = GetNode("ProjectileShooterHolder") as Node2D;
      _animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
      _holster = GetNode("Holster") as Holster;
      _stateMachine = GetNode("PlayerStateMachine") as StateMachine;
      _stateMachine.Start();
    }

    public override void _Process(float delta)
    {
      HolsterLoop();
      AnimationLoop();
    }

    /// <summary>
    ///   Plays the correct animation.
    /// </summary>
    private void AnimationLoop()
    {
      var direction = DirectionService.VelocityToDirection(_velocity);
      PlayAnimation(direction);
    }

    /// <summary>
    ///   Plays the appropriate animation for the player given a direction enum.
    /// </summary>
    /// <param name="direction">
    ///   The direction the player is facing.
    /// </param>
    private void PlayAnimation(Directions direction)
    {
      switch (direction)
      {
        case Directions.Up:
          _animationPlayer.Play("walk_up");
          break;
        case Directions.Right:
          _animationPlayer.Play("walk_right");
          break;
        case Directions.Down:
          _animationPlayer.Play("walk_down");
          break;
        case Directions.Left:
          _animationPlayer.Play("walk_left");
          break;
        default:
          _animationPlayer.Play("idle_down");
          break;
      }
    }

    /// <summary>
    ///   Checks if there was a projectile shooter switch and equips the new projectile shooter.
    /// </summary>
    private void HolsterLoop()
    {
      var next = Input.IsActionJustReleased("weapon_next");
      var previous = Input.IsActionJustReleased("weapon_previous");

      if (next) _holster.NextWeapon();
      if (previous) _holster.PreviousWeapon();

      if (next || previous) EquipHoldingProjectileShooter();
    }

    /// <summary>
    ///   Removes all child nodes from the _projectileShooterHolder node.
    /// </summary>
    private void UnequipProjectileShooters()
    {
      foreach (Node child in _projectileShooterHolder.GetChildren())
        _projectileShooterHolder.RemoveChild(child);
    }

    /// <summary>
    ///   Unequips all projectile shooters and equips the projectile shooter that is currently held.
    /// </summary>
    private void EquipHoldingProjectileShooter()
    {
      UnequipProjectileShooters();
      var holding = _holster.GetHolding() as Node;
      _projectileShooterHolder.AddChild(holding);
    }
  }
}