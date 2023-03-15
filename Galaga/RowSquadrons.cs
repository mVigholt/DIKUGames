namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;

public class RowSquadrons: ISquadron{

    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;}

    public RowSquadrons (List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {
        this.MaxEnemies = 32;
        this.Enemies =  new EntityContainer<Enemy>(MaxEnemies);
        this.CreateEnemies(enemyStride,alternativeEnemyStride);
    }

    public void CreateEnemies(List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {
        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 4; j++){
                int milliseconds = 80;
                Vec2F pos = new Vec2F(0.1f + i * 0.1f, 1.0f - j * 0.1f);
                Vec2F extent = new Vec2F(0.1f, 0.1f);
                Enemy enemy = new Enemy(
                new DynamicShape(pos, extent),
                new ImageStride(milliseconds, enemyStride),
                new ImageStride(milliseconds, alternativeEnemyStride));
                this.Enemies.AddEntity(enemy);
            }
        }
    }
}