using System.Collections;
using System.Collections.Generic;
using UKnack;
using UKnack.Events;
using UnityEngine;

public class ErrorousScriptThatNotUnsubscribe20221214 : MonoBehaviour
{
    [SerializeField] private SOEvent soEvent;
    void Start()
    {
        soEvent.Subscribe(LogItself);
    }

    private void LogItself()
    {
        Debug.Log("ErrorousScriptThatNotUnsubscribe20221214 was called by event");
    }

}
