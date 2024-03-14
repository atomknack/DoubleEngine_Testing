using UnityEngine;

public abstract class AlphaFeatureAbstract: MonoBehaviour
{
    public abstract void EnableFeature();
    public abstract void DisableFeature();
    private void OnEnable()
    {
        AlphaFeaturesStatic.AddFeature(this);
    }
    private void OnDisable()
    {
        AlphaFeaturesStatic.RemoveFeature(this);
    }
}
