using System;

namespace tdws.actors
{
  /// <summary>
  ///   The Stats class is used for storing a actors health.
  /// </summary>
  public class Stats : ILiving
  {
    private readonly int _maxHp;


    /// <param name="hp">The health points.</param>
    /// <param name="maxHp">The max amount of health points.</param>
    public Stats(int hp, int maxHp)
    {
      Hp = hp;
      _maxHp = maxHp;
    }

    public int Hp { get; private set; }

    public int Coins { get; set; }

    public void Heal(int hp)
    {
      Hp += hp;
      Hp = Math.Min(Hp, _maxHp);
    }

    public bool IsAlive()
    {
      return Hp > 0;
    }

    public bool IsDead()
    {
      return !IsAlive();
    }

    public void TakeDamage(int hp)
    {
      Hp -= hp;
    }
  }
}
