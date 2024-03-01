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

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        int milliseconds = Mathf.FloorToInt(remainingTime * 1000 % 1000);

        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = remainingTime.ToString();

            timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        }
        else if(remainingTime <0)
        {
            remainingTime = 0;
            timerText.text = "00:00.000";
        }
    }
}
