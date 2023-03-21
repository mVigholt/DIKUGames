using NUnit.Framework;
using DIKUArcade.GUI;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;

namespace GalagaTests;
[TestFixture]
public class TestPlayer{

    private Image playerImage;
    private Player player;
    [SetUp]
    public void InitiatePlayer(){
        Window.CreateOpenGLContext();
        playerImage = new Image(
            Path.Combine("..\\Galaga","Assets", "Images", "Player.png"));
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            playerImage);
    }

    [Test]
    public void TestPlayerLoseHealth([Range(0, 10, 1)] int hp){
        player.LoseHealth(hp);
        Assert.AreEqual(player.health.Points, 50 - hp);
    }

    [TestCase (50)]
    [TestCase (60)]
    public void TestPlayerIsDead(int loseHP){
        player.LoseHealth(loseHP);
        Assert.LessOrEqual(player.health.Points, 0);
        Assert.IsTrue(player.IsDead());
    }
}



