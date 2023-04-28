namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.IO;
using System;
using System.IO;


public class TestBlock {

    [SetUp]
    public void SetUp() {
        Window.CreateOpenGLContext();
    }

    [Test]
    public void TestMissingAssets() {
        string existingImage = "brown-block.png";
        string nonexistentImage = "kSSg0J8sDhJpB";
    }
}