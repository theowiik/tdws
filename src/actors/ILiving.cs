namespace tdws.actors
{
  /// <summary>
  ///   The ILiving interface represents something that has health points.
  /// </summary>
  public interface ILiving
  {
    /// <summary>
    ///   Heals the living thing.
    /// </summary>
    /// <param name="hp">
    ///   The amount of health points to add.
    /// </param>
    void Heal(int hp);

    /// <summary>
    ///   Damages the living thing.
    /// </summary>
    /// <param name="hp">
    ///   The amount of health points to remove.
    /// </param>
    void TakeDamage(int hp);

    /// <summary>
    ///   Checks if the living thing is living.
    /// </summary>
    /// <returns>
    ///   Returns true if it lives. False otherwise.
    /// </returns>
    bool IsAlive();

    /// <summary>
    ///   Checks if the living thing is dead.
    /// </summary>
    /// <returns>
    ///   Returns true if it is dead. False otherwise.
    /// </returns>
    bool IsDead();
  }
}