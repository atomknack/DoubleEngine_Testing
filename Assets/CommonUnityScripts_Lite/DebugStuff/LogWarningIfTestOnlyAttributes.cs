#if USES_DOUBLEENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DoubleEngine;

//logs for any fount type with attribute ( used / NOT used is NOT discrimintated )
public class LogWarningIfTestOnlyAttributes : MonoBehaviour
{
    public static void DoActionForTypeInAllAssemblies(Type attribute, Action<Type> actionForFoundType)
    {
        // this is making the assumption that all assemblies we need are already loaded.
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                    var attribs = type.GetCustomAttributes(attribute, false);
                    if (attribs != null && attribs.Length > 0)
                    {
                        actionForFoundType(type);
                    }
            }
        }
    }
    public void Awake()
    {
        DoActionForTypeInAllAssemblies(typeof(TestingOnlyAttribute), (type) => Debug.Log($"{type.Name} is used but should not be"));
    }
}
#endif