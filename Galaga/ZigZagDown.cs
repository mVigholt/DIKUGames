namespace Galaga;

using DIKUArcade.Entities;
using MovementStrategy;
using System;

public class ZigZagDown : IMovementStrategy {
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        throw new System.NotImplementedException();
    }

    public void MoveEnemy(Enemy enemy) {
        float p = 0.045f;
        float s = 0.0003f;
        float a = 0.05f;
        float x0 = enemy.Shape.Position.X;
        float y0 = enemy.Shape.Position.Y;
        enemy.Shape.MoveY(s);
        float y = enemy.Shape.Position.Y;
        enemy.Shape.Position.X = (float) (x0 + a* Math.Sin (( 2* Math.PI * (y0 - y ))/p));
    }
}