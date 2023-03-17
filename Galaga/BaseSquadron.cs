namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;


public class BaseSquadron : ISquadron {

    private EntityContainer<Enemy> enemies;
    private int maxEnemies;
    private int[,] formation;

    public BaseSquadron(
        int maxEnemies,
        int[,] formation
    ) {
        this.maxEnemies = maxEnemies;
        this.enemies = new EntityContainer<Enemy>(MaxEnemies);
        this.formation = formation;
    }

    public EntityContainer<Enemy> Enemies { get { return enemies; } }

    public int MaxEnemies { get { return maxEnemies; } }

    public int[,] Formation { get { return formation; } }

    public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        int milliseconds = 80;
        int height = Formation.GetLength(0);
        int width = Formation.GetLength(1);
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (Formation[y, x] == 1) {
                    Enemies.AddEntity(
                        new Enemy(
                            new Vec2F(x * 0.1f, 1 - y * 0.1f),
                            new ImageStride(milliseconds, enemyStride),
                            new ImageStride(milliseconds, alternativeEnemyStride)
                    ));
                }
            }
        }
    }
}