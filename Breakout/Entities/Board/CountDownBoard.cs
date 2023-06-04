namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

public class CountDownBoard : Text {
    private double totalTime;

    public double timeLeft {get; private set;}= 0;
    public double CreationTime {get; set;} = 1.0;

    public CountDownBoard(int levelTime) :
        base("Time: 0", new Vec2F(0.01f, 0.75f), new Vec2F(0.25f, 0.25f)) {
        totalTime = levelTime;
        timeLeft = totalTime;
        StaticTimer.RestartTimer();
        SetColor(new Vec3I(51, 153, 255));
        SetText($"Time: {timeLeft}");
    }

    public void AddTime(double addedTime) {
        timeLeft += addedTime;
        SetText($"Time: {timeLeft}");
    }

    public void UpdateCountDown() {
        if (CreationTime + 1/1000 < StaticTimer.GetElapsedSeconds()) {
            AddTime(-1);
            CreationTime++;
        }
    }
    public void Render() {
        RenderText();
    }
}