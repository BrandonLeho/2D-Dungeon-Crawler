using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReset : MonoBehaviour
{
    public void Reset()
    {
        TimeManager.instance.ResetTime();
    }
}
