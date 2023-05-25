namespace Breakout.GameStates;

using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameStateFactory : IGameState {
    private Text titleText;
    private BackGround backGround;
    private ButtonSwitch buttonSwitch;
    private static readonly Vec2F TEXT_EXTENT = new Vec2F(0.4f, 0.4f);
    private static readonly  Vec2F TITLE_TEXT_POSITION = new Vec2F(0.3f, 0.45f);
    private static readonly Vec3I TEXT_COLOR = new Vec3I (165, 49, 176);

    public GameStateFactory(string text, Image backGroundImage, params string[] buttons){
        titleText = new Text(text, TITLE_TEXT_POSITION, TEXT_EXTENT);
        titleText.SetColor(TEXT_COLOR);
        backGround  = new BackGround(backGroundImage);
        InitialFactory(buttons);
    }

    private void InitialFactory(string[] buttons){
        int i = 0;
        List<Button> buttonList = new List<Button>();
        foreach (string button in buttons){
            buttonList.Add(new Button(button, new Vec2F(0.2f, 0.4f - i * 0.1f)));
            i++;
        }
        buttonSwitch = new ButtonSwitch(buttonList.ToArray());
    }

    public void AddButton(string newButton, GameStateType? state){
        int menuLength = buttonSwitch.MenuButtons.Count;
        buttonSwitch.MenuButtons.Add(new Button(newButton, new Vec2F(0.2f, 0.4f- (menuLength)*0.1f)));
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
        titleText.RenderText();
        foreach (Button i in buttonSwitch.MenuButtons){
            i.Render();
        }
    }

    public void ResetState() {
        buttonSwitch.ActiveMenuButton = 0;
        foreach (Button i in buttonSwitch.MenuButtons){
            i.InactiveButton();
        }
        buttonSwitch.MenuButtons[buttonSwitch.ActiveMenuButton].ActiveButton();
    }

    public void UpdateState() {

    }
}