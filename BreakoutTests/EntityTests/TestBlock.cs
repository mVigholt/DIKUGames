namespace BreakoutTests;

using System.Collections.Generic;
using System.IO;
using Breakout.Entities;
using Breakout.IO;
using DIKUArcade.Graphics;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using DIKUArcade.Math;
using NUnit.Framework;

[TestFixture]
public class TestBlock {

    private Image image;
    private Block block;

    [SetUp]
    public void SetUp() {
        Window.CreateOpenGLContext();
        image = Assets.LoadImage("green-block.png");
        block = new Block.Builder()
                    .WithImage(image)
                    .WithPosition(new Vec2F(0.5f, 0.5f))
                    .WithValue(1)
                    .Build();
    }


    [Test]
    public void TestIsDead() {
        Assert.False(block.IsDead());
        block.LoseHealth(5);
        Assert.True(block.IsDead());
    }
}