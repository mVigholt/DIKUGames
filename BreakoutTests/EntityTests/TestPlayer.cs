namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout;
using Breakout.BreakoutEntities;
using Breakout.IO;
using DIKUArcade.Events;
using Breakout.Events;
using DIKUArcade.Input;

[TestFixture]
public class TestPlayer {
    private GameEventBus eventBus;
    private GameEvent playerEvent;
    private Image playerImage;
    private Player player;
    private DynamicShape playerShape;

    [SetUp]
    public void InitiatePlayer() {
        Window.CreateOpenGLContext();

        playerImage = Assets.LoadImage("player.png");
        playerShape = new DynamicShape(
            new Vec2F(0.4f, 0.1f),
            new Vec2F(0.15f, 0.03f));
        eventBus = GameBus.GetBus();

        player = new Player(playerShape, playerImage);

        eventBus.Subscribe(GameEventType.PlayerEvent, player);

    }
    private bool IsWithinBounds(Player player) {
        return player.GetPosition().X <= 1.0f &&
                player.GetPosition().X >= 0.0f;

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
        Assert.AreEqual(0.4f - 0.01f, player.GetPosition().X);
        // Precondition: Player is not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
    }

    // If I use for loop here, it will give precision problem.
    // [TestCase(1)]
    // [TestCase(2)]
    // [TestCase(3)]
    // public void TestMoveRight(int moveCount) {
    //     // for (int i = 0; i < moveCount; i++) {
    //     eventBus.RegisterEvent(new EventBuilder()
    //         .WithType(GameEventType.PlayerEvent)
    //         .WithKey(KeyboardKey.Right)
    //         .WithAction(KeyboardAction.KeyPress)
    //         .Build());
    //     eventBus.RegisterEvent(new EventBuilder()
    //         .WithType(GameEventType.PlayerEvent)
    //         .WithKey(KeyboardKey.Right)
    //         .WithAction(KeyboardAction.KeyRelease)
    //         .Build());
    //     // for (int i = 0; i < moveCount; i++) {
    //         eventBus.ProcessEventsSequentially();
    //         player.Move();
    // }

    // }
    //     Assert.AreEqual(0.40f + 0.01f * moveCount, player.GetPosition().X);
    //     // Precondition: Player is not out of bounds
    //     Assert.IsTrue(IsWithinBounds(player));
    // }

    [Test]
    public void TestMoveRight() {
        eventBus.RegisterEvent(new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Right)
            .WithAction(KeyboardAction.KeyPress)
            .Build());

        eventBus.ProcessEventsSequentially();
        player.Move();
        Assert.AreEqual(0.4f + 0.01f, player.GetPosition().X);
        // Precondition: Player is not out of bounds
        Assert.IsTrue(IsWithinBounds(player));
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
        Assert.AreEqual(0.40f - 0.01f * 3, player.GetPosition().X);
        Assert.IsTrue(IsWithinBounds(player));
    }

}



