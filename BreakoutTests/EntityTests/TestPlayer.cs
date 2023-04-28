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

    private Image playerImage;
    private Player player;
    private int startingHealth;
    private Vec2F POSITION = new Vec2F(0.4f, 0.1f);

    [SetUp]
    public void InitiatePlayer(){
        Window.CreateOpenGLContext();
        playerImage = Assets.LoadImage("player.png");
        player = new Player(
            new DynamicShape(POSITION, new Vec2F(0.15f, 0.03f)),
            playerImage, 3);
    }

    [Test]
    public void TestMoveLeft() {
        player.SetMoveLeft(true);
        for (int i = 0; i < 1000; i++) {
            player.Move();
        }
        Assert.LessOrEqual(player.GetPosition().X, 1);
        Assert.GreaterOrEqual(player.GetPosition().X, 0);
    }
}



