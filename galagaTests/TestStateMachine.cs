namespace GalagaTests;

using NUnit.Framework;
using Galaga;
using Galaga.GalagaStates;
using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.GUI;
using DIKUArcade.Events;

[TestFixture]
public class StateMachineTesting {
    private StateMachine stateMachine;
    [SetUp]
    public void InitiateStateMachine() {
        Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
    }

    public void GoToMainMenu() {
        GalagaBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithObject(GameStateType.MainMenu)
                .Build()
        );
    }

    public void GoToGameRunning() {
        GalagaBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithObject(GameStateType.GameRunning)
                .Build()
        );
    }

    public void GoToGamePaused() {
        GalagaBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithObject(GameStateType.GamePaused)
                .Build()
        );
    }

    public void GoToGameWon() {
        GalagaBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithObject(GameStateType.GameWon)
                .Build()
        );
    }

    public void GoToGameLost() {
        GalagaBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithObject(GameStateType.GameLost)
                .Build()
        );
    }

    [Test]
    public void TestEventGameRunning() {
        GoToGameRunning();
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestEventGamePaused() {
        GoToGameRunning();
        GoToGamePaused();
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

    [Test]
    public void TestEventMainMenu() {
        GoToGameRunning();
        GoToGamePaused();
        GoToMainMenu();
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGameWon() {
        GoToGameRunning();
        GoToGameWon();
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameWon>());
    }

    [Test]
    public void TestEventGameLost() {
        GoToGameRunning();
        GoToGameLost();
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
}
