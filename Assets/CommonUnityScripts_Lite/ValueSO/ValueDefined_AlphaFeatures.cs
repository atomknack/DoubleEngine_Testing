using System.Collections;
using System.Collections.Generic;
using UKnack.Values;
using UnityEngine;

namespace UKnack
{

    [CreateAssetMenu(fileName = "Defined_ALPHAFEATURES", menuName = "ScriptableObjects/ValueSO/Defined_ALPHAFEATURES", order = 5000)]
    public class ValueDefined_AlphaFeatures : SOValue<bool>
    {
#if ALPHAFEATURES
protected override bool GetValue() => true;
#else
        public override bool GetValue() => false;
#endif
    }
}