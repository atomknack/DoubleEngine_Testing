using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ToggleEventTwoUnityEvents : MonoBehaviour
{
    public string ToggleName;
    [Tooltip("On behavior OnEnable check toggle value and call corresponding event")]
    public bool OnScriptEnableCheckValue = false;
    public UnityEvent onTrue;
    public UnityEvent onFalse;
    private Toggle _toggle;
    private EventCallback<ChangeEvent<bool>> _eventCallback;

    private void OnEnable()
    {
        VisualElement rootUI = GetComponent<UIDocument>().rootVisualElement;
        _toggle = rootUI.Q<Toggle>(ToggleName);
        if (_toggle == null)
            throw new Exception($"toggle {ToggleName} not found");
        _eventCallback = ev => { if (ev.newValue) onTrue.Invoke(); else onFalse.Invoke(); };
        _toggle.RegisterCallback<ChangeEvent<bool>>(_eventCallback);
        if (OnScriptEnableCheckValue)
        {
            if (_toggle.value)
                onTrue.Invoke();
            else
                onFalse.Invoke();
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
