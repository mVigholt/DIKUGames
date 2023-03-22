namespace GalagaTests;

using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using Galaga;
using Galaga.MovementStrategy;


[TestFixture]
public class TestEnemy {

    private ImageStride image;
    private ImageStride alternativeImage;

    [SetUp]
    public void SetUp() {
        Window.CreateOpenGLContext();
        List<Image> stdImages = ImageStride.CreateStrides(
            4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        List<Image> altImages = ImageStride.CreateStrides(
            2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
        int millis = 80;
        image = new ImageStride(millis, stdImages);
        alternativeImage = new ImageStride(millis, altImages);
    }

    // Vec2F does not have an equals method
    private bool Vec2FEquals(Vec2F a, Vec2F b) {
        return a.GetHashCode() == b.GetHashCode();
    }

    [Test]
    public void TestMovement() {
        Enemy enemy = new EnemyBuilder()
            .WithPosition(new Vec2F(1f, 1f))
            .WithSpeed(-1f)
            .WithImage(image)
            .WithAlternativeImage(alternativeImage)
            .Build();
        IMovementStrategy movementStrategy = new Down(enemy);
        movementStrategy.MoveEnemy(enemy);
        Assert.That(Vec2FEquals(new Vec2F(1f, 2f), enemy.Shape.Position));
    }

    [Test]
    public void TestLoseHealth() {

    }
}