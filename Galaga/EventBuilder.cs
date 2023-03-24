namespace Galaga;

using System;
using DIKUArcade.Events;
using DIKUArcade.Input;

/// <summary>
/// The way we use events should be enforced.
/// 
/// GameEvent ev = new EventBuilder()
///     .WithType(GameEventType.PlayerEvent)
///     .WithObject(KeyboardKey.Up)
///     .Build();
/// 
/// Additionally, a debug string and a keyboard action can be set using
/// 
///     .WithAction(KeyboardAction.KeyPress)
///     .WithString("Hello")
/// 
/// All other fields from Event are impossible to populate
/// with this class, since we have decided not to use them.
/// </summary>
public class EventBuilder {
    
    private Nullable<GameEventType> _type;
    private Nullable<int> _action;
    // Strings and objects are already nullable
    private object _obj;
    private string _debugString;

    public EventBuilder() {}

    public EventBuilder WithType(GameEventType eventType) {
        _type = eventType;
        return this;
    }

    public EventBuilder WithObject(object obj) {
        _obj = obj;
        return this;
    }

    public EventBuilder WithAction(KeyboardAction action) {
        _action = (int)action;
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
        if (_obj is null) {
            throw new ArgumentException(
                "Events must specify a KeyboardKey using WithObject()");
        }
        if (_action is null) {
            return new GameEvent {
                EventType = _type.Value,
                ObjectArg1 = _obj,
                StringArg1 = _debugString,
                IntArg1 = -1
            };
        }
        return new GameEvent {
            EventType = _type.Value,
            ObjectArg1 = _obj,
            StringArg1 = _debugString,
            IntArg1 = _action.Value
        };
    }
}