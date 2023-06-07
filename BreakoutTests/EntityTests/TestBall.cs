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
        // Precondition P: Ball is not out of boundary
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(0, 1));
        ball.Move();
        Vec2F expectedPos = new Vec2F(0.5f, 0.015f);
        //Postcondition R': Ball is till within the boundary
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }


    [Test]
    public void TestBallAfterManyMovesUpwards([Range(0, 10, 1)] int steps) {
        // Precondition P: Ball is not out of boundary
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(0, 1));
        for (int i = 0; i < steps; i++) {
            ball.Move();
        }
        Vec2F expectedPos = new Vec2F(0.5f, 0.015f * steps);
        // Postcondition R': Ball is still within boundary
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }
    [Test]
    public void TestBallAfterManyMovesLeft([Range(0, 10, 1)] int steps) {
        // Precondition P: Ball is not out of boundary
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(-1, 0));
        var normDir = ball.GetDirection();
        for (int i = 0; i < steps; i++) {
            ball.Move();
        }
        Vec2F expectedPos = new Vec2F(0.5f - 0.015f * steps, 0.0f);
        // Postcondition R': Ball is still within boundary
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }

    [Test]
    public void TestBallAfterManyMovesRight([Range(0, 10, 1)] int steps) {
        // Precondition P: Ball is not out of boundary
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(1, 0));
        for (int i = 0; i < steps; i++) {
            ball.Move();
        }
        Vec2F expectedPos = new Vec2F(0.5f + 0.015f * steps, 0.0f);
        // Postcondition R': Ball is still within boundary
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.X, ball.GetPosition().X));
        Assert.That(FloatComparer.AreAlmostEqual(expectedPos.Y, ball.GetPosition().Y));
    }


    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(20000)]
    public void TestBallWithinBound(int steps) {
        // Precondition P: Ball is not out of boundary
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(1, 1));
        for (int i = 0; i < steps; i++) {
            ball.Move();
        }
        // Postcondition R': Ball is at the bounary
        Assert.GreaterOrEqual(ball.GetPosition().X, 0.0f);
        Assert.GreaterOrEqual(ball.GetPosition().Y, 0.0f);
        Assert.LessOrEqual(ball.GetPosition().X, 1.0f);
        Assert.LessOrEqual(ball.GetPosition().Y, 1.0f);
    }
    [Test]
    //Check if the ball move upwards and hit some unmoved thing from up, then it's direction will change
    //from upward to downward, while the X direction is unchanged.
    public void TestBallCollisionFromUp(){
        //Precondition P: Ball is within boundary and move upwards,
        // The ball hits an unmoveable entity.
        Ball ball = new Ball(new Vec2F(0.5f, 0.0f), ballImage);
        ball.UpdateDirection(new Vec2F(0, 1));
        for (int i = 0; i < 10; i++) {
            ball.Move();
        }
        ball.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirUp);
        ball.Move();
        //Postcondition R': Ball is still within boundary and X direction is unchanged
        Assert.Negative(ball.GetDirection().Y);
    }

    [Test]
    //Check if the ball move towards right and hit an entity from right,
    //then it's direction will change from going to the right to the left,
    //while the Y direction is unchanged.
    public void TestBallCollisionFromRight(){
        // Precondition P: Ball is within boundary, and move towards right
        // The ball hits an unmoveable entity.
        Ball ball = new Ball(new Vec2F(0.5f, 0.1f), ballImage);
         //Move the ball for 10 steps right and up
        ball.UpdateDirection(new Vec2F(1, 1));
        for (int i = 0; i < 10; i++) {
            ball.Move();
        }
        ball.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirRight);
        ball.Move();
        //Postcondition R': Ball is still within boundary and Y direction is unchanged
        Assert.Negative(ball.GetDirection().X);
    }

    [Test]
    public void TestBallIsDeleted(){
        // Precondition P: Ball is moving downwards and within boundary
        Ball ball = new Ball(new Vec2F(0.5f, 0.1f), ballImage);
         //Move the ball downwards for 10 steps
        ball.UpdateDirection(new Vec2F(0, -1));
        for (int i = 0; i < 10; i++) {
            ball.Move();
        }
        //Postcondition R': Ball is marked as isDeleted and the entity will
        //not be rendered.
        Assert.IsTrue(ball.IsDeleted());

    }


}
