namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;

public class TriangleSquadrons: ISquadron{

    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;}

    public TriangleSquadrons (List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {
        this.MaxEnemies = 8 ;
        this.Enemies =  new EntityContainer<Enemy>(MaxEnemies);
        this.CreateEnemies(enemyStride,alternativeEnemyStride);
    }

    public void CreateEnemies(List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {
        int j = 4;
        for (int i = 0; i < 9; i++){
            int milliseconds = 80;
            Vec2F pos = new Vec2F(0.0f , 0.0f);
            if (i < 4){
                pos = new Vec2F(0.1f + i * 0.1f, 1.0f - j * 0.1f);
                j--;
            }
            else {
                pos = new Vec2F(0.1f +(float) (i-1) * 0.1f, 1.0f - j * 0.1f);
                j++;
            }
            Enemy enemy = new Enemy(pos,
                new ImageStride(milliseconds, enemyStride),
                new ImageStride(milliseconds, alternativeEnemyStride));
            this.Enemies.AddEntity(enemy);

        }
    }
}