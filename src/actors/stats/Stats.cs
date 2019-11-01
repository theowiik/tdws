using System;
using Godot;

/// <summary>
/// The Stats class is used for storing a actors health.
/// </summary>
public class Stats : Node, ILiving
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
    _HP = Math.Min(_HP, _maxHP);
  }

  public bool isAlive()
  {
    return _HP <= 0;
  }

  public void takeDamage(int hp)
  {
    _HP -= hp;
  }
}
