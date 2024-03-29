using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnCompleteCallback = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, OnCompleteCallback));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action OnCompleteCallback)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeValue;
        OnCompleteCallback?.Invoke();
    }
}
