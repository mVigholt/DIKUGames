namespace GalagaTests;
[TestFixture]
public class MovementStrategyTest {
    private IMovementStrategy movementStrategy;
    [SetUp]

    [Test]
    public void TestMovement() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
}
