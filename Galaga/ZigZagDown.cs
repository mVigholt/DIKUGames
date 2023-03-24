namespace Galaga;
using MovementStrategy;
using System;
using DIKUArcade.Math;
///<summary>Make the squadron move downwards zigag</summary>
public class ZigZagDown : IMovementStrategy {
    public ZigZagDown(Enemy enemy) {}

    public void MoveEnemy(Enemy enemy) {
        float p = 0.045f;   //period / wavelength
        float a = 0.05f;    //amplitude

        Vec2F startPos = enemy.startPosition;
        Vec2F curPos = enemy.Shape.Position;
        Vec2F pos = new Vec2F();

        pos.Y = curPos.Y - enemy.Speed;
        pos.X = startPos.X + (float)(a * Math.Sin((2 * Math.PI * (startPos.Y - pos.Y)) / p));

        enemy.Shape.SetPosition(pos);
    }
}