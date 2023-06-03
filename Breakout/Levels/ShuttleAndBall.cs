namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Graphics;
using Breakout.IO;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class ShuttleAndBall {
    public EntityContainer<Ball> balls {
        get; set;
    }
    public Shuttle shuttle {
        get; set;
    }

    public ShuttleAndBall() {
        InitShuttle();
        InitBall();
    }

    public Vec2F BallPosOnShuttle() {
        return new Vec2F(
            shuttle.GetPosition().X + shuttle.GetExtent().X / 2 - Ball.STD_EXTEND.X / 2,
            shuttle.GetPosition().Y + shuttle.GetExtent().Y / 2);
    }

    private void InitBall() {
        IBaseImage ballImage = new Image(
            Path.Combine(PathFinder.Images(), "ball.png"));
        balls = new EntityContainer<Ball>();
        balls.AddEntity(new Ball(BallPosOnShuttle(), ballImage));
    }

    private void InitShuttle() {
        Vec2F playerPosition = new Vec2F(0.5f - Shuttle.STD_EXTEND.X / 2, 0.03f);
        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );
        shuttle = Shuttle.NewShuttle(playerPosition, image);
    }

    public void Render() {
        balls.RenderEntities();
        shuttle.Render();
    }
    public void Move() {
        shuttle.Move();
        foreach (Ball ball in balls) {
            ball.Move();
            //let the ball follow the shuttle until released
            if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                ball.Shape.SetPosition(this.BallPosOnShuttle());
            }
        }
    }

    public void ballVsShuttleCollide() {
        balls.Iterate(ball => {
            CollisionData ballVsShuttle =
                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), shuttle.Shape);

            if (ballVsShuttle.Collision) {
                ball.UpdateDirection(shuttle.GetDirection(), ballVsShuttle.CollisionDir);
            }
        });

    }
}


