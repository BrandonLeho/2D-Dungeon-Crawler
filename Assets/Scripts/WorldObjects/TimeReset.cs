using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReset : MonoBehaviour
{
    [SerializeField] private Countdown countdown;
    public void Reset()
    {
        countdown.TimeReset();
    }
}
