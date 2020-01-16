using System;
using System.Collections.Generic;
using System.Linq;

namespace tdws.services
{
  /// <summary>
  ///   A collection of useful array methods.
  /// </summary>
  public static class ListService
  {
    /// <summary>
    ///   Shuffles a list. Not a true shuffle.
    /// </summary>
    /// <param name="list">
    ///   The list to shuffle.
    /// </param>
    /// <typeparam name="T">
    ///   The type of objects the list contains.
    /// </typeparam>
    public static void ShuffleList<T>(IList<T> list)
    {
      var rnd = new Random();

      for (var i = list.Count; i > 0; i--)
        list.Swap(0, rnd.Next(0, i));
    }

    private static void Swap<T>(this IList<T> list, int i, int j)
    {
      var temp = list[i];
      list[i] = list[j];
      list[j] = temp;
    }

    /// <summary>
    ///   Selects n random items from the provided list and returns a new list containing said items.
    ///   Does not select a item multiple times.
    /// </summary>
    /// <param name="list">The list to select items from.</param>
    /// <param name="n">The amount of items to collect.</param>
    /// <typeparam name="T">The type of items the list holds.</typeparam>
    /// <returns>A new list with n randomly selected elements.</returns>
    /// <exception cref="Exception">If the provided list does not contain >= n items.</exception>
    public static IList<T> SelectNRandom<T>(IList<T> list, int n)
    {
      var shuffledList = list.ToList();
      ShuffleList(shuffledList);

      var nSelected = 0;
      var output = new List<T>();
      foreach (var item in shuffledList)
      {
        output.Add(item);
        nSelected++;

        if (nSelected >= n)
          break;
      }

      if (nSelected != n)
        throw new Exception("The provided list does not contain >= n items.");

      return output;
    }
  }
}