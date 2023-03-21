using NUnit.Framework;
using DIKUArcade.GUI;
using Galaga;
using DIKUArcade.Math;

namespace GalagaTests;
[TestFixture]
public class TestHealth {
    private Health? health;
    [SetUp]

    public void InitiateHealth() {
        Window.CreateOpenGLContext();
        Vec2F position = new Vec2F(0.45f, 0.1f);
        Vec2F extent = new Vec2F(0.1f, 0.1f);
        int startingHealth = 50;
        health = new Health(position, extent, startingHealth);
    }
    [Test]
    public void TestHealthLost([Range(0, 10, 1)] int lostPoint){
        health?.LoseHealth(lostPoint);
        Assert.AreEqual(health?.Points, 50-lostPoint);
    }

    [Test]
    public void TestHealthMax(){
        Assert.AreEqual(health?.Max, 50);
        health?.LoseHealth(5);
        Assert.AreEqual(health?.Max, 50);
    }
}