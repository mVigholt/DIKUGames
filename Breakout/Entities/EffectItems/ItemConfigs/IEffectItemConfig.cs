namespace Breakout.Entities.EffectItems.ItemConfigs;


/// <summary>
/// The information needed to create an EffectItem in factory.
/// To create a new kind of EffectItem, simply create a class
/// that extends this interface and place it inside this namespace.
/// </summary>
public interface IEffectItemConfig {

    /// <summary>
    /// The file name of the image used for
    /// a particular EffectItem.
    /// </summary>
    string IconFileName { get; }

    /// <summary>
    /// If true, the implementing class will be
    /// associated with an ITimedEffect that
    /// contains a Deactivate method.
    /// </summary>
    bool IsTimed { get; }
}