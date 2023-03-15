namespace Galaga;

using DIKUArcade.Entities;
using MovementStrategy;

public class NoMove : IMovementStrategy {
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        // throw new System.NotImplementedException();
        foreach (Enemy enemy in enemies){
            MoveEnemy(enemy);
        }
    }

    public void MoveEnemy(Enemy enemy) {
        // throw new System.NotImplementedException();
    }
}