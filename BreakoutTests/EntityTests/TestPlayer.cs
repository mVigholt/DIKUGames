namespace BreakoutTests;

using System;
using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout;
using Breakout.BreakoutEntities;
using Breakout.IO;
using Breakout.Events;

[TestFixture]
public class TestPlayer {
    private GameEventBus eventBus;
    private GameEvent playerEvent;
    private Image playerImage;
    private Player player;
    private DynamicShape playerShape;
    private readonly float START_POS = 0.4f;

    [SetUp]
    public void InitiatePlayer() {
        Window.CreateOpenGLContext();

        playerImage = Assets.LoadImage("player.png");
        playerShape = new DynamicShape(
            new Vec2F(START_POS, 0.1f),
            new Vec2F(0.15f, 0.03f));
        eventBus = GameBus.GetBus();

        player = new Player(playerShape, playerImage);

        eventBus.Subscribe(GameEventType.PlayerEvent, player);

    }

    /// <summary>
    /// Return true if the player is within the borders of the window.
    /// </summary>
    private bool IsWithinBounds(Player player) {
        return player.GetPosition().X <= 1.0f &&
                player.GetPosition().X >= 0.0f;
    }


    /// <summary>
    /// Return true if a is almost equal to b
    /// (difference is less than 1 / 1,000,000).
    /// </summary>
    private bool AreAlmostEqual(float a, float b) {
        float max_allowed_diff = 0.000001f;
        float diff = Math.Abs(a - b);
        return diff < max_allowed_diff;
    }


    [Test]
    public void TestMoveLeft() {
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Left)
            .WithAction(KeyboardAction.KeyPress)
            .Build());
        eventBus.ProcessEventsSequentially();
        player.Move();
        Assert.AreEqual(START_POS - player.MOVEMENT_SPEED, player.GetPosition().X);
        // Precondition: Player is not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
    }

    // If I use for loop here, it will give precision problem.
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    public void TestMoveRight(int moveCount) {
        // Precondition R: Player is not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
        for (int i = 0; i < moveCount; i++) {
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Right)
                .WithAction(KeyboardAction.KeyPress)
                .Build());
            eventBus.ProcessEventsSequentially();
            player.Move();
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Right)
                .WithAction(KeyboardAction.KeyRelease)
                .Build());
            eventBus.ProcessEventsSequentially();
        
        }
        // Precondition P: Player is not out of bounds
        // after moving (moveCount * movement speed) times.
        Assert.IsTrue(IsWithinBounds(player));
        // Postcondition R': Player's updated x position
        // should be (moveCount * movement speed + starting position).
        float expectedXPos = START_POS + player.MOVEMENT_SPEED * moveCount;
        Assert.That(AreAlmostEqual(expectedXPos, player.GetPosition().X));
    }

    [Test]
    public void TestMoveRight() {
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Right)
            .WithAction(KeyboardAction.KeyPress)
            .Build());

        eventBus.ProcessEventsSequentially();
        player.Move();
        Assert.IsTrue(IsWithinBounds(player));
        Assert.AreEqual(START_POS + player.MOVEMENT_SPEED, player.GetPosition().X);
        // Precondition: Player is not out of bounds
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Right)
            .WithAction(KeyboardAction.KeyRelease)
            .Build());
        eventBus.ProcessEventsSequentially();
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    public void TestMoveWithinBorder(int moveCount) {
        for (int i = 0; i < moveCount; i++) {
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Left)
                .WithAction(KeyboardAction.KeyPress)
                .Build());
            eventBus.ProcessEventsSequentially();
            player.Move();
        }
        Assert.AreEqual(0.0f, player.GetPosition().X);
        // Postcondition: Player is still not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
    }

    [Test]
    public void PlayerIsEntity() {
        // R5: Player must be a DIKU entity
        Assert.That(player is Entity);
    }

    public void TestMoveLeft2() {
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Left)
            .WithAction(KeyboardAction.KeyPress)
            .Build());
        eventBus.ProcessEventsSequentially();
        player.Move();
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Left)
            .WithAction(KeyboardAction.KeyPress)
            .Build());
        eventBus.ProcessEventsSequentially();
        player.Move();
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Left)
            .WithAction(KeyboardAction.KeyPress)
            .Build());
        eventBus.ProcessEventsSequentially();
        player.Move();
        Assert.AreEqual(START_POS - player.MOVEMENT_SPEED * 3, player.GetPosition().X);
        Assert.IsTrue(IsWithinBounds(player));
    }

}



