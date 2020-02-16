using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using tdws.Scripts.ProjectileShooters;
using tdws.Scripts.Services;
using Object = Godot.Object;

namespace tdws.Scripts.Actors
{
  /// <summary>
  ///   The Player character.
  /// </summary>
  public sealed class Player : AbstractActor, ICanPickup
  {
    [Signal]
    public delegate void ProjectileShooterChanged(AbstractProjectileShooter projectileShooter);

    private const int MaxWalkSpeed = 125;
    private const int MaxSprintSpeed = 175;

    /// <summary>
    ///   Contains a list of tuples where index 0 contains the key scan code. And index 1 contains the corresponding
    ///   inventory index.
    /// </summary>
    private readonly List<Tuple<int, int>> _keyboardIndex;

    private readonly Holster _holster;

    /// <summary>
    ///   The node that projectiles should be attached to.
    /// </summary>
    private Position2D _projectileShooterHolder;

    private Vector2 _velocity;

    public Player()
    {
      _keyboardIndex = new List<Tuple<int, int>>
      {
        new Tuple<int, int>((int) KeyList.Key1, 0),
        new Tuple<int, int>((int) KeyList.Key2, 1),
        new Tuple<int, int>((int) KeyList.Key3, 2),
        new Tuple<int, int>((int) KeyList.Key4, 3)
      };

      _holster = new Holster();
    }

    public void PickupProjectileShooter(IProjectileShooter projectileShooter)
    {
      if (projectileShooter == null) return;
      _holster.Add(projectileShooter);
      EmitChatAdded("Picked up " + projectileShooter.GetProjectileShooterName());
    }

    public void PickupCoins(int amount)
    {
      Stats.Coins += amount;
      EmitSignal(nameof(CoinsChanged), Stats.Coins);
    }

    private void MoveLoop()
    {
      var inputDirection = GetMovementInputVector();
      var speed = Input.IsActionPressed("sprint") ? MaxSprintSpeed : MaxWalkSpeed;
      _velocity = inputDirection * speed;

      // Move
      MoveAndSlide(_velocity, Vector2.Zero, false, 4, 0, false);

      GD.Print(_velocity);

      // Collisions
      for (var i = 0; i < GetSlideCount(); i++)
      {
        var collision = GetSlideCollision(i);
        var collider = collision.Collider;

        var rigidBody = collider as RigidBody2D;
        rigidBody?.ApplyCentralImpulse(-collision.Normal * Inertia);
      }
    }

    /// <summary>
    ///   Returns the unit vector of the input direction from the user.
    /// </summary>
    /// <returns>
    ///   The unit vector of the input direction.
    /// </returns>
    private static Vector2 GetMovementInputVector()
    {
      const int component = 1;
      var inputVector = new Vector2();

      if (Input.IsActionPressed("up")) inputVector.y -= component;
      if (Input.IsActionPressed("down")) inputVector.y += component;
      if (Input.IsActionPressed("right")) inputVector.x += component;
      if (Input.IsActionPressed("left")) inputVector.x -= component;

      return inputVector.Normalized();
    }

    /// <summary>
    ///   Emits the projectile shooter changed signal.
    /// </summary>
    private void EmitProjectileShooterChanged()
    {
      // Create a new getter to get something that isn't the interface?
      EmitSignal(nameof(ProjectileShooterChanged), _holster.GetHolding() as Object);
    }

    protected override void GetNodes()
    {
      _projectileShooterHolder = GetNode<Position2D>("ProjectileShooterHolder");
    }

    public override void _Process(float delta)
    {
      MoveLoop();
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
        EmitChatAdded(holding.Name);
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
