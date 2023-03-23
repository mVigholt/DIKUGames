namespace GalagaTests;

using NUnit.Framework;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Galaga;


[TestFixture]
public class TestEvents {

    private GameEvent sampleEvent;

    [SetUp]
    public void SetUp() {
        sampleEvent = new EventBuilder()
            .WithType(GameEventType.PlayerEvent)
            .WithKey(KeyboardKey.Up)
            .WithAction(KeyboardAction.KeyPress)
            .WithString("Hello")
            .Build();
    }

    [Test]
    public void TestEventDTO() {
        EventDTO dto = new EventDTO(sampleEvent);
        Assert.AreEqual(GameEventType.PlayerEvent, dto.Type);
        Assert.AreEqual(KeyboardKey.Up, dto.Key);
        Assert.AreEqual(KeyboardAction.KeyPress, dto.Action);
        Assert.AreEqual("Hello", dto.DebugString);
    }
}