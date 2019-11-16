using tdws.objects.projectiles.projectile;

namespace tdws.objects.projectiles.bullet
{
  public class Bullet : Projectile
  {
    protected override void OverrideProperties()
    {
      Speed = 600;
    }
  }
}