namespace Breakout.GameStates;

using System;
using System.Collections.Generic;
using System.Linq;
using Breakout.Events;
using DIKUArcade.Events;
using DIKUArcade.Input;

public class ButtonSwitch {
    private List<Button> menuButtons = new List<Button>();
    private int activeMenuButton;

    public List<Button> MenuButtons {
        get {
            return menuButtons;
        }
        set {
            this.menuButtons = value;
        }

    }
    public int ActiveMenuButton {
        get {
            return activeMenuButton;
        }
        set {
            this.activeMenuButton = value;
        }
    }

    public ButtonSwitch(List<Button> buttons) {
        MenuButtons = buttons.ToList();
        ResetState();
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
                string buttonLabel = MenuButtons[ActiveMenuButton].Text;
                GameBus.GetBus().RegisterEvent(
                    OnPressEvent(buttonLabel)
                );
                break;
            default:
                break;
        }
    }

    public void Render(){
        foreach (Button button in MenuButtons){
            button.Render();
        }
    }

    private GameEvent OnPressEvent(string buttonLabel) {
        return buttonLabel switch {
            "Quit" => new EventBuilder()
                .WithType(GameEventType.WindowEvent)
                .WithAction(KeyboardAction.KeyPress)
                .Build(),
            "Next Level" => new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameRunning)
                .WithAction(KeyboardAction.KeyPress)
                .WithMessage(buttonLabel)
                .Build(),
            _ => new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GetNextState(buttonLabel))
                .WithAction(KeyboardAction.KeyPress)
                .Build()
        };
    }

    public void ResetState() {
        activeMenuButton = 0;
        InactivateAllButtons();
        MenuButtons[ActiveMenuButton].Activate();
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
        ActiveMenuButton = Math.Min(MenuButtons.Count - 1, ActiveMenuButton + 1);
    }

    /// <summary>
    /// Get the next state based on the text on the button
    /// that was pressed.
    /// </summary>
    private GameStateType GetNextState(string buttonLabel) {
        return (GameStateType) (Button.textToState[buttonLabel]);
    }
}