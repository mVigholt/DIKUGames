namespace Breakout.GameStates;

using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;



public class Button : Text
{
    public string Text {get; private set;}
    public Vec2F Position{get; private set;}
    public static readonly Vec2F BUTTON_EXTEND = new Vec2F(0.3f, 0.3f);
    private Vec3I activeColor = new Vec3I(255, 255, 255);
    private Vec3I inactiveColor = new Vec3I(51, 153, 255);

    public static Dictionary<string, GameStateType?> textToState = new Dictionary<string, GameStateType?>{};

    public Button(string text, Vec2F pos) : base(text, pos, BUTTON_EXTEND)
    {
        this.Text = text;
        this.Position = pos;
        this.SetColor(inactiveColor);
        InitialButton();
    }

    private void InitialButton(){
        textToState.TryAdd("Main Menu", GameStateType.MainMenu);
        textToState.TryAdd("Game Over", GameStateType.GameLost);
        textToState.TryAdd("New Game", GameStateType.GameRunning);
        textToState.TryAdd("Continue", GameStateType.GameRunning);
        textToState.TryAdd("Quit",  null);
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
}