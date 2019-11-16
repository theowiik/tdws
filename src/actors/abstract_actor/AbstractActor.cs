using Godot;

namespace tdws.actors.abstract_actor
{
  /// <summary>
  ///   The base class all actors inherit from.
  /// </summary>
  public abstract class AbstractActor : KinematicBody2D
  {
    protected Stats Stats;

    protected AbstractActor()
    {
      Stats = new Stats(100, 100);
    }
  }
}