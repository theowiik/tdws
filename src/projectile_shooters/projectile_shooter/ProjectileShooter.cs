using Godot;
using System;

/// <summary>
/// The ProjectileShooter class represents something that can shoot.
///
/// TODO make projectile shooter abstract?
/// </summary>
public class ProjectileShooter : Sprite, IProjectileShooter
{
  private PackedScene _projectile;
  private int _magSize;
  private int _ammo;
  private float _secondsBetweenShots;
  private bool _canShoot;
  private Timer _timer;
  private string _name;

  public override void _Ready()
  {
    _projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
    _timer = GetNode("Timer") as Timer;
    _secondsBetweenShots = 0.2f;
    _canShoot = true;
    _magSize = 20;
    _ammo = 300;
    _name = "Assault Rifle";
  }

  public override void _Process(float delta)
  {
    if (Input.IsActionPressed("shoot") && _canShoot)
    {
      Shoot();
    }
  }

  public void _on_Timer_timeout()
  {
    _canShoot = true;
  }

  public void Reload()
  {
    GD.Print("reloading.");
  }

  public void AppendProjectile()
  {
    Projectile proj = _projectile.Instance() as Projectile;
    GetParent().AddChild(proj);
    proj.SetPosition(Transform.origin);
    proj.SetDirection(ToMouseVec());
  }

  public void Shoot()
  {
    _canShoot = false;
    AppendProjectile();
    _timer.Start(_secondsBetweenShots);
  }

  /// <summary>
  /// Returns the direction vector from the projectile shooter to the mouse.
  /// Returns a null vector if the global mouse position is null.
  /// </summary>
  ///
  /// <returns>
  /// The direction vector from the projectile shooter to the mouse.
  /// </returns>
  private Vector2 ToMouseVec()
  {
    Vector2 mousePos = GetGlobalMousePosition();
    if (mousePos == null) return new Vector2();

    return new Vector2(
      mousePos.x - GetGlobalPosition().x,
      mousePos.y - GetGlobalPosition().y
    ).Normalized();
  }
}
