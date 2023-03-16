namespace Galaga;

using DIKUArcade.Entities;
using MovementStrategy;

public class NoMove : IMovementStrategy {
    public NoMove(Enemy enemy){}
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        foreach (Enemy enemy in enemies){
            MoveEnemy(enemy);
        }
    }

    public void MoveEnemy(Enemy enemy) {

    }
}