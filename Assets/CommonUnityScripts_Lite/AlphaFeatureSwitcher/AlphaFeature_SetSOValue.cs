using System;
using System.Collections;
using UKnack;
using UKnack.Attributes;
using UKnack.Values;
using UnityEngine;

[Obsolete("Use Uknack soevents and value storage")]
public class AlphaFeature_SetValueStorageValue : AlphaFeatureAbstract
{
    [SerializeField][ValidReference] SOValueMutable<bool> storage;

    public override void DisableFeature()
    {
        ChangeAlphaObjectsState(false);
    }

    public override void EnableFeature()
    {
        ChangeAlphaObjectsState(true);
    }

    private void ChangeAlphaObjectsState(bool state)
    {
            storage.SetValue(state);
    }
}
