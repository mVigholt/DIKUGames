namespace Galaga.Squadron;

using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    int[,] Formation {get;}
    void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride);

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
