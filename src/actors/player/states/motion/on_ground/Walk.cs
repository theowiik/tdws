/// <summary>
/// The Walk class is used for handling walking movement.
/// </summary>
public sealed class Walk : OnGround
{
  private const int _MaxWalkSpeed = 3;
  private const int _MaxRunSpeed = 3;

  public void Enter()
  {
    Speed = 0.0;
  }
}
