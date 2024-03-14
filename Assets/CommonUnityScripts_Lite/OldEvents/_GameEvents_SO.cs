/*
#if USES_DOUBLEENGINE
using AtomKnackGame;
using DoubleEngine.Atom;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects_GameEvents_Static")]
[Obsolete("DO NOT use, use static SOEvent from UKnack instead")] 
public class GameEvents_SO : ScriptableObject //use static GameEvents from AtomKnackGame Assembly instead
{
    public void Publish_PlaceholderCellChanged(short value) => GameEvents.placeholderCellChanged.Publish(value);
    public void Publish_PlaceholderOrientationChanged_RotateZ90() => 
        GameEvents.placeholderOrientationChanged.Publish(
        Grid6SidesCached.FromRotationAndScale(GameEvents.placeholderOrientationChanged.Value).
        RotateZ90()._orientation);

    public void Publish_PlaceholderOrientationChanged_RotateX90() =>
        GameEvents.placeholderOrientationChanged.Publish(
        Grid6SidesCached.FromRotationAndScale(GameEvents.placeholderOrientationChanged.Value).
        RotateX90()._orientation);

    public void Publish_PlaceholderOrientationChanged_RotateY90() =>
        GameEvents.placeholderOrientationChanged.Publish(
        Grid6SidesCached.FromRotationAndScale(GameEvents.placeholderOrientationChanged.Value).
        RotateY90()._orientation);

    public void Publish_PlaceholderOrientationChanged_InvertZ() =>
        GameEvents.placeholderOrientationChanged.Publish(
        Grid6SidesCached.FromRotationAndScale(GameEvents.placeholderOrientationChanged.Value).
        InvertZ()._orientation);

    public void Publish_PlaceholderOrientationChanged_InvertX() =>
        GameEvents.placeholderOrientationChanged.Publish(
        Grid6SidesCached.FromRotationAndScale(GameEvents.placeholderOrientationChanged.Value).
        InvertX()._orientation);

    public void Publish_PlaceholderOrientationChanged_InvertY() =>
        GameEvents.placeholderOrientationChanged.Publish(
        Grid6SidesCached.FromRotationAndScale(GameEvents.placeholderOrientationChanged.Value).
        InvertY()._orientation);


}
#endif
*/