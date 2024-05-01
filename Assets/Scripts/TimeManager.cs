using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private Countdown countdown;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        countdown = FindObjectOfType<Countdown>(); // You can adjust this to your scene setup
    }

    public void ResetTime()
    {
        countdown.TimeReset();
    }
}
