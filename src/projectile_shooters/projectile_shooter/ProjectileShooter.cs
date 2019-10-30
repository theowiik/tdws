using Godot;
using System;

/// <summary>
/// The ProjectileShooter class represents something that can shoot.
/// </summary>
public class ProjectileShooter : Sprite, IProjectileShooter
{
  private PackedScene projectile;

  public override void _Ready()
  {
    projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
  }

  public override void _Process(float delta)
  {
    if (Input.IsActionPressed("shoot")) shoot();
  }

  public void reload()
  {
    GD.Print("reloading.");
  }

  public void shoot()
  {
    Projectile proj = projectile.Instance() as Projectile;
    GetParent().AddChild(proj);
    proj.SetPosition(Transform.origin);
    proj.setDirection(toMouseVec());
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
