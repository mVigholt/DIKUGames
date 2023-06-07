namespace BreakoutTests.EntityTests;

using System;
using Breakout;
using Breakout.Entities;
using Breakout.Events;
using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using NUnit.Framework;

[TestFixture]
public class TestShuttle {
    private GameEventBus eventBus;
    private IBaseImage playerImage;
    private Shuttle shuttle;
    private readonly float START_POS = 0.4f;
    private readonly float SPEED = 0.02f;

    [SetUp]
    public void InitiatePlayer() {
        playerImage = Assets.LoadImage("player.png");
        Vec2F pos = new Vec2F(START_POS, 0.1f);
        eventBus = GameBus.GetBus();
        shuttle = Shuttle.NewShuttle(pos, playerImage);
        shuttle.Speed = SPEED;
    }

    /// <summary>
    /// Return true if the shuttle is within the borders of the window.
    /// </summary>
    private bool IsWithinBounds(Shuttle shuttle) {
        return shuttle.GetPosition().X <= 1.0f &&
                shuttle.GetPosition().X >= 0.0f;
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    [TestCase(3000)]
    public void TestMoveRight(int moveCount) {
        // Precondition R: Shuttle is not out of bounds
        Assert.IsTrue(IsWithinBounds(shuttle));
        for (int i = 0; i < moveCount; i++) {
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Right)
                .WithAction(KeyboardAction.KeyPress)
                .Build());
            eventBus.ProcessEventsSequentially();
            shuttle.Move();
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Right)
                .WithAction(KeyboardAction.KeyRelease)
                .Build());
            eventBus.ProcessEventsSequentially();

        }
        // Precondition P: Shuttle is not out of bounds
        // after moving [moveCount * MOVEMENT_SPEED] times.
        Assert.IsTrue(IsWithinBounds(shuttle));
        // Postcondition R': Shuttle's updated x position
        // should be moveCount * MOVEMENT_SPEED + START_POS
        float expectedXPos = Math.Min(
            START_POS + SPEED * moveCount,
            1f - shuttle.GetExtent().X
        );
        string msg = $"TestCase({moveCount}): {expectedXPos}, {shuttle.GetPosition().X}";
        Assert.That(FloatComparer.AreAlmostEqual(expectedXPos, shuttle.GetPosition().X), msg);
    }

    /// <summary>
    /// Copy of TestMoveRight, only the event keys have changed
    /// </summary>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    [TestCase(30000)]
    public void TestMoveLeft(int moveCount) {
        // Precondition R: Shuttle is not out of bounds
        Assert.IsTrue(IsWithinBounds(shuttle));
        for (int i = 0; i < moveCount; i++) {
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Left)
                .WithAction(KeyboardAction.KeyPress)
                .Build());
            eventBus.ProcessEventsSequentially();
            shuttle.Move();
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Left)
                .WithAction(KeyboardAction.KeyRelease)
                .Build());
            eventBus.ProcessEventsSequentially();

        }
        // Precondition P: Shuttle is not out of bounds
        // after moving [moveCount * (-MOVEMENT_SPEED)] times.
        Assert.IsTrue(IsWithinBounds(shuttle));
        // Postcondition R': Shuttle's updated x position
        // should be moveCount * (-MOVEMENT_SPEED) + START_POS,
        // unless that is out of bounds.
        float expectedXPos = Math.Max(
            START_POS + -SPEED * moveCount, 0f
        );
        Assert.That(FloatComparer.AreAlmostEqual(expectedXPos, shuttle.GetPosition().X));
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    [TestCase(30000)]
    public void TestMoveWithinBorder(int moveCount) {
        for (int i = 0; i < moveCount; i++) {
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Left)
                .WithAction(KeyboardAction.KeyPress)
                .Build());
            eventBus.ProcessEventsSequentially();
            shuttle.Move();
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Left)
                .WithAction(KeyboardAction.KeyRelease)
                .Build());
            eventBus.ProcessEventsSequentially();
        }
        // Postcondition: Shuttle is still not out of bounds
        Assert.IsTrue(IsWithinBounds(shuttle));
    }

    [Test]
    public void PlayerIsEntity() {
        // Requirement 5: Shuttle must be a DIKU entity
        Assert.That(shuttle is Entity);
    }
}



