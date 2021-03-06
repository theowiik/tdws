﻿using System.Collections.Generic;
using Godot;
using tdws.Scripts.Actors;

namespace tdws.Scripts.Room
{
  /// <summary>
  ///   Represents a room.
  /// </summary>
  public interface IRoom
  {
    /// <summary>
    ///   Returns a list of the rooms doors.
    /// </summary>
    /// <returns>A list of the rooms doors.</returns>
    IEnumerable<Door> GetDoors();

    /// <summary>
    ///   Returns a list of the rooms enemies.
    /// </summary>
    /// <returns>A list of the rooms enemies.</returns>
    IEnumerable<AbstractEnemy> GetEnemies();

    /// <summary>
    ///   Returns the spawn point.
    /// </summary>
    /// <returns>The spawn point.</returns>
    Vector2 GetSpawnPoint();

    /// <summary>
    ///   Checks if all enemies are dead.
    /// </summary>
    /// <returns>True if all enemies are dead. False otherwise.</returns>
    bool AllEnemiesAreDead();
  }
}