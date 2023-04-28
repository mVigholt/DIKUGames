namespace BreakoutTests;

using System.IO;
using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout;
using Breakout.BreakoutEntities;
using Breakout.IO;


[TestFixture]
public class TestPlayer{

    private Player player;
    private Vec2F START_POSITION = new Vec2F(0.4f, 0.1f);

    [SetUp]
    public void InitiatePlayer(){
        Window.CreateOpenGLContext();
        Image playerImage = Assets.LoadImage("player.png");
        player = new Player(
            new DynamicShape(START_POSITION, new Vec2F(0.15f, 0.03f)),
            playerImage, 3);
    }

    private bool IsOutOfBounds(Player player) {
        return  player.GetPosition().X <= 1 &&
                player.GetPosition().X >= 0;
    }

    [Test]
    public void TestMoveLeft() {
        player.SetMoveLeft(true);
        // Precondition: Player is not out of bounds
        Assert.False(!IsOutOfBounds(player));
        for (int i = 0; i < 1000; i++) {
            player.Move();
        }
        // Postcondition: Player is still not out of bounds
        Assert.False(!IsOutOfBounds(player));
    }

    [Test]
    public void PlayerIsEntity() {
        // R5: Player must be a DIKU entity
        Assert.That(player is Entity);
    }

    [Test]
    public void TestMoveRight() {
        player.SetMoveRight(true);
        // Precondition R3: Player is not out of bounds
        Assert.False(!IsOutOfBounds(player));
        for (int i = 0; i < 1000; i++) {
            player.Move();
        }
        // Postcondition R1: Player has moved
        // Postcondition R3: Player is still not out of bounds
        Assert.False(!IsOutOfBounds(player));
        Assert.AreNotEqual(player.GetPosition(), START_POSITION);
    }
}



