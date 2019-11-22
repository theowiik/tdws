using Godot;
using tdws.actors.monsters.abstract_monster;
using tdws.objects.projectiles.projectile;

/// <summary>
///   A homing projectile.
/// </summary>
public class HomingProjectile : Projectile
{
  private Area2D _detectionArea;
  private Vector2 _target;

  protected override void OverrideProperties()
  {
    _detectionArea = GetNode("DetectionArea") as Area2D;
  }

  public void OnDetectionAreaBodyEntered(object body)
  {
    if (body is AbstractMonster monster)
    {
      Direction = GetGlobalPosition().DirectionTo(monster.GetGlobalPosition());
    }
  }
}