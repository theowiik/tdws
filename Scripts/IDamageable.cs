namespace tdws.Scripts
{
  /// <summary>
  ///   The IDamageable interface represents something that can take damage.
  /// </summary>
  public interface IDamageable
  {
    /// <summary>
    ///   Take damage from a damage source.
    /// </summary>
    /// <param name="damageSource">
    ///   The source of the damage.
    /// </param>
    void TakeDamage(IDamageSource damageSource);

    /// <summary>
    ///   Kills the damage object.
    /// </summary>
    void Die();
  }
}