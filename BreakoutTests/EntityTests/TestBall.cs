namespace BreakoutTests.EntityTests;

using System;
using System.IO;
using Breakout.Entities;
using Breakout.IO;
using DIKUArcade.Graphics;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using DIKUArcade.Math;
using DIKUArcade.Physics;
using NUnit.Framework;

[TestFixture]
public class TestBall {
    private IBaseImage ballImage;

    [SetUp]
    public void SetUp() {
        ballImage = new Image(
            Path.Combine(PathFinder.Images(), "ball.png"));
    }
    
    private Vec2F UnitVector(Vec2F vector) {
        float hyp = (float) System.Math.Sqrt(System.Math.Pow(vector.X, 2) + System.Math.Pow(vector.Y, 2));
        hyp = hyp != 0 ? hyp : 1;
        return new Vec2F(vector.X / hyp, vector.Y / hyp);
    }


    [Test]
    public void TestBallAtPosition() {
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(0, 1));
        ball.Move();
        Vec2F expectedPos = new Vec2F(0.5f, 0.015f);
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }


    [Test]
    public void TestBallAfterManyMovesUpwards([Range(0, 10, 1)] int steps) {
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        for (int i = 0; i < steps; i++) {
            ball.UpdateDirection(new Vec2F(0, 1));
            ball.Move();
        }
        Vec2F expectedPos = new Vec2F(0.5f, 0.015f * steps);
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }
    [Test]
    public void TestBallAfterManyMovesLeft([Range(0, 10, 1)] int steps) {
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        for (int i = 0; i < steps; i++) {
            ball.UpdateDirection(new Vec2F(-1, 0));
            ball.Move();
        }
        Vec2F expectedPos = new Vec2F(0.5f - 0.015f * steps, 0.0f);
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }

    [Test]
    public void TestBallAfterManyMovesRight([Range(0, 10, 1)] int steps) {
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        for (int i = 0; i < steps; i++) {
            ball.UpdateDirection(new Vec2F(1, 0));
            ball.Move();
        }
        Vec2F expectedPos = new Vec2F(0.5f + 0.015f * steps, 0.0f);
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }


    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(20000)]
    public void TestBallWithinBound(int steps) {
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        for (int i = 0; i < steps; i++) {
            ball.UpdateDirection(new Vec2F(0, 1));
            ball.Move();
        }
        Assert.Greater(ball.GetPosition().X, 0.0f);
        Assert.Greater(ball.GetPosition().Y, 0.0f);
        Assert.LessOrEqual(ball.GetPosition().X, 1.0f);
        Assert.LessOrEqual(ball.GetPosition().Y, 1.0f);
    }
    [Test]
    public void TestBallCollisionFromUp(){
        //Check if the ball move upwards and hit some unmoved thing from up, then it's direction will change
        //from upward to downward, while the X direction is unchanged.
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        for (int i = 0; i < 10; i++) {
            ball.UpdateDirection(new Vec2F(0, 1));
            ball.Move();
        }
        ball.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirUp);
        ball.Move();
        Assert.Negative(ball.GetDirection().Y);
    }

    [Test]
    public void TestBallCollisionFromRight(){
        //Check if the ball move towards right and hit an entity from right, then it's direction will change
        //from going to the right to the left, while the Y direction is unchanged.
        Ball ball = new Ball(new Vec2F(0.5f, 0.1f), ballImage);
         //Move the ball upwards for 10 steps right
        for (int i = 0; i < 10; i++) {
            ball.UpdateDirection(new Vec2F(1, 1));
            ball.Move();
        }
        ball.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirRight);
        ball.Move();
        Assert.Negative(ball.GetDirection().X);
    }

    [Test]
    public void TestBallIsDeleted(){
        Ball ball = new Ball(new Vec2F(0.5f, 0.1f), ballImage);
         //Move the ball upwards for 10 steps right
        for (int i = 0; i < 10; i++) {
            ball.UpdateDirection(new Vec2F(0, -1));
            ball.Move();
        }
        Assert.IsTrue(ball.IsDeleted());

    }


}
