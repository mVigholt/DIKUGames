namespace BreakoutTests;

using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using Breakout.IO;
using Breakout.Entities;

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
                    .WithPosition(new Vec2F(0.5f,0.5f))
                    .Build();
    }


    [Test]
    public void TestIsDead() {
        block.LoseHealth(5);
        Assert.False(block.IsDead());
        block.LoseHealth(5);
        Assert.True(block.IsDead());
    }
}