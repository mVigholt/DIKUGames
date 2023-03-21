namespace GalagaTests;

using DIKUArcade.Galaga.GalagaStates;
using NUnit.Framework;

[TestFixture]

public class TestStateTransformer {
    GameStateType stateType;

    [TestCase (GameStateType.MainMenu)]
    [TestCase (GameStateType.GamePaused)]
    [TestCase (GameStateType.GameRunning)]
    public void TestTransformStateToString(GameStateType stateType){
        Assert.AreEqual(StateTransformer.TransformStateToString(stateType),
            stateType.ToString());
    }


    [Test]
    public void TransformStringToState(){
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("MainMenu"), GameStateType.MainMenu);
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("GamePaused"), GameStateType.GamePaused);
        Assert.AreEqual(StateTransformer.TransformStringToState
            ("GameRunning"), GameStateType.GameRunning);
    }
}
