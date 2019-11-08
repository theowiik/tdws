using System;
using Godot;

namespace tdws.actors.stats
{
  /// <summary>
  ///   The Stats class is used for storing a actors health.
  /// </summary>
  public class Stats : Node, ILiving
  {
    private int _hp;
    private int _maxHp;

    public void Heal(int hp)
    {
      _hp += hp;
      _hp = Math.Min(_hp, _maxHp);
    }

    public bool IsAlive()
    {
      return _hp > 0;
    }

    public bool IsDead()
    {
      return !IsAlive();
    }

    public void TakeDamage(int hp)
    {
      _hp -= hp;
    }

    public override void _Ready()
    {
      _maxHp = 100;
      _hp = _maxHp;
    }
  }
}