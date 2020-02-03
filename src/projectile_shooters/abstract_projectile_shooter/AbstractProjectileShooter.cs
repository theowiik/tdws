using System;
using Godot;
using tdws.actors.abstract_actor;
using tdws.objects.projectiles;
using tdws.objects.projectiles.abstract_projectile;

namespace tdws.projectile_shooters.abstract_projectile_shooter
{
  /// <summary>
  ///   The ProjectileShooter class represents something that can shoot.
  /// </summary>
  public abstract class AbstractProjectileShooter : Sprite, IProjectileShooter
  {
    [Signal]
    public delegate void ProjectileAdded(AbstractProjectile projectile);

    private Position2D _output;
    private AudioStreamPlayer _shootPlayer;
    private Timer _timer;
    protected int Ammo;
    protected int KnockbackForce;
    protected int MagSize;

    /// <summary>
    ///   The maximum offset the projectiles will have in degrees.
    /// </summary>
    protected double MaxOffsetAngle;

    protected PackedScene Projectile;
    protected string ProjectileShooterName;
    protected int ProjectilesPerShot;
    protected float SecondsBetweenShots;

    public void Reload()
    {
      Ammo = MagSize; // infinite ammo atm.
      PrintAmmo();
    }

    public string GetProjectileShooterName()
    {
      return "test";
    }

    public void AppendProjectiles(AbstractActor actor = null)
    {
      for (var i = 0; i < ProjectilesPerShot; i++)
        if (Projectile.Instance() is AbstractProjectile projectile)
        {
          EmitSignal(nameof(ProjectileAdded), projectile);
          projectile.GlobalPosition = _output.GlobalPosition;
          projectile.Direction = GetTrajectoryVector();
          projectile.ActorSource = actor;
        }
    }

    public void Shoot(AbstractActor actorSource = null)
    {
      if (CanShoot())
      {
        AppendProjectiles(actorSource);
        KnockbackHolder();
        StartShootDelay();
        Ammo--;
        PrintAmmo();
        _shootPlayer.Play();
      }
    }

    /// <summary>
    ///   Knockbacks the holder.
    /// </summary>
    private void KnockbackHolder()
    {
      // Cheat fix :)
      // TODO: Implement in a better way.
      if (GetParent().GetParent() is KinematicBody2D owner)
        owner.MoveAndSlide(-ToMouseVec() * KnockbackForce);
    }

    /// <summary>
    ///   Starts the timer with SecondsBetweenShots seconds.
    /// </summary>
    private void StartShootDelay()
    {
      _timer.Start(SecondsBetweenShots);
    }

    /// <summary>
    ///   Checks if the projectile shooter can shoot.
    /// </summary>
    private bool CanShoot()
    {
      var timerIsStopped = _timer?.IsStopped() ?? false;
      return timerIsStopped && Ammo > 0;
    }

    /// <summary>
    ///   Creates and returns a vector pointing towards the mouse from the projectile shooter with a random offset.
    /// </summary>
    /// <returns>
    ///   A vector pointing towards the mouse from the projectile shooter with a random offset.
    /// </returns>
    private Vector2 GetTrajectoryVector()
    {
      var offsetAngle = (float) (MaxOffsetAngle * GD.RandRange(-1, 1));
      return ToMouseVec().Rotated(Mathf.Deg2Rad(offsetAngle));
    }

    public override void _Ready()
    {
      _output = GetNode("Output") as Position2D;
      _shootPlayer = GetNode("ShootPlayer") as AudioStreamPlayer;
      InitStandardValues();
      OverrideProperties();
    }

    /// <summary>
    ///   Sets all instance variables to standard values.
    /// </summary>
    private void InitStandardValues()
    {
      Projectile = ProjectileFactory.CreateBullet();
      _timer = GetNode("Timer") as Timer;
      _timer.Autostart = false;
      _timer.OneShot = true;
      SecondsBetweenShots = 0.4f;
      MagSize = 20;
      Ammo = MagSize;
      ProjectilesPerShot = 8;
      ProjectileShooterName = "Abstract Projectile Shooter";
      MaxOffsetAngle = 3;
      KnockbackForce = 50;
    }

    /// <summary>
    ///   Override specific properties of a projectile shooter, such as the mag size and damage.
    /// </summary>
    protected abstract void OverrideProperties();

    public override void _Process(float delta)
    {
      RotationLoop();
    }

    private void PrintAmmo()
    {
//      GD.Print(Ammo + "/" + MagSize);
    }

    /// <summary>
    ///   Rotates the projectile shooter to look towards the mouse.
    /// </summary>
    private void RotationLoop()
    {
      var radians = ToMouseVec().Angle();
      SetGlobalRotation(radians);

      var firstQuadrant = radians >= -Math.PI / 2 && radians <= 0;
      var secondQuadrant = radians <= Math.PI / 2 && radians >= 0;
      var thirdQuadrant = radians >= Math.PI / 2 && radians <= Math.PI;
      var fourthQuadrant = radians >= -Math.PI && radians <= -Math.PI / 2;

      FlipV = thirdQuadrant || fourthQuadrant;
    }

    /// <summary>
    ///   Returns the direction vector from the projectile shooter to the mouse.
    ///   Returns a null vector if the global mouse position is null.
    /// </summary>
    /// <returns>
    ///   The direction vector from the projectile shooter to the mouse.
    /// </returns>
    private Vector2 ToMouseVec()
    {
      var mousePos = GetGlobalMousePosition();

      return new Vector2(
        mousePos.x - GlobalPosition.x,
        mousePos.y - GlobalPosition.y
      ).Normalized();
    }
  }
}
