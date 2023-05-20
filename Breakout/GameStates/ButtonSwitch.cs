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

    private string activeButtonText;

    public GameStateType nextState {
        get; set;
    }

    public ButtonSwitch(params Button[] buttons) {
        this.MenuButtons = buttons.ToList();
        initialButtons();
    }

    private void initialButtons() {
        MaxMenuButtons = MenuButtons.Count;
        foreach (Button i in MenuButtons) {
            i.InactiveButton();
        }
        MenuButtons[this.ActiveMenuButton].ActiveButton();
    }

    public void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Up:
                foreach (Button i in this.MenuButtons) {
                    i.InactiveButton();
                }
                ActiveMenuButton = Math.Max(0, this.ActiveMenuButton - 1);
                this.MenuButtons[this.ActiveMenuButton].ActiveButton();
                break;
            case KeyboardKey.Down:
                foreach (Button i in this.MenuButtons) {
                    i.InactiveButton();
                }
                this.ActiveMenuButton = Math.Min(this.MaxMenuButtons - 1, this.ActiveMenuButton + 1);
                this.MenuButtons[ActiveMenuButton].ActiveButton();
                break;
            case KeyboardKey.Enter:
                activeButtonText = this.MenuButtons[this.ActiveMenuButton].Text;
                if (activeButtonText == "Quit") {
                    GameBus.GetBus().RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.WindowEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                    );
                } else {
                    this.nextState = (GameStateType) (Button.textToState[activeButtonText]);
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
        foreach (Button i in MenuButtons){
            i.Render();
        }
    }
}