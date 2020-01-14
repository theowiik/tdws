using Godot;

namespace tdws.objects.door
{
  public class Door : Area2D
  {
    [Signal]
    public delegate void DoorEntered();

    private RichTextLabel _enterText;

    public override void _Ready()
    {
      _enterText = GetNode("EnterText") as RichTextLabel;
      _enterText.Visible = false;
    }

    private void OnDoorEntered(object body)
    {
      if (body is KinematicBody2D kinematicBody2D)
        _enterText.Visible = true;
    }

    private void OnDoorBodyExited(object body)
    {
      if (body is KinematicBody2D kinematicBody2D)
        _enterText.Visible = false;
    }
  }
}