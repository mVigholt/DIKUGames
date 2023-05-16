namespace Breakout.GameStates;

using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameStateFactory : IGameState {
    private Text titleText;
    private string Text;
    private BackGround backGround;
    private Button FirstButton;
    private Button SecondButton;
    private ButtonSwitch buttonSwitch;
    public GameStateFactory(string text, string firstButton, string secondButton, Image backGroundImage){
        this.FirstButton = new Button(firstButton, new Vec2F(0.2f, 0.3f));
        this.SecondButton = new Button(secondButton, new Vec2F(0.2f, 0.2f));;
        this.Text = text;
        titleText = new Text(this.Text, new Vec2F(0.3f, 0.4f), new Vec2F(0.4f, 0.4f));
        titleText.SetColor(new Vec3I (165, 49, 176));
        backGround  = new BackGround(backGroundImage);
        buttonSwitch = new ButtonSwitch (FirstButton, SecondButton);
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
        foreach (Button i in buttonSwitch.menuButtons){
            i.Render();
        }
    }

    public void ResetState() {
        buttonSwitch.activeMenuButton = 0;
    }

    public void UpdateState() {

    }
}