namespace Breakout.GameStates;

using System.Collections.Generic;
using System.Linq;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;


/// <summary>
/// The super class for MainMenu, GameLost, GameWon and GamePaused,
/// and so, when there are only title text and buttons to show and choose.
/// </summary>
public class MenuGameState : IGameState {
    public static readonly Vec2F TEXT_EXTENT = new Vec2F(0.4f, 0.4f);
    private static readonly Vec2F TITLE_TEXT_POSITION = new Vec2F(0.3f, 0.45f);
    private static readonly Vec3I TEXT_COLOR = new Vec3I(165, 49, 176);
    private BackGround backGround;
    private ButtonSwitch buttonSwitch;
    protected List<Text> texts;
    protected Text titleText;

    public MenuGameState(string title, Image BackgroundImage, params string[] buttonLabels) {
        texts = new List<Text> { };
        titleText = new Text(title, TITLE_TEXT_POSITION, TEXT_EXTENT);
        texts.Add(titleText);
        titleText.SetColor(TEXT_COLOR);
        backGround = new BackGround(BackgroundImage);
        buttonSwitch = CreateButtonSwitch(buttonLabels);
    }

    public MenuGameState(Image BackgroundImage, params string[] buttonLabels)
        : this("", BackgroundImage, buttonLabels) {
    }


    /// <summary>
    /// Each state can be extended with some extra texts
    /// </summary>
    public void AddText(string newText, Vec2F pos, Vec2F extent) {
        Text otherText = new Text(newText, pos, extent);
        otherText.SetColor(TEXT_COLOR);
        texts.Add(otherText);
    }

    /// <summary>
    /// Each state can be extended with some extra buttons
    /// </summary>
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

    private ButtonSwitch CreateButtonSwitch(string[] labels) {
        List<Button> buttons =
            labels.Select((label, i) =>
                new Button(label, new Vec2F(0.2f, 0.4f - i * 0.1f)))
                .ToList();
        return new ButtonSwitch(buttons);
    }
}