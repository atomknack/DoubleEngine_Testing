using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SetBackgroundToColor : MonoBehaviour
{
    public Color newBackground;

    private Camera _camera;
    private Color _oldBackground;
    private CameraClearFlags _oldClearFlag;

    private bool _changed;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        if (_camera == null)
            throw new ArgumentException();
        _changed = false;
    }

    public void Toggle()
    {
        if (_changed)
        {
            ReverseToRemeberedSettings();
            _changed = false;
            return;
        }
        SetToColor();
        _changed = true;
    }

    public void SetToColor()
    {
        _oldBackground = _camera.backgroundColor;
        _oldClearFlag = _camera.clearFlags;

        _camera.backgroundColor = newBackground;
        _camera.clearFlags = CameraClearFlags.SolidColor;
        _changed = true;
    }
    public void ReverseToRemeberedSettings() 
    { 
        _camera.backgroundColor = _oldBackground;
        _camera.clearFlags = _oldClearFlag;
    }
}
