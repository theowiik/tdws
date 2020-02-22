using Godot;
using tdws.Scripts.Services;

namespace tdws.Scripts.Projectiles
{
  /// <summary>
  ///   A rocket that explodes on impact and does splash damage.
  /// </summary>
  public class Rocket : AbstractProjectile
  {
    protected override void OverrideProperties()
    {
      Speed = 200;
      HitScene = NodeService.LoadNotNull<PackedScene>("res://Scenes/Explosion.tscn");
    }
  }
}
