namespace Galaga.Squadron;

using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    void CreateEnemies (
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride);

    /// <summary>Return true if an enemy has reached the bottom of the viewport </summary>
    bool HasWon() {
        foreach (Enemy enemy in Enemies) {
            if (enemy.Shape.Position.Y < 0f) {
                return true;
            }
        }
        return false;
    }

    /// <summary>Return true if all enemies are dead </summary>
    bool HasLost() {
        foreach (Enemy enemy in Enemies) {
            if (!enemy.IsDead()) {
                return false;
            }
        }
        return true;
    }
}
