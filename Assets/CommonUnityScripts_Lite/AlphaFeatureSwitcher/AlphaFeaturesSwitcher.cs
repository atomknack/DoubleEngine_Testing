using System;
using UnityEngine;

[DefaultExecutionOrder(100501)]
[Obsolete("Use Uknack soevents and value storage")]
public class AlphaFeaturesSwitcher : MonoBehaviour {
    public bool TurnFeaturesOn_AtStart = false;
    public bool TurnFeaturesOff_AtStart = false;
    private void Start()
    {
        if (TurnFeaturesOn_AtStart)
            AlphaFeaturesStatic.enabled = true;
        if (TurnFeaturesOff_AtStart)
            AlphaFeaturesStatic.enabled = false;
    }

    public void Toggle()
    {
        AlphaFeaturesStatic.enabled = !AlphaFeaturesStatic.enabled;
    }
}
