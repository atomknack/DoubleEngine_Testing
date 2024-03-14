using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UKnack;

[Obsolete("Use new input system, by UtilityKnack.EventPlayerInput", true)]
public class CallEventOnKeyPressed : MonoBehaviour
{
    public KeyCode k;
    public UnityEvent e;

    private void Update()
    {
        if (Input.GetKeyDown(k))
            e.Invoke();
    }
}
