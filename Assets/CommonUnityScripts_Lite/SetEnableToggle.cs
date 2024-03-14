using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnableToggle : MonoBehaviour
{
    public GameObject targetGameObject;
    public bool boolValueOfFirstToggle;
    private bool _onOff;

    public void OnEnable()
    {
        if (targetGameObject == null)
            throw new ArgumentException(nameof(targetGameObject));
        _onOff = boolValueOfFirstToggle;
    }

    public void Toggle()
    {
        _onOff = ! _onOff;
        if (_onOff)
        {
            targetGameObject.SetActive(false);
            return;
        }
        targetGameObject.SetActive(true);
    }
}
