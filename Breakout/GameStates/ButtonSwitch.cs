namespace Breakout.GameStates;

using System;
using System.Collections.Generic;
using System.Linq;
using Breakout.Events;
using DIKUArcade.Events;
using DIKUArcade.Input;

public class ButtonSwitch {
    private List<Button> menuButtons = new List<Button>();
    public List<Button> MenuButtons {
        get{
            return menuButtons;
        }
        set{
            this.menuButtons = value;
        }

    }
    private int activeMenuButton = 0;

    public int ActiveMenuButton {
        get{
            return activeMenuButton;
        }
        set{
            this.activeMenuButton = value;
        }
    }

    public int MaxMenuButtons {
        get; set;
    }

    public ButtonSwitch(params Button[] buttons) {
        MenuButtons = buttons.ToList();
        initialButtons();
    }

    private GameEvent OnPressEvent(string buttonText) {
        return buttonText switch {
            "Quit" => new EventBuilder()
                .WithType(GameEventType.WindowEvent)
                .WithAction(KeyboardAction.KeyPress)
                .Build(),
            "Next Level" => new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameRunning)
                .WithAction(KeyboardAction.KeyPress)
                .WithMessage(buttonText)
                .Build(),
            _ => new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GetNextState(buttonText))
                .WithAction(KeyboardAction.KeyPress)
                .Build()
        };
    }

    public void initialButtons() {
        MaxMenuButtons = MenuButtons.Count;
        foreach (Button button in MenuButtons) {
            button.Inactivate();
        }
        MenuButtons[ActiveMenuButton].Activate();
    }

    public void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Up:
                InactivateAllButtons();
                NavigateUp();
                MenuButtons[ActiveMenuButton].Activate();
                break;
            case KeyboardKey.Down:
                InactivateAllButtons();
                NavigateDown();
                MenuButtons[ActiveMenuButton].Activate();
                break;
            case KeyboardKey.Enter:
                string buttonText = MenuButtons[ActiveMenuButton].Text;
                GameBus.GetBus().RegisterEvent(
                    OnPressEvent(buttonText)
                );
                break;
            default:
                break;
        }
    }

    private void InactivateAllButtons() {
        foreach (Button button in MenuButtons) {
            button.Inactivate();
        }
    }

    private void NavigateUp() {
        ActiveMenuButton = Math.Max(0, ActiveMenuButton - 1);
    }

    private void NavigateDown() {
        ActiveMenuButton = Math.Min(MaxMenuButtons - 1, ActiveMenuButton + 1);
    }

    /// <summary>
    /// Get the next state based on the text on the button
    /// that was pressed.
    /// </summary>
    private GameStateType GetNextState(string buttonText) {
        return (GameStateType) (Button.textToState[buttonText]);
    }

    public void KeyRelease(KeyboardKey key) {
        switch (key) {
            default:
                break;
        }
    }

    public void Render(){
        foreach (Button button in MenuButtons){
            button.Render();
        }
    }
}