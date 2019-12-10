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
    public delegate void ProjectileAdded(Node projectile);

    private Position2D _output;
    private Timer _timer;
    protected int Ammo;
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
    }

    public void AppendProjectiles(AbstractActor actor = null)
    {
      for (var i = 0; i < ProjectilesPerShot; i++)
        if (Projectile.Instance() is AbstractProjectile proj)
        {
          GetParent().GetParent().GetParent().AddChild(proj);
          proj.SetPosition(_output.GlobalPosition);
          proj.SetDirection(GetTrajectoryVector());
          proj.ActorSource = actor;
//          EmitSignal(nameof(ProjectileAdded), proj);
        }
    }

    public void Shoot(AbstractActor actorSource)
    {
      if (CanShoot())
      {
        AppendProjectiles(actorSource);
        _timer.Start(SecondsBetweenShots);
      }
    }

    public void Shoot()
    {
      Shoot(null);
    }

    /// <summary>
    ///   Checks if the projectile shooter can shoot.
    /// </summary>
    private bool CanShoot()
    {
      return _timer?.IsStopped() ?? false;
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
      Ammo = 300;
      ProjectilesPerShot = 8;
      ProjectileShooterName = "Abstract Projectile Shooter";
      MaxOffsetAngle = 3;
    }

    /// <summary>
    ///   Override specific properties of a projectile shooter, such as the mag size and damage.
    /// </summary>
    protected abstract void OverrideProperties();

    public override void _Process(float delta)
    {
      RotationLoop();
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
        mousePos.x - _output.GlobalPosition.x,
        mousePos.y - _output.GlobalPosition.y
      ).Normalized();
    }
  }
}