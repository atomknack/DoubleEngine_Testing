using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFPS : MonoBehaviour
{
    public bool disableVSync = false;
    public int TargetFPS = 120;
    void Start()
    {
        if (disableVSync)
            QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = TargetFPS;
    }
}
