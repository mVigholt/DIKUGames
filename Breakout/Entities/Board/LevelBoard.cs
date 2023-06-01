namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class LevelBoard : Text {
    private string levelName;
    private static readonly Vec2F LEVEL_POS = new Vec2F(0.4f, 0.75f);
    private static readonly Vec2F LEVEL_EXTENT = new Vec2F(0.25f, 0.25f);
    private static readonly Vec3I LEVEL_BOARD_COLOR = new Vec3I(255, 255, 255);

    public LevelBoard(string name) :
            base("Breakout", LEVEL_POS, LEVEL_EXTENT) {
        this.levelName = name.ToUpper();
        SetColor(LEVEL_BOARD_COLOR);
        SetText(this.levelName);
    }
    public void Render() {
        this.RenderText();
    }

}