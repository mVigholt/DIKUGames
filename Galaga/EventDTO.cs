namespace Galaga;

using DIKUArcade.Events;
using DIKUArcade.Input;

/// <summary>
/// This event data transfer object is instantiated
/// with a GameEvent and exposes the fields
/// that our business layer needs:
/// Type, Key, Action and an optional DebugString
/// </summary>
public class EventDTO {

    public readonly GameEventType Type;
    public readonly KeyboardKey Key;
    public readonly KeyboardAction Action;
    public readonly string DebugString; // optional

    public EventDTO(GameEvent ev) {
        Type = ev.EventType;
        Key = (KeyboardKey) ev.ObjectArg1;
        Action = (KeyboardAction) ev.IntArg1;
        DebugString = ev.StringArg1;
    }
}