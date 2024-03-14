//P 2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourNoAwakeInit : MonoBehaviour
{
    private class Construturizer
    {
        private static bool wasCalled = false;

        private void Init()
        {
            //Place init code here
            Debug.Log("init");
        }
        public Construturizer()
        {
            if(wasCalled)
            {
                Debug.Log("Already was called, no need for second time");
                return;
            }
            Debug.Log("Should be called even from non active gameobject, even before Awake, every time Game started, can be called multiple times");
            wasCalled = true;
            Init();
        }
    }

    private Construturizer _construturizer = new Construturizer();
}
