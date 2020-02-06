using Godot;

namespace tdws.particles
{
  public class DeathEffect : Particles2D
  {
    private float _elapsedTime;

    public override void _Ready()
    {
      _elapsedTime = 0f;
    }

    public override void _Process(float delta)
    {
      _elapsedTime += delta;

      if (_elapsedTime > Lifetime) QueueFree();
    }
  }
}
