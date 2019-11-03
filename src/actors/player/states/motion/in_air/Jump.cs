using Godot;

public class Jump : Motion
{
  private float _height;

  public Jump(IMovable movable) : base(movable)
  { }

  public override void Enter() { }

  public override void Exit() { }

  public override void HandleInput(InputEvent @event) { }

  public override void Update(float delta)
  {
    if (_height <= 0)
    {
      GD.Print("JUMP FINISHED!");
    }
  }

}
