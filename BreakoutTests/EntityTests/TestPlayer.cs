using NUnit.Framework;
using DIKUArcade.GUI;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;

namespace BreakoutTests;
[TestFixture]
public class TestPlayer{

    private Image playerImage;
    private Player player;
    private int startingHealth;
    [SetUp]
    public void InitiatePlayer(){
        Window.CreateOpenGLContext();
        playerImage = new Image(
            Path.Combine("..", "Breakout","Assets", "Images", "player.png"));
        player = new Player(
            new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.15f, 0.03f)),
            playerImage, 3);
        playerEvent = new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Up)
            .WithAction(KeyboardAction.KeyPress)
            .WithString("Hello")
            .Build();
    }

    [Test]
    public void TestMoveLeft() {
        EventDTO dto = new EventDTO(playerEvent);
        Assert.AreEqual(GameEventType.PlayerEvent, dto.Type);
        Assert.AreEqual(KeyboardKey.Up, dto.Key.Value);
        Assert.AreEqual(KeyboardAction.KeyPress, dto.Action.Value);
        Assert.AreEqual("Hello", dto.DebugString);
    }
}



