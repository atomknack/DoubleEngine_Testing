using System;
using System.Collections;
using UnityEngine;

[Obsolete("Use Uknack soevents and value storage")]
public class AlphaFeature_EnableDisableGameobjects : AlphaFeatureAbstract
{
    public GameObject[] alphaObjects = new GameObject[0];

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
        for (int i = 0; i < alphaObjects.Length; i++)
            alphaObjects[i].SetActive(state);
    }
}
