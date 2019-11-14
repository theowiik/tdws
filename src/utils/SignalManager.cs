using System.Collections.Generic;

namespace tdws.utils
{
  /// <summary>
  ///   A class that manages signals. A singleton.
  /// </summary>
  public class SignalManager
  {
    private static SignalManager _instance;
    private List<IHealthChangeListener> _healthChangeListeners;

    private SignalManager()
    {
      _healthChangeListeners = new List<IHealthChangeListener>();
    }

    /// <summary>
    ///   Returns the instance of the signal manager.
    /// </summary>
    /// <returns>
    ///   The instance of the signal manager.
    /// </returns>
    public static SignalManager GetInstance()
    {
      return _instance ?? (_instance = new SignalManager());
    }

    public void AddHealthChangeListener(IHealthChangeListener healthChangeListener)
    {
      if (healthChangeListener != null)
        _healthChangeListeners.Add(healthChangeListener);
    }

    /// <summary>
    ///   Notifies all health change listeners that the amount of health has been changed.
    /// </summary>
    /// <param name="amount">
    ///   The new amount of health.
    /// </param>
    public void NotifyHealthChanged(int amount)
    {
      foreach (var healthChangeListener in _healthChangeListeners)
        healthChangeListener.HealthChanged(amount);
    }
  }

  /// <summary>
  ///   Represents something that listens for health changes.
  /// </summary>
  public interface IHealthChangeListener
  {
    /// <summary>
    ///   Notifies the listener that the health has been changed.
    /// </summary>
    /// <param name="amount">
    ///   The new amount of health.
    /// </param>
    void HealthChanged(int amount);
  }
}