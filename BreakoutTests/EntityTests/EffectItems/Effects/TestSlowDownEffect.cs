namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.GameStates;
using Breakout.Entities;
using Breakout.Entities.EffectItems;


[TestFixture]
public class TestSlowDownEffect {

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
        EffectSimulator.Activate("SlowDown");
        Assert.AreEqual(initialSpeed * SCALAR, shuttle.Speed);
    }

    [Test]
    public void TestDeactivate() {
        float initialSpeed = shuttle.Speed;
        EffectSimulator.Activate("SlowDown");
        EffectSimulator.Deactivate("SlowDown");
        Assert.AreEqual(initialSpeed, shuttle.Speed);
    }
}