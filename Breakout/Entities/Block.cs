namespace Breakout.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Block : MoveableEntity {
    public static readonly Vec2F STD_EXTEND = new Vec2F(0.083f, 0.041f);
    private Builder build;

    private int health = 1;

    public Block(Block.Builder builder)
        : base(new DynamicShape(builder.position, STD_EXTEND), builder.image) {
        this.build = builder;
        if (this.build.isHardened) {
            health *= 2;
        }
    }

    public int Value {
        get {
            return build.value;
        }
    }

    public bool IsDead() {
        return this.health <= 0;
    }

    public void LoseHealth(int hp) {
        if (!this.build.isUnbreakable) {
            this.health -= hp;
        }

        if (this.IsDead()) {
            this.DeleteEntity();
        }
    }

    public class Builder {
        public Vec2F position;
        public IBaseImage image;
        public IBaseImage alterImage;
        public bool isUnbreakable = false;
        public bool isHardened = false;
        public int value;

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

        public Builder WithValue(int value) {
            if (value < 0) {
                throw new ArgumentException(
                    "A block's value cannot be negative");
            }
            this.value = value;
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