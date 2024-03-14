using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ToggleEventBoolValue : MonoBehaviour
{
    public string ToggleName;
    [Tooltip("On behavior OnEnable check toggle value and call event with toggle value")]
    public bool OnScriptEnableCallOnChange = false;
    public UnityEvent<bool> onChange;
    private Toggle _toggle;
    private EventCallback<ChangeEvent<bool>> _eventCallback;

    private void OnEnable()
    {
        VisualElement rootUI = GetComponent<UIDocument>().rootVisualElement;
        _toggle = rootUI.Q<Toggle>(ToggleName);
        if (_toggle == null)
            throw new Exception($"toggle {ToggleName} not found");
        _eventCallback = ev => onChange.Invoke(ev.newValue);
        _toggle.RegisterCallback<ChangeEvent<bool>>(_eventCallback);
        if (OnScriptEnableCallOnChange)
        {
                onChange.Invoke(_toggle.value);
        }
    }

    private void OnDisable()
    {
        if (_toggle == null)
            throw new Exception($"toggle {ToggleName} not found");
        _toggle.UnregisterCallback<ChangeEvent<bool>>(_eventCallback);
        _toggle = null;
        _eventCallback = null;
    }
}
