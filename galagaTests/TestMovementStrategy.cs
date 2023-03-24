using NUnit.Framework;
using DIKUArcade.GUI;
using Galaga.MovementStrategy;
using Galaga;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
using System.Collections.Generic;
using System;

namespace GalagaTests;
[TestFixture]
public class TestMovementStrategy {
    private IMovementStrategy movementStrategy;
    private Enemy enemy;

    [SetUp]
    public void InitiateMovementStrategy() {
        Window.CreateOpenGLContext();
       
        int milliseconds = 80;
        enemy = new Enemy(new Vec2F(0.1f, 0.9f),
                new ImageStride(milliseconds, Assets.blueEnemyStride),
                new ImageStride(milliseconds, Assets.redEnemyStride));
    }

    [Test]
    public void DownTest(){
        Vec2F posBefore = enemy.Shape.Position.Copy();
        movementStrategy = new Down(enemy);
        movementStrategy.MoveEnemy(enemy);
        Assert.AreEqual(posBefore.X, enemy.Shape.Position.X);
        Assert.AreEqual(posBefore.Y - enemy.Speed, enemy.Shape.Position.Y);
    }

    [Test]
    public void NoMoveTest(){
        Vec2F posBefore = enemy.Shape.Position.Copy();
        movementStrategy = new NoMove(enemy);
        movementStrategy.MoveEnemy(enemy);
        Assert.AreEqual(posBefore.X, enemy.Shape.Position.X);
        Assert.AreEqual(posBefore.Y, enemy.Shape.Position.Y);
    }

    [Test]
    public void ZigZagDown(){
        Vec2F posBefore = enemy.Shape.Position.Copy();
        Vec2F startPos = enemy.startPosition.Copy();
        Vec2F curPos = enemy.Shape.Position;
        movementStrategy = new ZigZagDown(enemy);
        movementStrategy.MoveEnemy(enemy);
        float p = 0.045f;   //period / wavelength
        float a = 0.05f;    //amplitude
        Assert.AreEqual(startPos.X +(float)(a * Math.Sin(2 * Math.PI * (startPos.Y - curPos.Y)/p)),
            curPos.X);
        Assert.AreEqual(posBefore.Y - enemy.Speed, enemy.Shape.Position.Y);
    }
}
