using Godot;
using tdws.objects.projectiles.projectile;

namespace tdws.projectile_shooters.projectile_shooter
{
  /// <summary>
  ///   The ProjectileShooter class represents something that can shoot.
  /// </summary>
  public abstract class ProjectileShooter : Sprite, IProjectileShooter
  {
    private bool _canShoot;
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
      GD.Print("reloading.");
    }

    public void AppendProjectile()
    {
      for (var i = 0; i < ProjectilesPerShot; i++)
      {
        var proj = Projectile.Instance() as Projectile;
        if (proj == null) continue;

        GetParent().AddChild(proj);
        proj.SetPosition(Transform.origin);
        proj.SetDirection(GetTrajectoryVector());
      }
    }

    public void Shoot()
    {
      _canShoot = false;
      AppendProjectile();
      _timer.Start(SecondsBetweenShots);
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
      InitStandardValues();
      OverrideProperties();
    }

    /// <summary>
    ///   Sets all instance variables to standard values.
    /// </summary>
    private void InitStandardValues()
    {
      Projectile = GD.Load("res://src/objects/projectile/Projectile.tscn") as PackedScene;
      _timer = GetNode("Timer") as Timer;
      SecondsBetweenShots = 0.4f;
      _canShoot = true;
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