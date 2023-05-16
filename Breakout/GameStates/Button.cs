namespace Breakout.GameStates;

using DIKUArcade.Graphics;
using DIKUArcade.Math;



public class Button : Text
{

    public string Text {get; private set;}
    public Vec2F Position{get; private set;}
    public Vec2F Extent {get; private set;}
    public static readonly Vec2F BUTTON_EXTEND = new Vec2F(0.3f, 0.3f);

    private Vec3I defaultColor = new Vec3I(255, 0, 0);
    private Vec3I activeColor = new Vec3I(255, 255, 255);
    private Vec3I inactiveColor = new Vec3I(51, 153, 255);

    public Button(string text, Vec2F pos) : base(text, pos, BUTTON_EXTEND)
    {
        this.Text = text;
        this.Position = pos;
        this.SetColor(defaultColor);
    }

    public void ActiveButton(){
        this.SetColor(activeColor);
    }

    public void InactiveButton(){
        this.SetColor(inactiveColor);
    }

    public void Render(){
        this.RenderText();
    }

    public GameStateType TransferFromTextToGameStateType(){
        switch (this.Text){
            case "Main Menu":
                return GameStateType.MainMenu;
            case "Game Over":
                return GameStateType.GameLost;
            case "You Win":
                return GameStateType.GameWon;
            case "New Game":
                return GameStateType.GameRunning;
            default:
                return GameStateType.MainMenu;
        }
    }

    
}