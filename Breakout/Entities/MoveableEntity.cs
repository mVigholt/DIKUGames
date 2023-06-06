namespace Breakout.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

///<summary>
/// The MoveableEntity class inherits Entity class. This is
/// a super class for all moveable entities, such as ball,
/// shuttle and a moveable block.
/// When a sub class is a potential moveable entity such as
/// Block, it can also use the constructor which has a default
/// speed as zero.
///</summary>
public class MoveableEntity : Entity {

    protected DynamicShape shape;

    private Vec2F dir = new Vec2F(0, 0);

    public float Speed {
        get; set;
    }

    public MoveableEntity(DynamicShape shape, IBaseImage image) :
         this(shape, image, 0.0f) {
    }

    public MoveableEntity(DynamicShape shape, IBaseImage image, float speed)
        : base(shape, image) {
        this.shape = shape;
        this.Speed = speed;
    }

    ///<summary>Return the shape position</summary>
    public Vec2F GetPosition() {
        return shape.Position.Copy();
    }

    public Vec2F GetDirection() {
        return shape.Direction.Copy();
    }

    public Vec2F GetExtent() {
        return shape.Extent.Copy();
    }

    public void Render() {
        RenderEntity();
    }


    ///<summary>Normalize a vector to ensure the speed will not
    ///change, but only the direction will change </summary>
    ///<param name = "vector"> The input vector</param>
    ///<return>The normalized vector</return>
    private Vec2F UnitVector(Vec2F vector) {
        float hyp = (float) Math.Sqrt(
            Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        hyp = hyp != 0 ? hyp : 1;
        return new Vec2F(vector.X / hyp, vector.Y / hyp);
    }

    ///<summary>When the collision direction is unchecked,
    ///you can use this method to update the vector's direction by
    ///just add another vector</summary>
    ///<param name = "addVector"> The vector to be added to the moveable
    ///entity</param>
    ///<return>no return</return>
    public void UpdateDirection(Vec2F addVector) {
        UpdateDirection(addVector, CollisionDirection.CollisionDirUnchecked);
    }

    ///<summary>When an entity hits another entity from left side
    ///or right side, the entity's X direction changes, but Y direction
    ///is unchanged. The same rule applies to up and down side.
    ///Meanwhile, if the hit entity is also moveable, then it will
    ///affect another entity by adding another vector to the about
    /// to be changed entity, in most case will be the ball in the game.
    ///Since the speed will not change, the vector is normalized.
    ///Furthurmore, if the ball bounce up and down or left and right
    ///in endless loop, a random vector is added to break this situation</summary>
    ///<param name = "addVector"> The vector to be added to the moveable
    ///entity </param>
    ///<return>no return </return>

    public void UpdateDirection(Vec2F addVector, CollisionDirection colDir) {
        switch (colDir) {
            case CollisionDirection.CollisionDirLeft:
            case CollisionDirection.CollisionDirRight:
                dir.X *= -1;
                break;
            case CollisionDirection.CollisionDirUp:
            case CollisionDirection.CollisionDirDown:
                dir.Y *= -1;
                break;
            default:
                break;
        }
        dir = UnitVector(UnitVector(dir) + UnitVector(addVector));
        //entities cannot bounce back and forth and an endless loop
        if (colDir != CollisionDirection.CollisionDirUnchecked &&
            (dir.X == 0 || dir.Y == 0)) {
            var rand = new System.Random().Next(-1, 2);
            if (dir.X == 0) {
                dir = UnitVector(UnitVector(dir) + new Vec2F(rand, 0));
            } else if (dir.Y == 0) {
                dir = UnitVector(UnitVector(dir) + new Vec2F(rand, 0));
            }
        }
        shape.ChangeDirection(new Vec2F(Speed * dir.X, Speed * dir.Y));
    }

    ///<summary>
    /// To stop the entity to move by neutralize its direction.
    /// This method is used when the game is from GameRunning to
    /// GamePaused, the entity will stop to move.
    ///</summary>
    public void Stop() {
        UpdateDirection(-1 * GetDirection());
    }

    protected Vec2F MinCorner() {
        return new Vec2F(0.0f, 0.0f);
    }

    protected Vec2F MaxCorner() {
        return new Vec2F(1.0f - shape.Extent.X, 1.0f - shape.Extent.Y);
    }

    ///<summary>
    /// To keep the moveable entity inside the boundary.
    /// It can be override as needed
    ///</summary>
    virtual public void Move() {
        shape.Move();
        if (shape.Position.X < MinCorner().X) {
            shape.Position.X = MinCorner().X;
        }
        if (shape.Position.X > MaxCorner().X) {
            shape.Position.X = MaxCorner().X;
        }
        if (shape.Position.Y < MinCorner().Y) {
            shape.Position.Y = MinCorner().Y;
        }
        if (shape.Position.Y > MaxCorner().Y) {
            shape.Position.Y = MaxCorner().Y;
        }
    }
}