namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

public class CountDownBoard : Text {
    public double timeLeft {get; private set;}= 0;
    private double totalTime;
    public double CreationTime {get; set;} = 1.0;

    public CountDownBoard(int levelTime) :
        base("Time: 0", new Vec2F(0.01f, 0.75f), new Vec2F(0.25f, 0.25f)) {
        this.totalTime = levelTime;
        this.timeLeft = totalTime;
        StaticTimer.RestartTimer();
        SetColor(new Vec3I(51, 153, 255));
        SetText($"Time: {this.timeLeft}");
    }

    public void AddOrMinusTime(double addedTime) {
        this.timeLeft += addedTime;
        SetText($"Time: {this.timeLeft}");
    }

    public void UpdateCountDown() {
        if (this.CreationTime + 1/1000 < StaticTimer.GetElapsedSeconds()) {
            this.AddOrMinusTime(-1);
            this.CreationTime++;
        }
    }
    public void Render() {
        this.RenderText();
    }

}