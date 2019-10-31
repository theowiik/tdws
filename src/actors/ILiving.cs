/// <summary>
/// The ILiving interface represents something that has health points.
/// </summary>
public interface ILiving
{
  /// <summary>
  /// Heals the living thing.
  /// </summary>
  ///
  /// <param name="hp">
  /// The amount of health points to add.
  /// </param>
  void heal(int hp);

  /// <summary>
  /// Damages the living thing.
  /// </summary>
  ///
  /// <param name="hp">
  /// The amount of health points to remove.
  /// </param>
  void takeDamage(int hp);

  /// <summary>
  /// Checks if the living thing is living.
  /// </summary>
  ///
  /// <returns>
  /// Returns true if it lives. False otherwise.
  /// </returns>
  bool isAlive();

  /// <summary>
  /// Kills the living thing.
  /// </summary>
  void kill();
}
