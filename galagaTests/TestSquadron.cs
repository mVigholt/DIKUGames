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

    [SetUp]
    public void InitiateSquadron() {
        Window.CreateOpenGLContext();
        squadron = new RowSquadron(
            Assets.blueEnemyStride,
            Assets.redEnemyStride
        );
    }

    [Test]
    public void TestSquadronNotEmpty(){
        Assert.IsTrue(squadron.Enemies.CountEntities() > 0);
    }
}