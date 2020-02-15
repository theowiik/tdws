using System;
using Godot;

namespace tdws.Scripts.Services
{
  /// <summary>
  ///   Helper methods for nodes in Godot.
  /// </summary>
  public static class NodeService
  {
    /// <summary>
    ///   Loads a scene and throws a null reference exception if the scene could not be loaded.
    /// </summary>
    /// <param name="path">The path to the scene.</param>
    /// <typeparam name="T">The type of the scene to load.</typeparam>
    /// <returns>The scene.</returns>
    /// <exception cref="NullReferenceException">If the scene could not be loaded.</exception>
    public static T LoadNotNull<T>(string path) where T : class
    {
      Objects.RequireNonNull(path);
      var scene = GD.Load<T>(path);
      return Objects.RequireNonNull(scene);
    }

    /// <summary>
    ///   Instances a scene as type T.
    /// </summary>
    /// <param name="path">The path to the scene.</param>
    /// <typeparam name="T">The type to instance.</typeparam>
    /// <returns>A instance of T.</returns>
    /// <exception cref="NullReferenceException">If the scene could not be loaded.</exception>
    public static T InstanceNotNull<T>(string path) where T : class
    {
      if (LoadNotNull<PackedScene>(path).Instance() is T instance)
        return instance;

      throw new NullReferenceException("Could not instance");
    }
  }
}
