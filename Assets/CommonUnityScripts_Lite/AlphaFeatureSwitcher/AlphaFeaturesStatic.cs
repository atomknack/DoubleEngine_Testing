using System;
using System.Collections.Generic;

public static class AlphaFeaturesStatic
{
    private static List<AlphaFeatureAbstract> s_features = new List<AlphaFeatureAbstract>();
    private static bool s_enabled = true;
    public static bool enabled
    {
        get 
        { 
            return s_enabled; 
        }
        set
        {
            s_enabled = value;
            if (value)
            {
                for (int i = 0; i < s_features.Count; i++)
                    s_features[i].EnableFeature();
            } else
            {
                for (int i = 0; i < s_features.Count; i++)
                    s_features[i].DisableFeature();
            }
        }
    }
    public static void AddFeature(AlphaFeatureAbstract feature) 
    { 
        s_features.Add(feature); 
    }
    public static void RemoveFeature(AlphaFeatureAbstract feature)
    {
        s_features.Remove(feature);
    }
}
