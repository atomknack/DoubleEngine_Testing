#if USES_DOUBLEENGINE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;

public class ReplaceMaterialProperties : MonoBehaviour
{
    private int _index;
    public Material Changeable;
    public Material[] Replacements;
    public bool ReplaceOnAwake = true;
    //public KeyCode PressToChangeMaterial = KeyCode.None;

    private void Awake()
    {
        _index = 0;
        if (ReplaceOnAwake)
        {
            ReplaceShaderAndMaterialProperties(Changeable, Replacements[_index]);
            _index.NextIntCyclicRef(Replacements.Length);
        }
    }

    public void ReplaceMaterialsCyclic()
    {
        ReplaceShaderAndMaterialProperties(Changeable, Replacements[_index]);
        _index.NextIntCyclicRef(Replacements.Length);
    }

    public static void ReplaceShaderAndMaterialProperties(Material changeable, Material donor)
    {
        changeable.shader = donor.shader;
        changeable.CopyPropertiesFromMaterial(donor);
    }

    /*
// Update is called once per frame
void Update()
{
    if (PressToChangeMaterial == KeyCode.None)
        return;
    if (Input.GetKeyDown(PressToChangeMaterial))
    {
        ReplaceMaterialsCyclic();
    }
}*/
}
#endif