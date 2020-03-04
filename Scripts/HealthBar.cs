using Godot;

namespace tdws.Scripts
{
  public class HealthBar : Control
  {
    private ColorRect _bar;
    private int _maxHealth;
    private bool _maxHealthSet;
    private float _widthPercentage;
    private float _animationSpeed;

    public HealthBar()
    {
      _maxHealthSet = false;
      _animationSpeed = 0.01f;
    }

    public override void _Process(float delta)
    {
      if (!_maxHealthSet)
        return;

      float currentWidth = _bar.RectScale.x;

      if (currentWidth > _widthPercentage)
      {
        var nextIsLowerThanActual = currentWidth - _animationSpeed < _widthPercentage;

        if (nextIsLowerThanActual)
        {
          GD.Print("setting to max: " + currentWidth);
          _bar.RectScale = new Vector2(_widthPercentage, 1);
        }
        else
        {
          GD.Print("decreasing a lil: " + currentWidth);
          _bar.RectScale -= new Vector2(_animationSpeed, 1);
        }
      }

      if (_bar.RectScale.x <= 0)
        Destroy();
    }

    public override void _Ready()
    {
      _bar = GetNode<ColorRect>("Bar/Green");
    }

    public void OnHealthChanged(int health)
    {
      if (!_maxHealthSet)
      {
        _maxHealth = health;
        _maxHealthSet = true;
      }

      _widthPercentage = (float)health / (float)_maxHealth;
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