using Godot;

/// <summary>
/// The Entity class represents a actor that can walk and take damage.
/// </summary>
public abstract class AbstractActor : KinematicBody2D
{
  private Stats _stats;

  public override void _Ready()
  {
    _stats = GetNode("Stats") as Stats;
  }
}
