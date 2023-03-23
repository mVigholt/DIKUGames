namespace Galaga;

using System;
using DIKUArcade.Events;
using DIKUArcade.Input;

/// <summary>
/// The way we use events should be enforced.
/// 
/// GameEvent ev = new EventBuilder()
///     .WithType(GameEventType.PlayerEvent)
///     .WithKey(KeyboardKey.Up)
///     .WithKeyboardAction(KeyboardAction.KeyPress)
///     .Build();
/// 
/// Additionally, a debug string can be set using
/// 
///     .WithString("Hello")
/// 
/// All other fields from Event are impossible to populate
/// with this class, since we have decided not to use them.
/// </summary>
public class EventBuilder {
    
    private Nullable<GameEventType> _type;
    private Nullable<int> _keyAction;
    // Strings and objects are already nullable
    private object _key;
    private string _debugString;

    public EventBuilder() {}

    public EventBuilder WithType(GameEventType eventType) {
        _type = eventType;
        return this;
    }

    public EventBuilder WithKey(KeyboardKey key) {
        _key = key;
        return this;
    }

    public EventBuilder WithKeyboardAction(KeyboardAction action) {
        _keyAction = (int)action;
        return this;
    }

    public EventBuilder WithString(string debugString) {
        _debugString = debugString;
        return this;
    }

    public GameEvent Build() {
        if (_type is null) {
            throw new ArgumentException(
                "Events must specify an event type using WithType()");
        }
        if (_key is null) {
            throw new ArgumentException(
                "Events must specify a KeyboardKey using WithKey()");
        }
        if (_keyAction is null) {
            throw new ArgumentException(
                "Events must specify a keyboard action " +
                "using WithKeyboardAction()");
        }
        return new GameEvent {
            EventType = _type.Value,
            ObjectArg1 = _key,
            StringArg1 = _debugString,
            IntArg1 = _keyAction.Value
        };
    }
}