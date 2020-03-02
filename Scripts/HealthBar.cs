using Godot;

namespace tdws.Scripts
{
  public class HealthBar : Control
  {
    private RichTextLabel _text;

    public override void _Ready()
    {
      _text = GetNode<RichTextLabel>("RichTextLabel");
    }

    public void OnHealthChanged(int health)
    {
      _text.Text = "H: " + health;

      if (health <= 0)
        Destroy();
    }

    /// <summary>
    ///   "Destroys" the health bar and removes it.
    /// </summary>
    private void Destroy()
    {
      QueueFree();
    }
  }
}