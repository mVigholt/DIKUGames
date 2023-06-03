namespace Breakout.GameStates;

using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameState : IGameState {
    protected List<Text> texts = new List<Text>{};
    private BackGround backGround;              // Alle
    private ButtonSwitch buttonSwitch;          // GameWon, GameLost, MainMenu, Pause
    protected Text titleText;                   // GameWon, GameLOst, MainMenu
    // These belong to titleText;
    public static readonly Vec2F TEXT_EXTENT = new Vec2F(0.4f, 0.4f);           
    public static readonly Vec2F TITLE_TEXT_POSITION = new Vec2F(0.3f, 0.45f);
    public static readonly Vec3I TEXT_COLOR = new Vec3I(165, 49, 176);

    public GameState(string text, Image backGroundImage, params string[] buttons) {
        this.InitFactory( text,  backGroundImage, buttons);

    }

    public void InitFactory(string text, Image backGroundImage, string[] buttons){
        titleText = new Text(text, TITLE_TEXT_POSITION, TEXT_EXTENT);
        texts.Add(titleText);
        titleText.SetColor(TEXT_COLOR);
        backGround = new BackGround(backGroundImage);
        int i = 0;
        List<Button> buttonList = new List<Button>();
        foreach (string button in buttons) {
            buttonList.Add(new Button(button, new Vec2F(0.2f, 0.4f - i * 0.1f)));
            i++;
        }
        buttonSwitch = new ButtonSwitch(buttonList.ToArray());
    }

    public void AddText(string newText, Vec2F pos, Vec2F extent) {
        Text otherText = new Text(newText, pos, extent);
        otherText.SetColor(TEXT_COLOR);
        texts.Add(otherText);
    }

    public void AddButton(string newButton, GameStateType? state) {
        int menuLength = buttonSwitch.MenuButtons.Count;
        buttonSwitch.MenuButtons.Add(new Button(newButton, new Vec2F(0.2f, 0.4f - (menuLength) * 0.1f)));
        Button.textToState.TryAdd(newButton, state);
        buttonSwitch.MaxMenuButtons = buttonSwitch.MenuButtons.Count;
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                buttonSwitch.KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                buttonSwitch.KeyRelease(key);
                break;
        }
    }

    public void RenderState() {
        backGround.RenderEntity();
        buttonSwitch.Render();
        foreach (Text text in texts) {
            text.RenderText();
        }
    }

    public virtual void ResetState() {
        buttonSwitch.ActiveMenuButton = 0;
        buttonSwitch.initialButtons();
    }

    public void UpdateState() {
    }
}