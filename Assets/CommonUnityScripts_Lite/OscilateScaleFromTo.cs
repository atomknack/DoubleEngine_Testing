#if USES_DOUBLEENGINE
using DoubleEngine;
using DoubleEngine.UHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UKnack;
using UKnack.Attributes;
using UKnack.Values;
using UnityEngine;
using VectorCore;

public class OscilateScaleFromTo : MonoBehaviour
{
    [SerializeField][ValidReference]
    private SOValue<bool> _additionalEnableConditionStorage;
    
    private Vec3D _fromScale;
    private Vec3D _toScale;
    private double _timeScale;
    bool _enabled = false;

    public void TurnOn(Vec3D fromScale, Vec3D toScale, double timeScale)
    {
        _fromScale = fromScale;
        _toScale = toScale;
        _timeScale = timeScale;
        _enabled = true;
    }
    public void TurnOff(Vec3D scale)
    {
        transform.localScale = scale.ToVector3();
        _enabled = false;
    }

    private void Awake()
    {
        if (_additionalEnableConditionStorage == null)
            throw new NullReferenceException(nameof(_additionalEnableConditionStorage));
    }

    void Update()
    {
        if (!_enabled)
            return;
        if (_additionalEnableConditionStorage.GetValue()==false)
            return;

        var scale = Vec3D.Lerp(_fromScale,_toScale,Math.Abs(Math.Sin(_timeScale * Time.timeAsDouble)));
        transform.localScale = scale.ToVector3();
        
    }
}
#endif