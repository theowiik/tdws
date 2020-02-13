namespace tdws.Scripts
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

    /// <summary>
    ///   Gets the actor that conflicted the damage.
    ///   Returns null if the actor source is ambiguous.
    /// </summary>
    /// <returns>
    ///   The actor that conflicted the damage.
    /// </returns>
    AbstractActor GetActorSource();

    /// <returns>
    ///   True if the damage source has a actor that conflicted the damage.
    /// </returns>
    bool HasActorSource();
  }
}