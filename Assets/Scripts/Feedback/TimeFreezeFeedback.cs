using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeFeedback : Feedback
{
    [SerializeField] private float freezeTimeDelay = 0.01f, unfreezeTimeDelay = 0.02f;
    [SerializeField] [Range(0, 1)] private float timeFreezeValue = 0.2f;

    public override void CompletePreviousFeedback()
    {
        if(TimeController.Instance != null)
            TimeController.Instance.ResetTimeScale();
    }

    public override void CreateFeedback()
    {
        TimeController.Instance.ModifyTimeScale(timeFreezeValue, freezeTimeDelay, () =>
            TimeController.Instance.ModifyTimeScale(1, unfreezeTimeDelay));
    }
}
