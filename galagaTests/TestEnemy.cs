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

    private List<Image> blueEnemyStride;
    private List<Image> redEnemyStride;

    [SetUp]
    public void SetUp() {
        Window.CreateOpenGLContext();
        blueEnemyStride = ImageStride.CreateStrides(
            4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        redEnemyStride = ImageStride.CreateStrides(
            2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
    }

    // Vec2F does not have an equals method
    private bool Vec2FEquals(Vec2F a, Vec2F b) {
        return a.GetHashCode() == b.GetHashCode();
    }

    [Test]
    public void TestMovement() {
        Vec2F pos = new Vec2F(1f, 1f);
        float speed = -1f; // Move down
        int millis = 80;
        Enemy enemy = new Enemy(
            pos,
            speed,
            new ImageStride(millis, blueEnemyStride),
            new ImageStride(millis, redEnemyStride));
        IMovementStrategy movementStrategy = new Down(enemy);
        movementStrategy.MoveEnemy(enemy);
        Assert.That(Vec2FEquals(new Vec2F(1f, 2f), enemy.Shape.Position));
    }
}