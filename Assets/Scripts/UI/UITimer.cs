using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITimer : MonoBehaviour
{
    public bool hourly = false;
    public int defaultTimerTimeMinutes = 0;
    public TextMeshProUGUI timerText;

    private int timerUpdateTime = 1;
    private TimeSpan currentTimer = TimeSpan.MinValue;
    private WaitForSeconds timerDelay = null;

    void Start()
    {
        SetUpTimer();
        StartCoroutine(TimerUpdateRoutine());
    }

    public void ReducteTimerMinutes(int minutes)
    {
        currentTimer = currentTimer.Subtract(new TimeSpan(0, minutes, 0));
    }

    private IEnumerator TimerUpdateRoutine()
    {
        while (currentTimer.Minutes >= 0 && currentTimer.Seconds >= 0)
        {
            yield return timerDelay;
            UpdateTimer();
        }
        OnTimerComplete();
    }

    private void UpdateTimer()
    {
        currentTimer = currentTimer.Subtract(new TimeSpan(0, 0, timerUpdateTime));

        if (hourly)
        {
            timerText.text = currentTimer.Hours + ":" + (currentTimer.Minutes);
        }
        else
        {
            timerText.text = currentTimer.Minutes + ":" + (currentTimer.Seconds);
        }
    }

    private void OnTimerComplete()
    {
        Debug.Log("Timer completed on: " + name);
    }

    private void SetUpTimer()
    {
        timerDelay = new WaitForSeconds(timerUpdateTime);
        currentTimer = new TimeSpan(0, defaultTimerTimeMinutes, 0);

        DateTime loadedTime = PersistantTimer.LoadedTime;
        if (loadedTime != DateTime.MinValue)
        {
            TimeSpan timeOffset = DateTime.Now - loadedTime;
            currentTimer = currentTimer.Subtract(timeOffset);
        }
    }
}
