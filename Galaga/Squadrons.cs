namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;
using Galaga.MovementStrategy;

public class Squadrons: ISquadron{
    private Vec2F position;
    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;}

    public Squadrons (int maxEnemies, Vec2F startPosition) {
        this.MaxEnemies = maxEnemies;
        this.Enemies =  new EntityContainer<Enemy>(MaxEnemies);
        this.position = startPosition;
    }

    public void CreateEnemies(List<Image> enemyStride,
        List<Image> alternativeEnemyStride) {
        for (int i = 0; i < this.MaxEnemies; i++){
            int milliseconds = 80;
            Vec2F pos = new Vec2F(0.0f , 0.0f);
            if (i < 4){
                pos = new Vec2F(this.position.X + (float) i * 0.1f, this.position.Y);
            }
            else {
                pos = new Vec2F(this.position.X + (float) (i - 4) * 0.1f, this.position.Y - 0.1f);
            }
            // Vec2F pos = new Vec2F(0.1f + (float) i * 0.1f, 1.0f - row * 0.1f);
            Vec2F extent = new Vec2F(0.1f, 0.1f);
            Enemy enemy = new Enemy(
            new DynamicShape(pos, extent),
            new ImageStride(milliseconds, enemyStride),
            new ImageStride(milliseconds, alternativeEnemyStride));
            this.Enemies.AddEntity(enemy);
        }
    }
}