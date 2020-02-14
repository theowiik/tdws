using System;

namespace tdws.Scripts.Services
{
  /// <summary>
  ///   Helper methods for all things objects.
  /// </summary>
  public static class Objects
  {
    public static T RequireNonNull<T>(T obj, string message = "some message")
    {
      if (obj == null)
        throw new NullReferenceException(message);

      return obj;
    }
  }
}