using NUnit.Framework;
using DIKUArcade.GUI;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace GalagaTests;
[TestFixture]
public class TestHealth {
    private Player? player;
    private Image? playerImage;
    private Health? health;
    [SetUp]

    public void InitiatePlayerWithHealth() {
        Window.CreateOpenGLContext();
        Vec2F position = new Vec2F(0.45f, 0.1f);
        Vec2F extent = new Vec2F(0.1f, 0.1f);
        int startingHealth = 50;
        health = new Health(position, extent, startingHealth);
    }
    [Test]
    public void TestHealthLost(){
        health?.LoseHealth(4);
        Assert.AreEqual(health?.Points, 50-4);
    }

    public void TestHealthMax(){
        Assert.AreEqual(health?.Max, 50);
        health?.LoseHealth(5);
        Assert.AreEqual(health?.Max, 50);
    }
}