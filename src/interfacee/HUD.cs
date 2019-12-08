using Godot;

namespace tdws.interfacee
{
  /// <summary>
  ///   The heads up display. Displays health and amount of coins.
  /// </summary>
  public class HUD : Control
  {
    private RichTextLabel _chat;
    private RichTextLabel _coins;
    private RichTextLabel _health;

    /// <summary>
    ///   Adds a chat message in a new row. Does nothing if the provided message is null or empty.
    /// </summary>
    /// <param name="message">
    ///   The message to add.
    /// </param>
    public void AddChat(string message)
    {
      ClearChat(); // TODO: Remove. Only here during dev.
      AddChat(message, Colors.White); // Ineffective since the color is a struct?
    }

    /// <summary>
    ///   Adds a chat message in a new row. Does nothing if the provided message is null or empty.
    /// </summary>
    /// <param name="message">
    ///   The message to add.
    /// </param>
    /// <param name="color">
    ///   The color of the message.
    /// </param>
    public void AddChat(string message, Color color)
    {
      if (message == null) return;

      _chat.AddText(message);
    }

    /// <summary>
    ///   Gets called when the players health is changed.
    /// </summary>
    /// <param name="amount">
    ///   The new amount of health.
    /// </param>
    public void HealthChanged(int amount)
    {
      SetHealth(amount);
    }

    public override void _Ready()
    {
      _coins = GetNode("Coins") as RichTextLabel;
      _health = GetNode("Health") as RichTextLabel;
      _chat = GetNode("Chat") as RichTextLabel;

      SetHealth(666);
      SetCoins(123);
    }

    /// <summary>
    ///   Updates the amount of health displayed.
    /// </summary>
    /// <param name="amount">
    ///   The amount of health to display.
    /// </param>
    private void SetHealth(int amount)
    {
      _health.SetText(amount.ToString());
    }

    /// <summary>
    ///   Updates the amount of coins displayed.
    /// </summary>
    /// <param name="amount">
    ///   The amount of coins to display.
    /// </param>
    private void SetCoins(int amount)
    {
      _coins.SetText(amount.ToString());
    }

    /// <summary>
    ///   Clears the chat.
    /// </summary>
    private void ClearChat()
    {
      _chat.Clear();
    }
  }
}