using Godot;
using System;

/// <summary>
///   The main camera for the game.
/// </summary>
public class Camera : Camera2D
{
  public override void _Ready()
  {
    Current = true;
    SmoothingEnabled = true;
    SmoothingSpeed = 5f;
  }
}