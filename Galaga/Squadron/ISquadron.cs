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

    void RenderHealth() {
        // Huge code smell.
        // If I hadn't noticed so late that the EnemyContainer.Render method
        // is hidden away deep inside DIKUArcade, I would have 
        // used a different pattern.
        //      - Asger
        foreach (Enemy enemy in Enemies) {
            enemy.RenderHealth();
        }
    }

    /// <summary>Return true if an enemy has reached the bottom of the viewport </summary>
    bool HasWon() {
        foreach (Enemy enemy in Enemies) {
            if (enemy.Shape.Position.Y < 0f) {
                return true;
            }
        }
        return false;
    }
}
