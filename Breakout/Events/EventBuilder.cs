namespace Breakout.Events;

using System;
using Breakout.GameStates;
using DIKUArcade.Events;
using DIKUArcade.Input;

/// <summary>
/// The way we use events should be enforced.
/// 
/// GameEvent ev = new EventBuilder()
///     .WithType(GameEventType.PlayerEvent)
///     .WithKey(KeyboardKey.Up)
///     .Build();
/// 
/// All other fields from Event are impossible to populate
/// with this class, since we have decided not to use them.
/// Just like you can create events with this class,
/// you can read them with EventDTO.
/// </summary>
public class EventBuilder {

    private Nullable<GameEventType> _type;
    private Nullable<int> _action;
    // Strings and objects are already nullable
    private object _key;
    private string _message;
    private object _gameStateType; // Using Event.from

    public EventBuilder() {
    }

    public EventBuilder WithType(GameEventType eventType) {
        _type = eventType;
        return this;
    }

    public EventBuilder WithKey(KeyboardKey key) {
        _key = key;
        return this;
    }

    public EventBuilder WithStateType(GameStateType stateType) {
        _gameStateType = stateType;
        return this;
    }

    public EventBuilder WithAction(KeyboardAction action) {
        _action = (int) action;
        return this;
    }

    public EventBuilder WithMessage(string message) {
        _message = message;
        return this;
    }

    public GameEvent Build() {
        if (_type is null) {
            throw new ArgumentException(
                "Events must specify an event type using WithType()");
        }
        if (_gameStateType is null &&
            _key is null) {
            throw new ArgumentException(
                "Events must provide either a KeyboardKey " +
                "or a GameStateType"
            );
        }

        GameEvent ev = new GameEvent();
        // Required fields
        ev.EventType = _type.Value;
        // Optional fields
        if (!(_key is null)) {
            ev.ObjectArg1 = _key;
        }
        if (!(_gameStateType is null)) {
            ev.From = _gameStateType;
        }
        if (!(_message is null)) {
            ev.StringArg1 = _message;
        }
        if (!(_action is null)) {
            ev.IntArg1 = _action.Value;
        } else {
            ev.IntArg1 = -1;
        }
        return ev;
    }
}