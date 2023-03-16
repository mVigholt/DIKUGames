namespace Galaga;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Score{
    private int credit = 0;
    public Text display;
    public int Credit {
        get {return credit;}
        set {
            credit = value;
        }
    }
    public Score(Vec2F pos, Vec2F extent){
        display = new Text("Score: " + this.Credit.ToString(), pos, extent);
        display.SetColor(new Vec3I(255,0,0));
    }
}