namespace Galaga;

using DIKUArcade.Entities;
using MovementStrategy;
using System;

public class ZigZagDown : IMovementStrategy {
    float x0 ;
    float y0 ;
    public ZigZagDown(Enemy enemy){
        x0 = enemy.Shape.Position.X;
        y0 = enemy.Shape.Position.Y;
    }
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        foreach (Enemy enemy in enemies){
            MoveEnemy(enemy);
        }
    }

    //#TODO, logic is not fully correct. It shows only half of the enemies.
    //Maybe the start position has some problems.
    public void MoveEnemy(Enemy enemy) {
        float p = 0.045f;
        float s = 0.0003f;
        float a = 0.05f;
        enemy.Shape.MoveY(-s);
        float yi = enemy.Shape.Position.Y;
        float xs = (float) (a* Math.Sin (( 2* Math.PI * (y0- yi))/p));
        enemy.Shape.Position.X =  x0 + xs;
    }
}