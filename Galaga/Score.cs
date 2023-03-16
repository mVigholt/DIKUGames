namespace Galaga;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Score {
    private int points = 0;
    public Text display;
    public int Points { get { return points; } }
    public Score(Vec2F pos, Vec2F extent){
        display = new Text($"Score: {points}", pos, extent);
        display.SetColor(new Vec3I(255,0,0));
    }

    public void IncrementPoints() {
        points++;
        display.SetText($"Score: {points}");
    }
}