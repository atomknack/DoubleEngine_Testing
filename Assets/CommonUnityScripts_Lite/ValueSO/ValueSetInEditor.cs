using System.Collections;
using System.Collections.Generic;
using UKnack.Values;
using UnityEngine;

namespace UKnack
{

    [CreateAssetMenu(fileName = "ValueSetInEditor", menuName = "ScriptableObjects/ValueSO/ValueSetInEditor", order = 100)]
    public class ValueSetInEditor : SOValue<bool>
    {
        [SerializeField] protected bool _value = false;
        public override bool GetValue() => _value;
    }
}