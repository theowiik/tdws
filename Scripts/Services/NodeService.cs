using System;
using System.Collections.Generic;
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

    /// <summary>
    ///   Instances a scenes as type T.
    /// </summary>
    /// <typeparam name="T">The type to instance as.</typeparam>
    /// <exception cref="NullReferenceException">If the provided scene is null.</exception>
    public static T InstanceScene<T>(PackedScene scene) where T : class
    {
      Objects.RequireNonNull(scene);

      if (scene.Instance() is T instance)
        return instance;

      throw new Exception("The scene provided is not type T");
    }

    /// <summary>
    ///   Returns a list of all child nodes that are of the specified type.
    /// </summary>
    /// <param name="node">The node to extract children from.</param>
    /// <typeparam name="T">The type of children to extract.</typeparam>
    /// <returns>A list of all child nodes that are of the specified type.</returns>
    public static IList<T> GetChildrenOfType<T>(Node node)
    {
      if (node == null)
        return new List<T>();

      var output   = new List<T>();
      var children = node.GetChildren();

      foreach (var child in children)
        if (child is T t)
          output.Add(t);

      return output;
    }
  }
}