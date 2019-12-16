using Godot;
using tdws.actors;

namespace tdws.objects.coin
{
  /// <summary>
  ///   A coin. TODO: Make it implement something like pickupable.
  /// </summary>
  public class Coin : RigidBody2D
  {
    private const int Value = 100;

    /// <summary>
    ///   Is called when a body enters the coin.
    /// </summary>
    /// <param name="body">
    ///   The body that entered the coin.
    /// </param>
    private void OnBodyEntered(object body)
    {
      return;

      if (body is ICanPickup pickup)
      {
        pickup.PickupCoins(Value);
        QueueFree();
      }
    }
  }
}