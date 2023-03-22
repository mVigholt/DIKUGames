using NUnit.Framework;
using Galaga;
using Galaga.Squadron;
using DIKUArcade.GUI;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;

namespace GalagaTests;

[TestFixture]
public class TestSquadron {
    private ISquadron squadron;
    private Enemy enemy;

    [SetUp]
    public void InitiateSquadron() {
        Window.CreateOpenGLContext();
        List<Image> blueEnemyStride = ImageStride.CreateStrides(
            4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        List<Image> greenEnemyStride = ImageStride.CreateStrides(
            2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png"));
        List<Image> redEnemyStride = ImageStride.CreateStrides(
            2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
        int milliseconds = 80;
        enemy = new Enemy(new Vec2F(0.1f, 0.9f),
                new ImageStride(milliseconds, blueEnemyStride),
                new ImageStride(milliseconds, redEnemyStride));
        squadron = new RowSquadron(
                    blueEnemyStride,
                    redEnemyStride,
                    new int[,] {
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                        { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                        { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    });
        }

        [Test]
        public void TestSquadronMove(){

        }
}