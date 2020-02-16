using System.Collections.Generic;
using tdws.Scripts.Projectiles;

namespace tdws.Scripts
{
  /// <summary>
  ///   A pool containing projectiles.
  /// </summary>
  public class ProjectilePool : IObjectPool<AbstractProjectile>
  {
    private readonly Queue<AbstractProjectile> _queue;

    public ProjectilePool()
    {
      _queue = new Queue<AbstractProjectile>();
    }

    public AbstractProjectile Get()
    {
      if (_queue.Count > 0)
        return _queue.Dequeue();

      var projectileScene = ProjectileFactory.CreateBullet();
      return projectileScene.Instance() as AbstractProjectile;
    }

    public void Add(AbstractProjectile obj)
    {
      if (obj == null) return;

      _queue.Enqueue(obj);
    }

    public void Clear()
    {
      _queue.Clear();
    }
  }
}