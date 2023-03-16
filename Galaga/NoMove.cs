namespace Galaga;

using DIKUArcade.Entities;
using MovementStrategy;

public class NoMove : IMovementStrategy {
    public NoMove(Enemy enemy) {}

    public void MoveEnemy(Enemy enemy) {}
}