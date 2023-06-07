namespace BreakoutTests.EntityTests.EffectItems.Effects;

using NUnit.Framework;
using DIKUArcade.Math;
using Breakout.GameStates;
using Breakout.Entities;
using Breakout.Entities.EffectItems.Effects;


/// <summary>
/// Test both WideEffect and SlimJimEffect.
/// </summary>
[TestFixture]
public class TestWideEffect {

    GameRunning gameRunning = GameRunning.GetInstance(true);

    [Test]
    public void TestShuttleGetsWidened() {
        Shuttle shuttle = EntityCreator.CreateShuttle();
        float initialWidth = shuttle.GetExtent().X;
        float pctOfShuttleWidth = new WideEffect(shuttle).pctOfShuttleWidth;
        float dw0 = pctOfShuttleWidth * initialWidth;
        System.Console.WriteLine("Before: " + shuttle.GetExtent().X);
        System.Console.WriteLine("Adding " + dw0);
        EffectSimulator.Activate("Wide");
        System.Console.WriteLine("After: " + shuttle.GetExtent().X);
        Assert.AreEqual(
            initialWidth + dw0,
            shuttle.GetExtent().X
        );
        float dw1 = shuttle.GetExtent().X * pctOfShuttleWidth;
        EffectSimulator.Activate("Wide");
        Assert.AreEqual(
            initialWidth + dw0 + dw1,
            shuttle.GetExtent().X
        );
        EffectSimulator.Deactivate("Wide");
        EffectSimulator.Deactivate("Wide");
        Assert.AreEqual(
            initialWidth,
            shuttle.GetExtent().X
        );
    }

    /// <summary>
    /// Assert that when the shuttle gets widened and narrowed,
    /// its horizontal center stays at the same x-coordinate.
    /// Requirement from assignment 10, page 5:
    /// "The player’s visual position must not change 
    /// when this power-up is activated"
    /// </summary>
    [Test]
    public void TestShuttleKeepsItsCenter() {
        Shuttle shuttle = EntityCreator.CreateShuttle();
        Vec2F initialCenter = CenterOf(shuttle);
        EffectSimulator.Activate("Wide");
        System.Console.WriteLine((initialCenter - CenterOf(shuttle)).X);
        Assert.AreEqual(initialCenter.X, CenterOf(shuttle).X);
        EffectSimulator.Deactivate("Wide");
        Assert.AreEqual(initialCenter.X, CenterOf(shuttle).X);
    }

    private Vec2F CenterOf(Shuttle shuttle) {
        return shuttle.GetPosition() + shuttle.GetExtent() / 2f;
    }
}