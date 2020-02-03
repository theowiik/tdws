using Godot;

namespace tdws
{
  /// <summary>
  ///   The main camera for the game.
  /// </summary>
  public class Camera : Camera2D
  {
    public override void _Ready()
    {
      Current = true;
      SmoothingEnabled = true;
      SmoothingSpeed = 5f;
    }
  }
}
