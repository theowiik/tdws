using System.Collections.Generic;
using Godot;
using tdws.Scripts.Actors;
using tdws.Scripts.Services;

namespace tdws.Scripts.Room
{
  /// <summary>
  ///   Manages rooms and regions.
  /// </summary>
  public class RoomLoader : Node
  {
    private AbstractActor _player;
    private IRoom _room;
    private Region _region;

    public override void _Process(float delta)
    {
    }

    /// <summary>
    ///   Checks if all enemies are dead in the current room.
    /// </summary>
    /// <returns>True if all enemies are dead in the current room. False otherwise.</returns>
    public bool AllEnemiesAreDead()
    {
      return _room.AllEnemiesAreDead();
    }

    public RoomLoader()
    {
      _region = Region.Factory.CreateStartDungeon();
    }

    public void SetPlayer(AbstractActor player)
    {
      _player = Objects.RequireNonNull(player);
    }

    public void NextRoom()
    {
      RemoveAllChildren();
      _room = GetRandomRoom();

      AddChild((Room)_room); // Hmm...
      _player.GlobalPosition = _room.GetSpawnPoint();
    }

    private IRoom GetRandomRoom()
    {
      return _region.GetRandomRoom();
    }

    public IEnumerable<AbstractEnemy> GetEnemies()
    {
      return _room.GetEnemies();
    }

    private void RemoveAllChildren()
    {
      foreach (Node child in GetChildren()) child.QueueFree();
    }

    /// <summary>
    ///   Makes all doors enterable.
    /// </summary>
    public void UnlockDoors()
    {
      foreach (var door in _room.GetDoors())
        door.MakeEnterable();
    }

    /// <summary>
    ///   Returns a list of the doors on the current room.
    /// </summary>
    /// <returns>
    ///   A list of the doors on the current room.
    /// </returns>
    public IEnumerable<Door> GetDoors()
    {
      return _room.GetDoors();
    }
  }
}
