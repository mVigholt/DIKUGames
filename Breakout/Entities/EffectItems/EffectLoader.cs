namespace Breakout.Entities.EffectItems;

using System;
using System.Linq;
using System.Reflection;
using DIKUArcade.Entities;

public class EffectLoader : TypeLoader<IEffect> {

    private Shuttle _shuttle;
    private ScoreBoard _scoreBoard;
    private EntityContainer<Ball> _activeBalls;

    public EffectLoader(
        Shuttle shuttle,
        ScoreBoard scoreBoard,
        EntityContainer<Ball> activeBalls
    ) : base("Breakout.Entities.EffectItems.Effects") {
        // These are the references needed
        // to create all kinds of IEffect
        _shuttle = shuttle;
        _scoreBoard = scoreBoard;
        _activeBalls = activeBalls;
    }

    public override IEffect CreateInstance(Type type) {
        ConstructorInfo[] constructors = type.GetConstructors();
        ConstructorInfo constructor = constructors.FirstOrDefault();
        if (constructor != null) {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] args = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++) {
                ParameterInfo parameter = parameters[i];
                if (parameter.ParameterType == typeof(Shuttle)) {
                    args[i] = _shuttle;
                }
                else if (parameter.ParameterType == typeof(ScoreBoard)) {
                    args[i] = _scoreBoard;
                }
                else if (parameter.ParameterType == typeof(EntityContainer<Ball>)) {
                    args[i] = _activeBalls;
                }
                else {
                    throw new ArgumentException(
                        $"Could not create an instance of type {type}"
                    );
                }
            }
            return (IEffect)constructor.Invoke(args);
        }
        return null;
    }
}