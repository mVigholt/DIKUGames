namespace Galaga;

using System;
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
    public readonly object Obj;
    public readonly Nullable<KeyboardAction> Action;
    public readonly string DebugString; // optional

    public EventDTO(GameEvent ev) {
        Type = ev.EventType;
        try {
            Key = (KeyboardKey) ev.ObjectArg1;
            Obj = ev.ObjectArg1;
        } catch (Exception) {
            Obj = ev.ObjectArg1;
        }
        DebugString = ev.StringArg1;
        if (ev.IntArg1 == -1) {
            Action = null;
        } else {
            Action = (KeyboardAction) ev.IntArg1;
        }
    }
}