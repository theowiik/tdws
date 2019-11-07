namespace tdws.objects
{
  /// <summary>
  ///   Represents something that can deal damage.
  /// </summary>
  public interface IDamageSource
  {
    /// <summary>
    ///   Returns the amount of damage that the object deals.
    /// </summary>
    /// <returns>
    ///   The amount of damage that the object deals.
    /// </returns>
    int GetDamage();
  }
}