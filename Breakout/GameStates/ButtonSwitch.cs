namespace Breakout.GameStates;

using System;
using Breakout.Events;
using DIKUArcade.Events;
using DIKUArcade.Input;

public class ButtonSwitch {
    public Button[] menuButtons {
        get; private set;
    }
    public int activeMenuButton = 0;
    public int maxMenuButtons {
        get; private set;
    }
    public GameStateType nextState {
        get; set;
    }

    public ButtonSwitch(Button button1) {
        this.menuButtons = new Button[]{button1};
        initialButtons();
    }
    public ButtonSwitch(Button button1, Button button2) {
        this.menuButtons = new Button[]{button1, button2};
        initialButtons();
    }
    public ButtonSwitch(Button button1, Button button2, Button button3) {
        this.menuButtons = new Button[]{button1, button2, button3};
        initialButtons();
    }
    public ButtonSwitch(params Button[] buttons) {
        this.menuButtons = buttons;
        initialButtons();
    }

    private void initialButtons() {
        maxMenuButtons = menuButtons.Length;
        foreach (Button i in menuButtons) {
            i.InactiveButton();
        }
        menuButtons[activeMenuButton].ActiveButton();
    }
    public void KeyPress(KeyboardKey key, GameStateType currentState) {
        switch (key) {
            case KeyboardKey.Up:
                foreach (Button i in this.menuButtons) {
                    i.InactiveButton();
                }
                activeMenuButton = Math.Max(0, this.activeMenuButton - 1);
                this.menuButtons[this.activeMenuButton].ActiveButton();
                break;
            case KeyboardKey.Down:
                foreach (Button i in this.menuButtons) {
                    i.InactiveButton();
                }
                this.activeMenuButton = Math.Min(this.maxMenuButtons - 1, this.activeMenuButton + 1);
                this.menuButtons[activeMenuButton].ActiveButton();
                break;
            case KeyboardKey.Enter:
                this.nextState = this.menuButtons[this.activeMenuButton].TransferFromTextToGameStateType();
                if (this.menuButtons[this.activeMenuButton].Text == "Quit") {
                    GameBus.GetBus().RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.WindowEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                    );
                } else {
                    GameBus.GetBus().RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.GameStateEvent)
                        .WithStateType(this.nextState)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                    );
                }
                break;
            default:
                break;
        }
    }

    public void KeyRelease(KeyboardKey key) {
        switch (key) {
            default:
                break;
        }
    }

    public void Render(){
        foreach (Button i in menuButtons){
            i.Render();
        }
    }
}