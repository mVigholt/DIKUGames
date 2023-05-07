namespace Breakout.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Block : MoveableEntity {
    private Builder build;

    private int health = 8;

    public Block(Block.Builder builder)
        : base(new DynamicShape(builder.position, new Vec2F(0.083f, 0.041f)), builder.image) {
            this.build = builder;
            if (this.build.isHardened) {
                health *= 2;
            }
    }

    public void LoseHealth(int hp) {
        if (!this.build.isUnbreakable){
            this.health-= hp;
        }
    }

    public bool IsDead() {
        return this.health <= 0;
    }

    public class Builder {
        public Vec2F position;
        public IBaseImage image;
        public IBaseImage alterImage;
        public bool isUnbreakable = false;
        public bool isHardened = false;

        public Builder() {
        }

        public Builder WithIsUnbreakable(Vec2F position) {
            this.isUnbreakable = true;
            return this;
        }

        public Builder WithIsHardened(Vec2F position) {
            this.isHardened = true;
            return this;
        }

        public Builder WithPosition(Vec2F position) {
            this.position = position;
            return this;
        }

        public Builder WithImage(IBaseImage image) {
            this.image = image;
            this.alterImage = image;
            return this;
        }

        public Builder WithAlterImage(IBaseImage alterImage) {
            this.alterImage = alterImage;
            return this;
        }

        public Block Build() {
            // Required arguments
            if (position is null ||
                image is null
            ) {
                throw new ArgumentException(
                    "A brick must have a position and a image");
            }
            return new Block(this);
        }
    }
}