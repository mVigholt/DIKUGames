namespace Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Enemy : Entity {
    
    public static float baseSpeed = 0.0003f;
    private float speed = baseSpeed;
    
    private static readonly Vec2F EXTENT = new Vec2F(0.1f, 0.1f);
    public readonly Vec2F startPosition;
    private IBaseImage alterImage;
    private int hitpoints;

    public Enemy(Vec2F position, IBaseImage image, IBaseImage alterImage)
        : base(new DynamicShape(position, EXTENT), image) {
        Image = image;
        this.alterImage = alterImage;
        startPosition = position;
        hitpoints = 4;
    }
    
    public float Speed {
        get {
            return speed;
        }
        private set {
            speed = value;
        }
    }

    public void LoseHealth() {
        hitpoints--;
    }

    public bool IsDead() {
        return hitpoints <= 0;
    }


    public bool isEnraged(){
        if (this.hitpoints <= 2) {
            this.speed += baseSpeed * 1.5f;
            this.Image = alterImage;
            return true;
        }
        else {
            return false;
        }
    }
}