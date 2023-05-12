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
    private Block normalBlock;
    private Block hardenedBlock;
    private Block unbreakableBlock;

    [SetUp]
    public void SetUp() {
        Window.CreateOpenGLContext();
        image = Assets.LoadImage("green-block.png");
        normalBlock = new Block.Builder()
                    .WithImage(image)
                    .WithPosition(new Vec2F(0.5f, 0.5f))
                    .WithValue(1)
                    .WithIsHardened(false)
                    .WithIsUnbreakable(false)
                    .Build();
        hardenedBlock = new Block.Builder()
                    .WithImage(image)
                    .WithPosition(new Vec2F(0.5f, 0.5f))
                    .WithValue(1)
                    .WithIsHardened(true)
                    .WithIsUnbreakable(false)
                    .Build();
        unbreakableBlock = new Block.Builder()
                    .WithImage(image)
                    .WithPosition(new Vec2F(0.5f, 0.5f))
                    .WithValue(1)
                    .WithIsHardened(false)
                    .WithIsUnbreakable(true)
                    .Build();
    }


    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public void TestIsDead(int hp) {
        Assert.False(normalBlock.IsDead());
        Assert.False(hardenedBlock.IsDead());
        Assert.False(unbreakableBlock.IsDead());

        normalBlock.LoseHealth(hp);
        hardenedBlock.LoseHealth(hp);
        unbreakableBlock.LoseHealth(hp);
        
        if (hp <= 1) {
            Assert.True(normalBlock.IsDead());
            Assert.False(hardenedBlock.IsDead());
            Assert.False(unbreakableBlock.IsDead());
        } else {
            Assert.True(normalBlock.IsDead());
            Assert.True(hardenedBlock.IsDead());
            Assert.False(unbreakableBlock.IsDead());
        }
    }
}