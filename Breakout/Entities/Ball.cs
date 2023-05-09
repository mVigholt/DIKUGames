namespace Breakout.Entities;

using Breakout.Events;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class Ball : MoveableEntity {
    private GameEventBus eventBus;


    public int Level {
        get;
        internal set;
    }
    // float ballDiameter = 0.03f;
    // float ballRadius = ballDiameter / 2;
    // Vec2F ballExtent = new Vec2F(ballDiameter, ballDiameter);
    public Ball(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, new Vec2F(0.03f, 0.03f)), image, 0.01f) {
        this.Level = 0;
    }

    public override void Move() {
        Vec2F nullSpeed = new Vec2F(0, 0);

        if (this.GetPosition().X < 0.0f) {
            this.ChangeDir(CollisionDirection.CollisionDirRight, nullSpeed);
        }
        if (this.GetPosition().X > 1.0f - this.GetExtent().X) {
            this.ChangeDir(CollisionDirection.CollisionDirLeft, nullSpeed);
        }
        if (this.GetPosition().Y > 1.0f - this.GetExtent().Y){
            this.ChangeDir(CollisionDirection.CollisionDirDown, nullSpeed);
        }
        if (this.GetPosition().Y < 0.0f){
            this.DeleteEntity();
        }
        // base.Move();
        this.shape.Move();
    }
}

//     public void ChangeSpeed(CollisionDirection dir, Vec2F CollidingShapeDir) {
//         // static Vec2F VectorCal(Vec2F v1, Vec2F v2) {
//         //     return new Vec2F(v1.X + v2.X, v1.Y + v2.Y);
//         // }
//         switch (dir) {
//             case CollisionDirection.CollisionDirUp:
//                 System.Console.WriteLine("ball moveDown: " + moveDown);
//                 SetMoveUp(false);
//                 SetMoveDown(true);
//                 System.Console.WriteLine("ball moveDown: " + moveDown);
//                 addDirection((CollidingShapeDir));
//                 // Move();
//                 break;
//             case CollisionDirection.CollisionDirDown:
//                 SetMoveUp(true);
//                 SetMoveDown(false);
//                 addDirection((CollidingShapeDir));
//                 // Move();
//                 break;
//             case CollisionDirection.CollisionDirLeft:
//                 SetMoveLeft(false);
//                 SetMoveRight(true);
//                 addDirection((CollidingShapeDir));
//                 // Move();
//                 break;
//             case CollisionDirection.CollisionDirRight:
//                 SetMoveLeft(true);
//                 SetMoveRight(false);
//                 addDirection((CollidingShapeDir));
//                 // Move();
//                 break;
//             default:
//                 break;
//         }
//     }
// }