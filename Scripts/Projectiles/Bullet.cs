using tdws.objects.projectiles.abstract_projectile;

namespace tdws.objects.projectiles.bullet
{
  public class Bullet : AbstractProjectile
  {
    protected override void OverrideProperties()
    {
      Speed = 600;
    }
  }
}
