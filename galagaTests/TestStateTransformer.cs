namespace GalagaTests;

using DIKUArcade.Galaga.GalagaStates;
using NUnit.Framework;

[TestFixture]

public class TestStateTransformer {

    [Test]
    public void TestTransformStateToString(){
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu),
            "MainMenu");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning),
            "GameRunning");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused),
            "GamePaused");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameWon),
            "GameWon");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameLost),
            "GameLost");
    }

    [Test]
    public void TransformStringToState(){
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("MainMenu"), GameStateType.MainMenu);
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("GamePaused"), GameStateType.GamePaused);
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("GameRunning"), GameStateType.GameRunning);
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("GameWon"), GameStateType.GameWon);
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("GameLost"), GameStateType.GameLost);
    }
}
