/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UKnack;
using UnityEngine.UIElements;
using System;

public class Ý_NotWorks_MakeUINotPickableCommand : UKnack.UI.UIDependant, ICommand
{
    [Tooltip("Set Element name, if empty layout root will be used")]
    [SerializeField] string elementName;
    private VisualElement element;
    private bool _wasAskedToExecute = false;
    private bool _wasExecuted = false;
    private PickingMode savedMode;


    public void Execute()
    {
        _wasAskedToExecute = true;
        ActualExecutionIfPossible();
    }
    public void ReverseExecution()
    {
        if(_wasExecuted == false) 
            return;
        element.pickingMode = savedMode;
        Disarm();
    }
    private void ActualExecutionIfPossible()
    {
        if (_wasAskedToExecute == false)
            return;
        if (_wasExecuted)
            return;
        if (element == null)
            return;
        savedMode = element.pickingMode;
        element.pickingMode = PickingMode.Ignore;
        Debug.Log("picking set to ignore");
        _wasExecuted = true;
    }

    private void Disarm()
    {
        element = null;
        _wasAskedToExecute = false;
        _wasExecuted = false;
    }
    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        SetElement(layout);
        ActualExecutionIfPossible();
    }

    private void SetElement(VisualElement layout)
    {
        if (string.IsNullOrEmpty(elementName))
        {
            element = layout;
            return;
        }
        element = layout.Q<VisualElement>(elementName);
        if (element == null)
            throw new Exception($"visual element name was set to: {elementName} but was not found in provided layout");
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        Disarm();
    }
}
*/