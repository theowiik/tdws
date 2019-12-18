namespace tdws.engine
{
  /// <summary>
  ///   A game level.
  /// </summary>
  public interface ILevel
  {
    /// <summary>
    ///   Configs the level.
    /// </summary>
    void Enter();

    /// <summary>
    ///   Resets a level.
    /// </summary>
    void Reset();

    /// <summary>
    ///   "Exits" a level.
    ///   TODO: Explain better its use case, dont even know if it has any good use cases atm.
    /// </summary>
    void Exit();
  }
}