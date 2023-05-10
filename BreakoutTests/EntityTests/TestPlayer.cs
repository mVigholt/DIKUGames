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
using Breakout.Entities;
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
    private readonly float SPEED = 0.01f;

    [SetUp]
    public void InitiatePlayer() {
        Window.CreateOpenGLContext();

        playerImage = Assets.LoadImage("player.png");
        Vec2F pos = new Vec2F(START_POS, 0.1f);
        playerShape = new DynamicShape(
            pos, new Vec2F(0.15f, 0.03f)
        );
        eventBus = GameBus.GetBus();

        player = new Player(pos, playerImage);

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
    /// Print a debug message if that is not the case.
    /// </summary>
    private bool AreAlmostEqual(float a, float b) {
        float max_allowed_diff = 0.000001f;
        float diff = Math.Abs(a - b);
        bool almostEqual = diff < max_allowed_diff;
        if (!almostEqual) {
            Console.WriteLine(
                $"|a - b| < {max_allowed_diff} => \n" +
                $"|{a} - {b}| < {max_allowed_diff} => \n" +
                $"{diff} < {max_allowed_diff} => false");
        }
        return almostEqual;
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    [TestCase(30000)]
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
        // after moving [moveCount * MOVEMENT_SPEED] times.
        Assert.IsTrue(IsWithinBounds(player));
        // Postcondition R': Player's updated x position
        // should be moveCount * MOVEMENT_SPEED + START_POS
        float expectedXPos = Math.Min(
            START_POS + SPEED * moveCount, 
            1f + SPEED - player.GetExtent().X
        );
        string msg = $"TestCase({moveCount}): {expectedXPos}, {player.GetPosition().X}";
        Assert.That(AreAlmostEqual(expectedXPos, player.GetPosition().X), msg);
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
        // Precondition R: Player is not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
        for (int i = 0; i < moveCount; i++) {
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
                .WithAction(KeyboardAction.KeyRelease)
                .Build());
            eventBus.ProcessEventsSequentially();
        
        }
        // Precondition P: Player is not out of bounds
        // after moving [moveCount * (-MOVEMENT_SPEED)] times.
        Assert.IsTrue(IsWithinBounds(player));
        // Postcondition R': Player's updated x position
        // should be moveCount * (-MOVEMENT_SPEED) + START_POS,
        // unless that is out of bounds.
        float expectedXPos = Math.Max(
            START_POS + (-SPEED) * moveCount, 0f
        );
        Assert.That(AreAlmostEqual(expectedXPos, player.GetPosition().X));
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
            player.Move();
            eventBus.RegisterEvent(new EventBuilder()
                .WithType(GameEventType.PlayerEvent)
                .WithKey(KeyboardKey.Left)
                .WithAction(KeyboardAction.KeyRelease)
                .Build());
            eventBus.ProcessEventsSequentially();
        }
        // Postcondition: Player is still not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
    }

    [Test]
    public void PlayerIsEntity() {
        // Requirement 5: Player must be a DIKU entity
        Assert.That(player is Entity);
    }
}



