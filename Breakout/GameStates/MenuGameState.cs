namespace Breakout.GameStates;

using System.Collections.Generic;
using System.Linq;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class MenuGameState : IGameState {
    protected List<Text> texts = new List<Text>{};
    private BackGround backGround;              
    private ButtonSwitch buttonSwitch;          
    protected Text titleText;                   
    public static readonly Vec2F TEXT_EXTENT = new Vec2F(0.4f, 0.4f);           
    public static readonly Vec2F TITLE_TEXT_POSITION = new Vec2F(0.3f, 0.45f);
    public static readonly Vec3I TEXT_COLOR = new Vec3I(165, 49, 176);

    public MenuGameState(string title, Image backGroundImage, params string[] buttonLabels) {
        titleText = new Text(title, TITLE_TEXT_POSITION, TEXT_EXTENT);
        texts.Add(titleText);
        titleText.SetColor(TEXT_COLOR);
        backGround = new BackGround(backGroundImage);
        buttonSwitch = CreateButtonSwitch(buttonLabels);
    }

    public MenuGameState(Image backGroundImage, params string[] buttonLabels) 
        : this("", backGroundImage, buttonLabels) {
    }

    private ButtonSwitch CreateButtonSwitch(string[] labels) {
        List<Button> buttons = 
            labels.Select((label, i) =>
                new Button(label, new Vec2F(0.2f, 0.4f - i * 0.1f)))
                .ToList();
        return new ButtonSwitch(buttons);
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
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            buttonSwitch.KeyPress(key);
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
        buttonSwitch.ResetState();
    }

    public void UpdateState() {
    }
}