namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Enemy : Entity {

    public static float baseSpeed = 0.0003f;
    private float speed = baseSpeed;

    private bool enraged = false;

    private static readonly Vec2F EXTENT = new Vec2F(0.1f, 0.1f);
    public readonly Vec2F startPosition;
    private IBaseImage alterImage;
    public int hitpoints {get; private set;}

    public Enemy(Vec2F position, IBaseImage image, IBaseImage alterImage)
        : base(new DynamicShape(position, EXTENT), image) {
        Image = image;
        this.alterImage = alterImage;
        startPosition = position;
        hitpoints = 4;
    }

    public Enemy(EnemyBuilder builder)
        : base(new DynamicShape(builder.position, EXTENT), builder.image) {
        this.startPosition = builder.position;
        this.Image = builder.image;
        this.alterImage = builder.alterImage;
        this.speed = builder.speed;
        this.hitpoints = builder.hitpoints;
    }

    public Enemy(
        Vec2F position,
        float speed,
        IBaseImage image,
        IBaseImage alterImage
    ) : base(new DynamicShape(position, EXTENT), image) {
        // Use this constructor to set speed
        Image = image;
        this.alterImage = alterImage;
        startPosition = position;
        hitpoints = 4;
        this.speed = speed;
    }

    public float Speed {
        get {
            return speed;
        }
        private set {
            speed = value;
        }
    }
    ///<summary>Make the enemy's hitpoint drop by input hp</summary>
    ///<param name = "hp">The input hitpoint</param>
    ///<return>no return</return>
    public void LoseHealth(int hp) {
        hitpoints -= hp;
    }

    ///<summary>Check if the enemy is dead</summary>
    ///<return>True for hitpoints is equal and below 0, false for over</return>
    public bool IsDead() {
        return hitpoints <= 0;
    }

    ///<summary>Check if the enemy is enraged by its hitpoints</summary>
    ///<return>True for enraged state, false for not</return>
    public bool isEnraged(){
        if (this.hitpoints <= 2 && !enraged) {
            this.speed = baseSpeed * 3f;
            this.Image = alterImage;
            enraged = true;
        }
        return enraged;
    }
}