using Godot;

namespace tdws.objects.door
{
  /// <summary>
  ///   A door that can be entered.
  /// </summary>
  public class Door : Area2D
  {
    [Signal]
    public delegate void DoorEntered();

    private bool _atDoor;

    private RichTextLabel _enterText;

    public override void _Ready()
    {
      _enterText = GetNode("EnterText") as RichTextLabel;
      _enterText.Visible = false;
      _atDoor = false;
    }

    public override void _Input(InputEvent @event)
    {
      if (_atDoor && @event.IsActionPressed("open_door"))
        EmitSignal(nameof(DoorEntered));
    }

    private void OnDoorEntered(object body)
    {
      if (body is KinematicBody2D kinematicBody2D)
      {
        _atDoor = true;
        _enterText.Visible = true;
      }
    }

    private void OnDoorBodyExited(object body)
    {
      if (body is KinematicBody2D kinematicBody2D)
      {
        _atDoor = false;
        _enterText.Visible = false;
      }
    }
  }
}