using System.Collections;
using System.Collections.Generic;
using UKnack.Values;
using UnityEngine;

namespace UKnack
{
    [CreateAssetMenu(fileName = "Defined_Patreon", menuName = "ScriptableObjects/ValueSO/Defined_Patreon", order = 4000)]
    public class ValueDefined_Patreon : SOValue<bool>
    {
#if PATREON
protected override bool GetValue() => true;
#else
        public override bool GetValue() => false;
#endif
    }
}