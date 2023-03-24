namespace Galaga;

using System;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Galaga.GalagaStates;


/// <summary>
/// This event data transfer object is instantiated
/// with a GameEvent and exposes the fields
/// that our business layer needs:
/// Type, Key, Action and an optional DebugString
/// </summary>
public class EventDTO {

    public readonly GameEventType Type;
    public readonly Nullable<KeyboardKey> Key = null;
    public readonly Nullable<GameStateType> StateType = null;
    public readonly Nullable<KeyboardAction> Action = null;
    public readonly string DebugString;

    public EventDTO(GameEvent ev) {
        // This code is ugly so the callers' code
        // can be pretty
        Type = ev.EventType;
        if (!(ev.ObjectArg1 is null)) {
            Key = (KeyboardKey) ev.ObjectArg1;
        }
        if (!(ev.From is null)) {
            StateType = (GameStateType) ev.From;
        }
        if (ev.IntArg1 == -1) {
            Action = null;
        } else {
            Action = (KeyboardAction) ev.IntArg1;
        }
    }
}