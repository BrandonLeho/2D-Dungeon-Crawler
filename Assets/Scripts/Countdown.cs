using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.FullSerializer;

public class Countdown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] float timeToCountdown;
    private int minutes, seconds, milliseconds;

    // Update is called once per frame
    void Update()
    {
        minutes = Mathf.FloorToInt(remainingTime / 60);
        seconds = Mathf.FloorToInt(remainingTime % 60);
        milliseconds = Mathf.FloorToInt(remainingTime * 1000 % 1000);

        if(remainingTime > 0)
        {
            remainingTime -= Time.unscaledDeltaTime;
            timerText.text = remainingTime.ToString();

            timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        }
        else if(remainingTime <0)
        {
            remainingTime = 0;
            timerText.text = "00:00.000";
        }
    }

    public void TimeReset()
    {
        remainingTime = timeToCountdown;
    }
}
