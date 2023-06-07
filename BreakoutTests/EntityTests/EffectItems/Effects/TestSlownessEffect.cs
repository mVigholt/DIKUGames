namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using Breakout.GameStates;
using Breakout.Entities;


[TestFixture]
public class TestSlownessEffect {

    GameRunning gameRunning = GameRunning.GetInstance(true);
    readonly float SCALAR = 0.5f;
    Shuttle shuttle;

    [SetUp]
    public void Setup() {
        shuttle = EntityCreator.CreateShuttle();
    }

    [Test]
    public void TestActivate() {
        float initialSpeed = shuttle.Speed;
        EffectSimulator.Activate("Slowness");
        Assert.AreEqual(initialSpeed * SCALAR, shuttle.Speed);
    }

    [Test]
    public void TestDeactivate() {
        float initialSpeed = shuttle.Speed;
        EffectSimulator.Activate("Slowness");
        EffectSimulator.Deactivate("Slowness");
        Assert.AreEqual(initialSpeed, shuttle.Speed);
    }
}