namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// This class inherits Text class to show the name of
/// the level. The name is read from an Ascii file
///</summary>
public class LevelBoard : Text {
    private static readonly Vec2F LEVEL_POS = new Vec2F(0.4f, 0.75f);
    private static readonly Vec2F LEVEL_EXTENT = new Vec2F(0.25f, 0.25f);
    private static readonly Vec3I LEVEL_BOARD_COLOR = new Vec3I(255, 255, 255);

    private string levelName;

    public LevelBoard(string name) :
            base("Breakout", LEVEL_POS, LEVEL_EXTENT) {
        levelName = name.ToUpper();
        SetColor(LEVEL_BOARD_COLOR);
        SetText(levelName);
    }
    public void Render() {
        RenderText();
    }
}