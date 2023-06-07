namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using Breakout.GameStates;


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