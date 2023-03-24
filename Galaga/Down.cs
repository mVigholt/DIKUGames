namespace Galaga;
using MovementStrategy;

///<summary>Make the squadron move downwards</summary>
public class Down : IMovementStrategy {
    public Down(Enemy enemy) {}

    public void MoveEnemy(Enemy enemy) {
        enemy.Shape.MoveY(-enemy.Speed);
    }
}