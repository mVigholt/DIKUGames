using NUnit.Framework;
using Breakout.Entities.Board;

namespace BreakoutTests.TestBoard;
[TestFixture]
public class TestHealth {
    private LivesBoard livesBoard;

    [SetUp]
    public void InitiateHealth() {
        int lives = 10;
        livesBoard = new LivesBoard (lives);
    }
    [Test]
    public void TestHealthLost([Range(0, 10, 1)] int lostLives){
        livesBoard.LostLives(lostLives);
        Assert.That(livesBoard.GetRemainingLives(), Is.EqualTo(10 - lostLives));
    }

    [Test]
    public void TestWhenLostLIvesAreBig([Range(10, 100, 1)] int lostLives){
        livesBoard.LostLives(lostLives);
                Assert.That(livesBoard.GetRemainingLives(), Is.EqualTo(0));

    }
}