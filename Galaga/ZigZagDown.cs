namespace Galaga;

using DIKUArcade.Entities;
using MovementStrategy;
using System;

public class ZigZagDown : IMovementStrategy {
    private float x0 ;
    private float y0 ;
    public ZigZagDown(Enemy enemy){
        x0 = enemy.Shape.Position.X;
        y0 = enemy.Shape.Position.Y;
    }

    public void MoveEnemy(Enemy enemy) {
        float p = 0.045f;
        float s = 0.0003f;
        float a = 0.05f;
        enemy.Shape.MoveY(-s -enemy.Speed);
        float yi_1 = enemy.Shape.Position.Y + s;
        float yi = enemy.Shape.Position.Y;
        float xi_1 = x0 + (float) (a* Math.Sin (( 2* Math.PI * (y0 - yi_1))/p));
        float xi = x0 + (float) (a* Math.Sin (( 2* Math.PI * (y0 - yi))/p));
        float deltaX = xi - xi_1;
        enemy.Shape.MoveX(-deltaX);
    }
}