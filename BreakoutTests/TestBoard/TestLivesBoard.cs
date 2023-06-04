using NUnit.Framework;
using Breakout.Entities.Board;

namespace BreakoutTests.TestBoard;
[TestFixture]
public class TestLivesBoard {
    private LivesBoard livesBoard;

    [SetUp]
    public void InitiateLives() {
        int lives = 10;
        livesBoard = new LivesBoard (lives);
    }
    [Test]
    public void TestLivesLost([Range(0, 10, 1)] int lostLives){
        livesBoard.LoseLives(lostLives);
        Assert.That(livesBoard.GetRemainingLives(), Is.EqualTo(10 - lostLives));
    }

    [Test]
    public void TestWhenLostLivesAreBig([Range(10, 100, 1)] int lostLives){
        livesBoard.LoseLives(lostLives);
                Assert.That(livesBoard.GetRemainingLives(), Is.EqualTo(0));

    }
}