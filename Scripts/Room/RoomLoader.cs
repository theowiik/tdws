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
    private readonly Region        _region;
    private          AbstractActor _player;
    private          Room          _room;

    public RoomLoader()
    {
      _region = Region.Factory.CreateStartDungeon();
    }

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

    public void SetPlayer(AbstractActor player)
    {
      _player = Objects.RequireNonNull(player);
    }

    public Vector2 GetSpawnPoint()
    {
      return _room.GetSpawnPoint();
    }

    /// <summary>
    ///   Removes the current room from the scene tree and adds a new random door.
    /// </summary>
    public void NextRoom()
    {
      RemoveAllChildren();
      _room = GetRandomRoom();
      _room.Connect("ready", this, nameof(OnRoomReady));

      AddChild(_room);
    }

    /// <summary>
    ///   Gets called when a room is loaded. Sets the players position at the spawn coordinate.
    /// </summary>
    private void OnRoomReady()
    {
      _player.GlobalPosition = _room.GetSpawnPoint();
      _player.ForceUpdateTransform();
    }

    private Room GetRandomRoom()
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