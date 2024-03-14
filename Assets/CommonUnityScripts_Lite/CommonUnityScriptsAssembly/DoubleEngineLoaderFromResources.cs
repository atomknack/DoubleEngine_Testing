#if USES_DOUBLEENGINE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine.Atom.Loaders;

public class DoubleEngineLoaderFromResources : IEngineLoader
{
    private string prefix;

    public DoubleEngineLoaderFromResources(string prefix)
    {
        this.prefix = prefix;
    }

    public string LoadFlatNodes() => LoadResourceAsText(prefix + "flatnodesData");
    public string LoadTDCellMeshes() => LoadResourceAsText(prefix + "cellMeshesData");

    private string LoadResourceAsText(string resourceName)
    {
        TextAsset textAsset = Resources.Load(resourceName) as TextAsset;
#if DEBUG
        if (textAsset == null)
        {
            Debug.Log($"resource not found: {resourceName}");
        }
#endif
        return textAsset.text;
    }
}
#endif