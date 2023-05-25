namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class CountDown : Text {
    public double timeLeft = 0;
    public double initialTime = 0;

    public CountDown(int levelTime) :
        base("Time: 0", new Vec2F(0.01f, 0.75f),  new Vec2F(0.25f, 0.25f)) {
        this.initialTime = levelTime;
        this.timeLeft = initialTime;
        SetColor(new Vec3I(51, 153, 255));
        SetText($"Time: {this.timeLeft}");
    }

    public void AddOrMinusTime(double addedTime) {
        this.timeLeft += addedTime;
        SetText($"Time: {this.timeLeft}");
    }

    public void Render(){
        this.RenderText();
    }

}