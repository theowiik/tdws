using Godot;

namespace tdws.Scripts
{
  /// <summary>
  ///   A coin. TODO: Make it implement something like pickupable.
  /// </summary>
  public class Coin : RigidBody2D
  {
    private const int Value = 10;
    private bool _pickupable = true;

    /// <summary>
    ///   Is called when a body enters the coin.
    /// </summary>
    /// <param name="body">
    ///   The body that entered the coin.
    /// </param>
    private void OnPickupAreaEntered(object body)
    {
      if (!_pickupable) return;

      if (body is ICanPickup pickup)
      {
        _pickupable = false;
        pickup.PickupCoins(Value);
        GetNode<AudioStreamPlayer>("PickupPlayer").Play();
        Hide();
      }
    }

    /// <summary>
    ///   Remove the coin only when the audio is finished playing.
    /// </summary>
    public void OnPickupPlayerFinished()
    {
      QueueFree();
    }

    /// <summary>
    ///   Hides the coins.
    /// </summary>
    private void Hide()
    {
      GetNode("Sprite").QueueFree();
    }
  }
}
