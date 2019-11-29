using System.Collections.Generic;
using Godot;
using tdws.interfacee;

namespace tdws.utils
{
  /// <summary>
  ///   A class that manages signals. A singleton.
  /// </summary>
  public class SignalManager
  {
    private static SignalManager _instance;
    private readonly List<IChatListener> _chatListeners;
    private readonly List<IHealthChangeListener> _healthChangeListeners;

    private SignalManager()
    {
      _healthChangeListeners = new List<IHealthChangeListener>();
      _chatListeners = new List<IChatListener>();
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

    /// <summary>
    ///   Adds a message to the chat.
    /// </summary>
    /// <param name="message">
    ///   The message to add.
    /// </param>
    public void AddChat(string message)
    {
      foreach (var chatListener in _chatListeners)
        chatListener.AddChat(message);
    }

    public void AddChatListener(IChatListener listener)
    {
      if (listener == null) return;

      _chatListeners.Add(listener);
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

  /// <summary>
  ///   Represents something that listens for chat events.
  /// </summary>
  public interface IChatListener
  {
    /// <summary>
    ///   Adds a message to the chat.
    /// </summary>
    /// <param name="message">The message to add.</param>
    void AddChat(string message);

    /// <summary>
    ///   Adds a message to the chat. Does nothing if the provided string is null.
    /// </summary>
    /// <param name="message">The message to add.</param>
    /// <param name="color">The color of the chat message.</param>
    void AddChat(string message, Color color);
  }
}