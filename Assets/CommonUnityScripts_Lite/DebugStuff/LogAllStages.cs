using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogAllStages : MonoBehaviour
{
    [SerializeField] private string prefix;
    [SerializeField] private int logUpdateEveryNFrame = 100;
    private int updateCounter = 0;
    [SerializeField] private int logFixedUpdateEveryN = 30;
    private int fixedUpdateCounter = 0;

    private void Awake() => Log("Awake");
    private void Start() => Log("Start");
    private void OnEnable() => Log("OnEnable");
    private void OnDisable() => Log("OnDisable");
    private void OnDestroy() => Log("OnDestroy");

    private void FixedUpdate()
    {
        if (logFixedUpdateEveryN < 0)
            return;
        ++fixedUpdateCounter;
        if (fixedUpdateCounter >= logFixedUpdateEveryN)
        {
            Log($"FixedUpdate after {logFixedUpdateEveryN}");
            fixedUpdateCounter = -1;
        }
    }
    private void Update()
    {
        if (logUpdateEveryNFrame < 0)
            return;
        ++updateCounter;
        if (updateCounter >= logUpdateEveryNFrame)
        {
            Log($"Update after {logUpdateEveryNFrame}");
            updateCounter = -1;
        }
    }

    private void Log(string s)
    {
        Debug.Log($"{gameObject.name}_{prefix}: {s}");
    }
}
