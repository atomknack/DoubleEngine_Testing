using System.Collections;
using System.Collections.Generic;
using UKnack.Values;
using UnityEngine;

namespace UKnack
{

[CreateAssetMenu(fileName = "Defined_UsesDoubleEngine", menuName = "ScriptableObjects/ValueSO/Defined_UsesDoubleEngine", order = 3000)]
public class ValueDefined_UsesDoubleEngine : SOValue<bool>
    {
#if USES_DOUBLEENGINE
        public override bool GetValue() => true;
#else
public override bool GetValue() => false;
#endif
}

}