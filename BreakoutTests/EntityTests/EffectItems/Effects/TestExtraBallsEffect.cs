namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.GameStates;
using Breakout.Entities;
using Breakout.Entities.EffectItems;


[TestFixture]
public class TestExtraBallsEffect {

    GameRunning gameRunning = GameRunning.GetInstance(true);
    
    [Test]
    public void TestActivate() {
        int initialBalls = NumBalls();
        EffectSimulator.Activate("ExtraBalls");
        Assert.AreEqual(initialBalls + 1, NumBalls());
    }

    private int NumBalls() {
        return gameRunning.level.balls.CountEntities();
    }
}