namespace Breakout.BreakoutEntities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Block : Entity {
    private static readonly Vec2F EXTENT = new Vec2F(0.083f, 0.041f);
    public readonly Vec2F position;
    private IBaseImage alterImage;
    private int health = 8;

    public Block(Block.Builder builder)
        : base(new DynamicShape(builder.position, EXTENT), builder.image) {
        this.position = builder.position;
        this.Image = builder.image;
        this.alterImage = builder.alterImage;
    }
    public void LoseHealth(int hp) {
            this.health-= hp;
        }
    public bool IsDead() {
        return this.health <= 0;
    }

    public class Builder {
        public Vec2F position;
        public IBaseImage image;
        public IBaseImage alterImage;
        public Builder() {
        }

        public Builder WithPosition(Vec2F position) {
            this.position = position;
            return this;
        }

        public Builder WithImage(IBaseImage image) {
            this.image = image;
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