using Godot;

/// <summary>
/// The Entity class represents a actor that can walk and take damage.
/// </summary>
public abstract class AbstractActor : KinematicBody2D, ILiving
{
  private int _maxHP;
  private int _HP;

  public override void _Ready()
  {
    _maxHP = 10;
    _HP = _maxHP;
  }

  public void heal(int hp)
  {
    _HP += hp;
  }

  public bool isAlive()
  {
    return _HP <= 0;
  }

  public void kill()
  {
    QueueFree();
  }

  public void takeDamage(int hp)
  {
    _HP -= hp;
  }
}
