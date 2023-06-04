namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using DIKUArcade.Events;
using Breakout.GameStates;
using Breakout.Entities;
using Breakout.Events;
using Breakout.Entities.EffectItems.ItemConfigs;
using Breakout.Entities.EffectItems;

[TestFixture]
public class TestExtraLifeEffect {

    GameRunning gameRunning = GameRunning.GetInstance(true);

    [Test]
    public void TestActivate() {
        int initialLives = gameRunning.Lives;
        Shuttle shuttle = EntityCreator.CreateShuttle();
        EffectSimulator.Activate("ExtraLife");
        Assert.AreEqual(initialLives + 1, gameRunning.Lives);
    }
}