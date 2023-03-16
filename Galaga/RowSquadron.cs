namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;

public class RowSquadron: ISquadron{

    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;}

    public RowSquadron (
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride
    ) {
        this.MaxEnemies = 32;
        this.Enemies =  new EntityContainer<Enemy>(MaxEnemies);
        this.CreateEnemies(enemyStride, alternativeEnemyStride);
    }

    public void CreateEnemies(
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {
        for (int i = 0; i < 8; i++){
            for (int j = 1; j < 3; j++){
                int milliseconds = 80;
                Vec2F pos = new Vec2F(0.1f + i * 0.1f, 1.0f - j * 0.1f);
                Enemy enemy = new Enemy(
                    pos,
                    new ImageStride(milliseconds, enemyStride),
                    new ImageStride(milliseconds, alternativeEnemyStride));
                Enemies.AddEntity(enemy);
            }
        }
    }
}