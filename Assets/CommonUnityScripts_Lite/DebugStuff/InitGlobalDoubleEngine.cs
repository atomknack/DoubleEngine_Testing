#if ! USES_DOUBLEENGINE
using UnityEngine;
public class InitGlobalDoubleEngine : MonoBehaviour {}
#endif

#if USES_DOUBLEENGINE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;

public class InitGlobalDoubleEngine : MonoBehaviour
{
    void Awake()
    {
        DoubleEngine.__GlobalStatic.Init(Application.dataPath, Debug.Log);
    }
}
#endif