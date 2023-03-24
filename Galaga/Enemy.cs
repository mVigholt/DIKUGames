namespace Galaga;

using System;
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

    public Enemy(Enemy.Builder builder)
        : base(new DynamicShape(builder.position, EXTENT), builder.image) {
        this.startPosition = builder.position;
        this.Image = builder.image;
        this.alterImage = builder.alterImage;
        this.speed = builder.speed;
        this.hitpoints = builder.hitpoints;
    }

    public float Speed {
        get {
            return speed;
        }
        private set {
            speed = value;
        }
    }

    public void LoseHealth(int hp) {
        hitpoints -= hp;
    }

    public bool IsDead() {
        return hitpoints <= 0;
    }

    public bool isEnraged(){
        if (this.hitpoints <= 2 && !enraged) {
            this.speed = baseSpeed * 3f;
            this.Image = alterImage;
            enraged = true;
        }
        return enraged;
    }

    public class Builder {
        public Vec2F position;
        public IBaseImage image;
        public IBaseImage alterImage;
        public int hitpoints;
        public float speed;

        public Builder() {}
        public Builder WithSpeed(float speed) {
            this.speed = speed;
            return this;
        }

        public Builder WithPosition(Vec2F position) {
            this.position = position;
            return this;
        }

        public Builder WithImage(IBaseImage image) {
            this.image = image;
            return this;
        }

        public Builder WithAlternativeImage(IBaseImage image) {
            this.alterImage = image;
            return this;
        }

        public Builder WithHitpoints(int hitpoints) {
            this.hitpoints = hitpoints;
            return this;
        }

        /// <summary>
        /// The final method you need to call when constructing
        /// an enemy. It is common practice to put all validation
        /// inside this method.
        /// </summary>
        public Enemy Build() {
            // Required arguments
            if (position is null ||
                image is null ||
                alterImage is null
            ) {
                throw new ArgumentException(
                    "An enemy must have a position and two images");
            }
            // Optional arguments
            if (speed == 0f) {
                speed = Enemy.baseSpeed;
            }
            if (hitpoints == 0) {
                hitpoints = 4;
            }
            return new Enemy(this);
        }
    }
}