using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingletonSourceThatNotDestroyedOnLoadForMonoBehaviour<T> where T : MonoBehaviour
{
    private static T _instance;

    public static T Get()
    {
        if (!_instance)
        {
            Set(UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
            Debug.LogWarning($"Get for singleton {typeof(T)} was called, but there was not correct instance at time");
        }
        return _instance;
    }

    public static void Set(T newInstance)
    {
        if (_instance)
        {
            Debug.LogWarning($"Set for singleton {typeof(T)} was called, but there was already existing instance");
            if (!ReferenceEquals(_instance, newInstance))
                UnityEngine.Object.DestroyImmediate(newInstance);
        }
        if (!newInstance)
        {
            GameObject singletonPlaceholder = new GameObject($"singletonPlaceholderFor{typeof(T)}");
            singletonPlaceholder.AddComponent<T>();
            newInstance = singletonPlaceholder.GetComponent<T>();
        }
        _instance = newInstance;
        UnityEngine.Object.DontDestroyOnLoad(_instance);
    }
}
