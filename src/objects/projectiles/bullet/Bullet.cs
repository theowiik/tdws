using Godot;
using System;
using tdws.objects.projectiles.projectile;

public class Bullet : Projectile
{
  protected override void OverrideProperties()
  {
    Speed = 600;
  }
}