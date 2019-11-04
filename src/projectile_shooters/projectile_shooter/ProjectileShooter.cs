using System;
using Godot;
using tdws.objects.projectile;

namespace tdws.projectile_shooters.projectile_shooter
{
  /// <summary>
  ///   The ProjectileShooter class represents something that can shoot.
  ///   TODO make projectile shooter abstract?
  /// </summary>
  public class ProjectileShooter : Sprite, IProjectileShooter
  {
    private int _ammo;
    private bool _canShoot;
    private int _magSize;

    /// <summary>
    ///   The maximum offset the projectiles will have in degrees.
    /// </summary>
    private double _maxOffsetAngle;

    private string _name;
    private PackedScene _projectile;

    private int _projectilesPerShot;
    private float _secondsBetweenShots;
    private Timer _timer;

    public void Reload()
    {
      GD.Print("reloading.");
    }

    public void AppendProjectile()
    {
      for (var i = 0; i < _projectilesPerShot; i++)
      {
        if (!(_projectile.Instance() is Projectile proj))
          continue;

        GetParent().AddChild(proj);
        proj.SetPosition(Transform.origin);
        proj.SetDirection(GetTrajectoryVector());
      }
    }

    public void Shoot()
    {
      _canShoot = false;
      AppendProjectile();
      _timer.Start(_secondsBetweenShots);
    }

    /// <summary>
    ///   Creates and returns a vector pointing towards the mouse from the projectile shooter with a random offset.
    /// </summary>
    /// <returns>
    ///   A vector pointing towards the mouse from the projectile shooter with a random offset.
    /// </returns>
    private Vector2 GetTrajectoryVector()
    {
      var offsetAngle = (float) (_maxOffsetAngle * GD.RandRange(-1, 1));
      return ToMouseVec().Rotated(Mathf.Deg2Rad(offsetAngle));
    }


    public override void _Ready()
    {
      _projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
      _timer = GetNode("Timer") as Timer;
      _secondsBetweenShots = 0.4f;
      _canShoot = true;
      _magSize = 20;
      _ammo = 300;
      _projectilesPerShot = 8;
      _name = "Base Projectile Shooter";
      _maxOffsetAngle = 3;
    }

    public override void _Process(float delta)
    {
      if (Input.IsActionPressed("shoot") && _canShoot) Shoot();
    }

    public void _on_Timer_timeout()
    {
      _canShoot = true;
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
        mousePos.x - GetGlobalPosition().x,
        mousePos.y - GetGlobalPosition().y
      ).Normalized();
    }
  }
}