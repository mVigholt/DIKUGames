using NUnit.Framework;
using Breakout.Entities.Board;

namespace BreakoutTests.TestBoard;
[TestFixture]
public class TestScoreBoard {
    private ScoreBoard scoreBoard;

    [SetUp]
    public void InitiateScores() {
        scoreBoard= new ScoreBoard();
    }
    [Test]
    public void TestAddPoints([Range(0, 10, 1)] int points){
        scoreBoard.AddPoints(points);
        Assert.That(scoreBoard.GetRemainingPoint(), Is.EqualTo(points));
    }
    [Test]
    public void TestNextLevel(){
        int initialLevel = scoreBoard.Level;
        scoreBoard.NextLevel();
        Assert.That(scoreBoard.Level, Is.EqualTo(1));
    }
}