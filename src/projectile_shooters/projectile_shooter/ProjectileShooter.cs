using Godot;
using System;

/// <summary>
/// The ProjectileShooter class represents something that can shoot.
/// </summary>
public class ProjectileShooter : Sprite, IProjectileShooter
{
  private PackedScene projectile;
  private int magSize;
  private int ammo;
  private float secBetweenShots;
  private bool canShoot;
  private Timer timer;

  public override void _Ready()
  {
    projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
    timer = GetNode("Timer") as Timer;
    secBetweenShots = 0.2f;
    canShoot = true;
    magSize = 20;
    ammo = 300;
  }

  public override void _Process(float delta)
  {
    if (Input.IsActionPressed("shoot") && canShoot)
    {
      shoot();
    }
  }

  public void _on_Timer_timeout()
  {
    canShoot = true;
  }

  public void reload()
  {
    GD.Print("reloading.");
  }

  public void appendProjectile()
  {
    Projectile proj = projectile.Instance() as Projectile;
    GetParent().AddChild(proj);
    proj.SetPosition(Transform.origin);
    proj.setDirection(toMouseVec());
  }

  public void shoot()
  {
    canShoot = false;
    appendProjectile();
    timer.Start(secBetweenShots);
  }

  /// <summary>
  /// Returns the direction vector from the projectile shooter to the mouse.
  /// Returns a null vector if the global mouse position is null.
  /// </summary>
  ///
  /// <returns>
  /// The direction vector from the projectile shooter to the mouse.
  /// </returns>
  private Vector2 toMouseVec()
  {
    Vector2 mousePos = GetGlobalMousePosition();
    if (mousePos == null) return new Vector2();

    return new Vector2(
      mousePos.x - GetGlobalPosition().x,
      mousePos.y - GetGlobalPosition().y
    ).Normalized();
  }
}
