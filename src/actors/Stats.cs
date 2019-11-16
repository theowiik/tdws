using System;
using tdws.utils;

namespace tdws.actors.stats
{
  /// <summary>
  ///   The Stats class is used for storing a actors health.
  /// </summary>
  public class Stats : ILiving
  {
    private readonly int _maxHp;
    private int _hp;

    /// <param name="hp">The health points.</param>
    /// <param name="maxHp">The max amount of health points.</param>
    public Stats(int hp, int maxHp)
    {
      _hp = hp;
      _maxHp = maxHp;
      NotifyHealthChanged();
    }

    public void Heal(int hp)
    {
      _hp += hp;
      _hp = Math.Min(_hp, _maxHp);
      NotifyHealthChanged();
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
      NotifyHealthChanged();
    }

    /// <summary>
    ///   Notifies the HealthChanged signal.
    /// </summary>
    private void NotifyHealthChanged()
    {
      SignalManager.GetInstance().NotifyHealthChanged(_hp);
    }
  }
}