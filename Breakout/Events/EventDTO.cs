namespace Breakout.Events;

using System;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout.GameStates;


/// <summary>
/// This event data transfer object is instantiated
/// with a GameEvent and exposes the fields
/// that our business layer needs.
/// Type is required. All other fields are optional.
///
/// We use DIKUArcade.Events.GameEvent for our underlying
/// logic, but we have remodelled it using this class.
/// Since we wanted to have an additional object field,
/// we have repurposed GameEvent.From to hold a
/// GameStateType.
/// Using EventBuilder and EventDTO, you do not need
/// to know the fields of GameEvent.
/// </summary>
public class EventDTO {

    public readonly GameEventType Type;
    // Optional fields
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
        DebugString = ev.StringArg1;
    }
}