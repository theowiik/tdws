using Godot;

interface IProjectile
{
  /// <summary>
  /// Destroys the projectile.
  /// </summary>
  void destroy();

  /// <summary>
  /// Moves the projectile with 
  /// </summary>
  void move();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="direction"></param>
  void append(Vector2 direction);
}
