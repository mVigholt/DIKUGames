namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.GameStates;
using Breakout.Entities;
using Breakout;


[TestFixture]
public class TestHardBallEffect {

    readonly int NUM_MOVES = 2;
    readonly float SPEED = 0.1f;
    readonly Vec2F UP = new Vec2F(0f, 1f);
    GameEventBus eventBus = GameBus.GetBus();
    Shuttle shuttle;
    Block block;
    Vec2F ballStartingPos;
    Ball ball;

    [SetUp]
    public void Setup() {
        shuttle = EntityCreator.CreateShuttle();
        Vec2F blockPos = new Vec2F(0.5f, 0.8f);
        block = new Block.Builder()
            .WithImage(new NoImage())
            .WithPosition(blockPos)
            .Build();
        Vec2F ballPos = new Vec2F(
            0.5f + Ball.STD_EXTENT.X / 2f,
            blockPos.Y - NUM_MOVES * SPEED
        );
        ball = new Ball(ballPos, new NoImage());
    }

    [Test]
    public void TestBallBouncesBack() {
        ball.UpdateDirection(UP);
        for (int i = 0; i < NUM_MOVES + 1; i++) {
            ball.Move();
        }
        Assert.AreEqual(
            ball.GetDirection().X,
            UP.X * -1
        );
    }

    [Test]
    public void TestHardBallDoesntBounceBack() {
        
    }
}