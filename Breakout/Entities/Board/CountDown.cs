namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

public class CountDown : Text {
    public double timeLeft = 0;
    public double initialTime = 0;
    public double CreationTime {
        get;
        set;
    }

    public CountDown(int levelTime) :
        base("Time: 0", new Vec2F(0.01f, 0.75f), new Vec2F(0.25f, 0.25f)) {
        this.initialTime = levelTime;
        this.timeLeft = initialTime + 1;
        SetColor(new Vec3I(51, 153, 255));
        SetText($"Time: {this.timeLeft}");
        StaticTimer gameTimer = new StaticTimer();
    }

    public void AddOrMinusTime(double addedTime) {
        this.timeLeft += addedTime;
        SetText($"Time: {this.timeLeft}");
    }

    public void UpdateCountDown() {
        if (this.CreationTime + 1 / 1000 < StaticTimer.GetElapsedSeconds()) {
            this.AddOrMinusTime(-1);
            this.CreationTime++;
        }
    }
    public void Render() {
        this.RenderText();
    }

}