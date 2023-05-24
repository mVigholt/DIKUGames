namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class LevelBoard : Text {
    private string levelName;

    public LevelBoard(string name) :
            base("Breakout", new Vec2F(0.4f, 0.75f), new Vec2F(0.25f, 0.25f)) {
        this.levelName = name.ToUpper();
        SetColor(new Vec3I(255, 255, 255));
        SetText(this.levelName);
    }
    public void Render() {
        this.RenderText();
    }

}