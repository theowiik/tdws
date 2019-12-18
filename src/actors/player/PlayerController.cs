using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
using tdws.actors.abstract_actor;
using tdws.actors.player.holster;
using tdws.objects;
using tdws.projectile_shooters;
using tdws.projectile_shooters.abstract_projectile_shooter;
using tdws.utils;
using tdws.utils.state;
using Object = Godot.Object;

namespace tdws.actors.player
{
  /// <summary>
  ///   The Player character.
  /// </summary>
  public sealed class PlayerController : AbstractActor, IMovable, ICanPickup
  {
    [Signal]
    public delegate void ProjectileShooterChanged(AbstractProjectileShooter projectileShooter);

    private AnimationPlayer _animationPlayer;
    private Holster _holster;

    /// <summary>
    ///   Contains a list of tuples where index 0 contains the key scan code. And index 1 contains the corresponding
    ///   inventory index.
    /// </summary>
    private List<Tuple<int, int>> _keyboardIndex;

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

    public void PickupProjectileShooter(IProjectileShooter projectileShooter)
    {
      if (projectileShooter == null) return;
      _holster.Add(projectileShooter);
    }

    public void PickupCoins(int amount)
    {
      Stats.Coins += amount;
    }

    void IMovable.Move(Vector2 velocity)
    {
      _velocity = MoveAndSlide(velocity, Vector2.Zero, false, 4, 0, false);

      for (var i = 0; i < GetSlideCount(); i++)
      {
        var collision = GetSlideCollision(i);
        var collider = collision.Collider;

        var rigidBody = collider as RigidBody2D;
        rigidBody?.ApplyCentralImpulse(-collision.Normal * Inertia);
      }
    }

    /// <summary>
    ///   Emits the projectile shooter changed signal.
    /// </summary>
    private void EmitProjectileShooterChanged()
    {
      // Create a new getter to get something that isn't the interface?
      EmitSignal(nameof(ProjectileShooterChanged), _holster.GetHolding() as Object);
    }

    public override void _Ready()
    {
      _projectileShooterHolder = GetNode("ProjectileShooterHolder") as Node2D;
      _animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
      _holster = GetNode("Holster") as Holster;
      _stateMachine = GetNode("PlayerStateMachine") as StateMachine;
      _stateMachine.Start();
      _keyboardIndex = new List<Tuple<int, int>>
      {
        new Tuple<int, int>((int) KeyList.Key1, 0),
        new Tuple<int, int>((int) KeyList.Key2, 1),
        new Tuple<int, int>((int) KeyList.Key3, 2),
        new Tuple<int, int>((int) KeyList.Key4, 3),
      };
    }

    public override void _Process(float delta)
    {
      HolsterLoop();
      AnimationLoop();
      ShootLoop();

      if (Input.IsActionPressed("debug")) EmitSignal(nameof(CoinDropped), 10);
    }

    /// <summary>
    ///   Checks if the player wants to shoot and/or reload.
    /// </summary>
    private void ShootLoop()
    {
      if (Input.IsActionPressed("shoot"))
      {
        var p = _holster.GetHolding();
        p?.Shoot(this);
      }

      if (Input.IsActionPressed("reload"))
      {
        var p = _holster.GetHolding();
        p?.Reload();
      }
    }

    /// <summary>
    ///   Plays the correct animation.
    /// </summary>
    private void AnimationLoop()
    {
      var direction = DirectionService.VelocityToDirection(_velocity);
      PlayAnimation(direction);

      List<int> hello = new List<int>();
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
      var change = false;

      if (Input.IsActionJustReleased("weapon_next"))
      {
        _holster.NextWeapon();
        change = true;
      }
      else if (Input.IsActionJustReleased("weapon_previous"))
      {
        _holster.PreviousWeapon();
        change = true;
      }
      else
      {
        foreach (var tuple in _keyboardIndex.Where(tuple => Input.IsKeyPressed(tuple.Item1)))
        {
          _holster.Select(tuple.Item2);
          change = true;
        }
      }

      if (change)
      {
        EquipHoldingProjectileShooter();
        EmitProjectileShooterChanged();
      }
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

      if (_holster.GetHolding() is Node holding)
      {
        _projectileShooterHolder.AddChild(holding);
        EmitChatAdded(holding.GetName());
      }
      else
      {
        EmitChatAdded("Unequipped projectile shooter");
      }
    }

    protected override void HandleDamage(IDamageSource damageSource)
    {
      // Does the player need to do something special?
    }
  }
}