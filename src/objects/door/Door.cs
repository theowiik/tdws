using Godot;

namespace tdws.objects.door
{
  public class Door : Area2D
  {
    [Signal]
    public delegate void DoorEntered();

    private void OnDoorEntered(object body)
    {
      if (body is KinematicBody2D kinematicBody2D)
      {
        GD.Print("entered door!");
      }
    }
  }
}