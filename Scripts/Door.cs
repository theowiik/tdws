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

    private AnimationPlayer _animationPlayer;
    private bool _atDoor;
    private bool _enterable;
    private RichTextLabel _enterText;

    public override void _Ready()
    {
      _enterable = false;
      _enterText = GetNode("EnterText") as RichTextLabel;
      _enterText.Visible = false;
      _animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
      _animationPlayer.Play("locked");
      _atDoor = false;
    }

    public override void _Input(InputEvent @event)
    {
      if (_enterable && _atDoor && @event.IsActionPressed("open_door"))
        EmitSignal(nameof(DoorEntered));
    }

    /// <summary>
    ///   Makes the door enterable.
    /// </summary>
    public void Enterable()
    {
      _enterable = true;
      _animationPlayer.Play("unlocked");
    }

    private void OnDoorEntered(object body)
    {
      _atDoor = true;

      if (_enterable)
      {
        _enterText.Visible = true;
        _animationPlayer.Play("open");
      }
    }

    private void OnDoorBodyExited(object body)
    {
      _atDoor = false;
      _enterText.Visible = false;

      if (_enterable)
        _animationPlayer.Play("unlocked");
    }
  }
}
