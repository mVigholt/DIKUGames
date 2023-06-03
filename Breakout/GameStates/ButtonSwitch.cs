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

    private string ActiveButtonText;

    public GameStateType NextState {
        get; set;
    }

    public ButtonSwitch(params Button[] buttons) {
        MenuButtons = buttons.ToList();
        initialButtons();
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
                foreach (Button button in MenuButtons) {
                    button.Inactivate();
                }
                ActiveMenuButton = Math.Max(0, ActiveMenuButton - 1);
                MenuButtons[ActiveMenuButton].Activate();
                break;
            case KeyboardKey.Down:
                foreach (Button button in MenuButtons) {
                    button.Inactivate();
                }
                ActiveMenuButton = Math.Min(MaxMenuButtons - 1, ActiveMenuButton + 1);
                MenuButtons[ActiveMenuButton].Activate();
                break;
            case KeyboardKey.Enter:
                ActiveButtonText = MenuButtons[ActiveMenuButton].Text;
                if (ActiveButtonText == "Quit") {
                    GameBus.GetBus().RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.WindowEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                    );
                } else if(ActiveButtonText == "Next Level"){
                        NextState = GameStateType.GameRunning;
                        GameBus.GetBus().RegisterEvent(
                        new EventBuilder()
                            .WithType(GameEventType.GameStateEvent)
                            .WithStateType(NextState)
                            .WithAction(KeyboardAction.KeyPress)
                            .WithMessage(ActiveButtonText)
                            .Build()
                    );

                } else{
                    NextState = (GameStateType) (Button.textToState[ActiveButtonText]);
                    GameBus.GetBus().RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.GameStateEvent)
                        .WithStateType(this.NextState)
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
        foreach (Button button in MenuButtons){
            button.Render();
        }
    }
}