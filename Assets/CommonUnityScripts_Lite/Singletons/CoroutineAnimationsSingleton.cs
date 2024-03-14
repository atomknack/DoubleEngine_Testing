using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineAnimationsSingleton: MonoBehaviour
{
    private static CoroutineAnimationsSingleton _instance = SingletonSourceThatNotDestroyedOnLoadForMonoBehaviour<CoroutineAnimationsSingleton>.Get();
    private static Dictionary<GameObject, Coroutine> _scaleCoroutines = new();

    public static void ChangeScaleFromToOverTime(GameObject gameObject, Vector3 fromScale, Vector3 toScale, float time, Action afterAnimation = null)
    {
        if (_scaleCoroutines.ContainsKey(gameObject))
            _instance.StopCoroutine(_scaleCoroutines[gameObject]);
        _scaleCoroutines.Remove(gameObject);
        var coroutine = _instance.StartCoroutine(ChangeScaleFromToOverTimeCoroutine(gameObject, fromScale, toScale, time, afterAnimation));
        _scaleCoroutines.Add(gameObject, coroutine);
    }

    public static IEnumerator ChangeScaleFromToOverTimeCoroutine(GameObject gameObject, Vector3 fromScale, Vector3 toScale, float time, Action afterAnimation)
    {
        gameObject.transform.localScale = fromScale;
        float passedTime = 0;
        yield return null;
        while (passedTime< time)
        {
            passedTime+=Time.deltaTime;
            float lerp = time == 0? passedTime: passedTime/time;
            gameObject.transform.localScale = Vector3.Lerp(fromScale, toScale, lerp);
            yield return null;
        }
        gameObject.transform.localScale = toScale;
        _scaleCoroutines.Remove(gameObject);
        if (afterAnimation is not null)
            afterAnimation();
    }

    private void Awake()
    {
        SingletonSourceThatNotDestroyedOnLoadForMonoBehaviour<CoroutineAnimationsSingleton>.Set(this); 
    }
    private void OnDestroy()
    {
        _scaleCoroutines.Clear();
    }
}
