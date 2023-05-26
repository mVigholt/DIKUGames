namespace BreakoutTests.StatesTests;

using NUnit.Framework;
using Breakout;
using Breakout.GameStates;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using Breakout.Events;

[TestFixture]
public class StateMachineTesting {
    private StateMachine stateMachine;
    [SetUp]
    public void InitiateStateMachine() {
        Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
    }

    public void GoToMainMenu() {
        GameBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.MainMenu)
                .Build()
        );
    }

    public void GoToGameRunning() {
        GameBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameRunning)
                .Build()
        );
    }

    public void GoToGamePaused() {
        GameBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GamePaused)
                .Build()
        );
    }

    public void GoToGameWon() {
        GameBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameWon)
                .Build()
        );
    }

    public void GoToGameLost() {
        GameBus.GetBus().RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameLost)
                .Build()
        );
    }

    [Test]
    public void TestEventGameRunning() {
        GoToGameRunning();
        GameBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestEventGamePaused() {
        GoToGameRunning();
        GoToGamePaused();
        GameBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

    [Test]
    public void TestEventMainMenu() {
        GoToGameRunning();
        GoToGamePaused();
        GoToMainMenu();
        GameBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGameWon() {
        GoToGameRunning();
        GoToGameWon();
        GameBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameWon>());
    }

    [Test]
    public void TestEventGameLost() {
        GoToGameRunning();
        GoToGameLost();
        GameBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
}
