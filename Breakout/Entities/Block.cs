namespace Breakout.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities.EffectItems;
using DIKUArcade.Physics;

///<summary>
///Create different kinds of blocks.
///Since the block has a property moveable, so this class also inherits
///MoveableEntity class.
///This class uses a builder design model, which allows
///a block can have two properties at the same time. I.e. a block can be
///both moveable and hardened. The effect can be seen in level 4.
///</summary>
public class Block : MoveableEntity {
    public static readonly Vec2F STD_EXTENT = new Vec2F(0.083f, 0.041f);
    public static readonly float BLOCK_SPEED = 0.007f;
    public Builder build {
        get; private set;
    }

    private int maxHealth = 1;
    public int Health { get; private set; } = 1;

    private Block(Block.Builder builder)
        : base(new DynamicShape(builder.position, STD_EXTENT), builder.image) {
        build = builder;
        if (build.isHardened) {
            maxHealth *= 2;
        }
        if (build.isMoveable) {
            shape.Direction = (new Vec2F(BLOCK_SPEED, 0));
        }
        Health = maxHealth;
    }

    public int Value {
        get {
            return build.value;
        }
    }

    ///<summary>
    ///When the health of the block is below 0, it is dead
    ///</summary>
    public bool IsDead() {
        return Health <= 0;
    }


    ///<summary>
    /// A function to deduct health from a block (when the ball
    /// hits it). If the block is dead, then its entity will be
    /// marked as deleted.
    /// When a block's health is halved, the image will change
    ///</summary>
    public void LoseHealth(int hp) {
        if (!build.isUnbreakable) {
            Health -= hp;
        }

        if (IsDead()) {
            DeleteEntity();
        } else if (hp <= maxHealth / 2) {
            Image = build.alterImage;
        }
    }



    ///<summary>
    ///When the block is moveable, it will be given a speed.
    ///The block is also ensured to not go out of the boundary.
    ///When it hits the wall boundary, it will change to an opposite direction
    ///</summary>
    public override void Move() {
        if (build.isMoveable) {
            Speed = BLOCK_SPEED;
            if (GetPosition().X == 0.0f) {
                UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirRight);
            }
            if (GetPosition().X == 1.0f - GetExtent().X) {
                UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirLeft);
            }
            base.Move();
        }
    }

    ///<summary>
    ///The builder class used to create different blocks.
    ///The property method will receive a boolean to check if the block has
    ///the property. While others receive needed data to create a block entity
    ///</summary>
    public class Builder {
        public Vec2F position;
        public IBaseImage image;
        public IBaseImage alterImage;
        public bool isUnbreakable = false;
        public bool isHardened = false;
        public bool isMoveable = false;
        public int value;
        public EffectItem effectItem;

        public Builder() {
        }

        public Builder WithMoveable(bool statement) {
            isMoveable = statement;
            return this;
        }
        public Builder WithIsUnbreakable(bool statement) {
            isUnbreakable = statement;
            return this;
        }

        public Builder WithIsHardened(bool statement) {
            isHardened = statement;
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
            if (alterImage == null) {
                alterImage = image;
            }

            return new Block(this);
        }
    }
}