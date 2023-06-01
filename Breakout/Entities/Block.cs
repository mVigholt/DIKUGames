namespace Breakout.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities.EffectItems;
using DIKUArcade.Physics;

public class Block : MoveableEntity {
    public static readonly Vec2F STD_EXTEND = new Vec2F(0.083f, 0.041f);
    public Builder build {
        get; private set;
    }

    private int maxHealth = 1;
    public int Health { get; private set; } = 1;

    private Block(Block.Builder builder)
        : base(new DynamicShape(builder.position, STD_EXTEND), builder.image) {
        this.build = builder;
        if (this.build.isHardened) {
            maxHealth *= 2;
        }
        Health = maxHealth;
    }

    public int Value {
        get {
            return build.value;
        }
    }

    public bool IsDead() {
        return this.Health <= 0;
    }

    public void LoseHealth(int hp) {
        if (!this.build.isUnbreakable) {
            this.Health -= hp;
        }

        if (this.IsDead()) {
            this.DeleteEntity();
        } else if (hp <= maxHealth / 2) {
            this.Image = this.build.alterImage;
        }
    }

    public override void Move() {
        if (this.build.isMovable) {
            this.Speed = 0.007f;
            if (this.GetPosition().X == 0.0f) {
                this.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirRight);
            }
            if (this.GetPosition().X == 1.0f - this.GetExtent().X) {
                this.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirLeft);
            }
            base.Move();
        }
    }

    public class Builder {
        public Vec2F position;
        public IBaseImage image;
        public IBaseImage alterImage;
        public bool isUnbreakable = false;
        public bool isHardened = false;
        public bool isMovable = false;
        public int value;
        public EffectItem effectItem;

        public Builder() {
        }
        public Builder WithMovable(bool statement) {
            if (statement) {
                this.isMovable = true;
            }
            return this;
        }
        public Builder WithIsUnbreakable(bool statement) {
            if (statement) {
                this.isUnbreakable = true;
            }
            return this;
        }

        public Builder WithIsHardened(bool statement) {
            if (statement) {
                this.isHardened = true;
            }
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

        public Builder WithEffectItem(EffectItem effectItem) {
            this.effectItem = effectItem;
            return this;
        }

        public Block Build() {
            // Required arguments
            if (position is null || image is null) {
                throw new ArgumentException(
                    "A brick must have a position and a image");
            }
            if (this.alterImage == null) {
                this.alterImage = this.image;
            }

            return new Block(this);
        }
    }
}