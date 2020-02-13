namespace tdws.core
{
  /// <summary>
  ///   A representation of a object pool.
  /// </summary>
  /// <typeparam name="T">
  ///   The type of objects the pool holds.
  /// </typeparam>
  public interface IObjectPool<T>
  {
    /// <summary>
    ///   Get a object in the pool.
    /// </summary>
    /// <returns>
    ///   Returns a object.
    /// </returns>
    T Get();

    /// <summary>
    ///   Adds a object to the pool.
    /// </summary>
    /// <param name="obj">
    ///   The object to add.
    /// </param>
    void Add(T obj);

    /// <summary>
    ///   Removes all the objects in the pool.
    /// </summary>
    void Clear();
  }
}