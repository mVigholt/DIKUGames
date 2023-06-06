namespace BreakoutTests.TestBoard;

using NUnit.Framework;
using Breakout.Entities.Board;

[TestFixture]
public class  TestCountDownBoard{
    private CountDownBoard countDownBoard;

    [SetUp]
    public void InitCountDownBoard() {
        countDownBoard = new CountDownBoard (200);
    }

    [TestCase(0)]
    [TestCase(-100)]
    [TestCase(-200)]
    [TestCase(5)]
    [TestCase(100)]
    [TestCase(200)]
    public void TestAddTime(double addedTime){
        //Precondition: timeLeft is not null
        countDownBoard.AddTime(addedTime);
        Assert.That(countDownBoard.timeLeft, Is.EqualTo(200 + addedTime));
    }

}