namespace Galaga;

using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;


/// <summary> A squadron in the shape of an A </summary>
public class RiSquadron : ISquadron {

    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;}

    public RiSquadron(
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride
    ) {
        this.MaxEnemies = 32;
        this.Enemies =  new EntityContainer<Enemy>(MaxEnemies);
        this.CreateEnemies(enemyStride, alternativeEnemyStride);
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        int[,] formation = new int[,] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        int milliseconds = 80;
        int height = formation.GetLength(0);
        int width = formation.GetLength(1);
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (formation[y, x] == 1) {
                    Enemies.AddEntity(
                        new Enemy(
                            new Vec2F(x * 0.1f, 1 - y * 0.1f),
                            new ImageStride(milliseconds, enemyStride),
                            new ImageStride(milliseconds, alternativeEnemyStride)
                    ));
                }
            }
        }

        /* int width = 8;
        for (int i = 1; i < width - 1; i++) {
            int milliseconds = 80;
            Enemies.AddEntity(
                new Enemy(
                    new Vec2F(0.1f + i * 0.1f, 0.9f),
                    new ImageStride(milliseconds, enemyStride),
                    new ImageStride(milliseconds, alternativeEnemyStride)
            ));
            Enemies.AddEntity(
                new Enemy(
                    new Vec2F(0.8f, 0.9f - i * 0.1f),
                    new ImageStride(milliseconds, enemyStride),
                    new ImageStride(milliseconds, alternativeEnemyStride)
            ));
            Enemies.AddEntity(
                new Enemy(
                    new Vec2F(0.1f + i * 0.1f, 0.6f),
                    new ImageStride(milliseconds, enemyStride),
                    new ImageStride(milliseconds, alternativeEnemyStride)
            ));
            Enemies.AddEntity(
                new Enemy(
                    new Vec2F(0.1f, 0.9f - i * 0.1f),
                    new ImageStride(milliseconds, enemyStride),
                    new ImageStride(milliseconds, alternativeEnemyStride)
            ));
        } */
    }
}