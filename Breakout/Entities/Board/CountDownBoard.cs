namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

/// <summary>
/// This class inheritates Text class to show how much time left
/// for each level. The totalTime is read from ascii file.
/// In general the timer uses a built-in module: StaticTimer to
/// record how much time has been passed.
///</summary>
public class CountDownBoard : Text {
    private double totalTime;
    public double timeLeft { get; private set; } = 0;
    private double CreationTime { get; set; } = 1.0;

    public CountDownBoard(int levelTime) :
        base("Time: 0", new Vec2F(0.01f, 0.75f), new Vec2F(0.25f, 0.25f)) {
        totalTime = levelTime;
        timeLeft = totalTime;
        StaticTimer.RestartTimer();
        SetColor(new Vec3I(51, 153, 255));
        SetText($"Time: {timeLeft}");
    }

    ///<summary>
    ///The function is to show a count down effect in the game
    ///</summary>
    ///<param name = "addedTime"> the time added to the creation time.
    /// if it is negative, then the time is deducted from the creation time</param>
    public void AddTime(double addedTime) {
        timeLeft += addedTime;
        SetText($"Time: {timeLeft}");
    }


    ///<summary>
    /// To update the count down board by using static time
    /// when the creation time plus a millisecond is smaller than one second
    /// detected in the game, a second is decuted from the count down board.
    ///</summary>
    public void UpdateCountDown() {
        if (CreationTime + 1 / 1000 < StaticTimer.GetElapsedSeconds()) {
            AddTime(-1);
            CreationTime++;
        }
    }
    public void Render() {
        RenderText();
    }
}