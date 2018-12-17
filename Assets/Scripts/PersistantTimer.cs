using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistantTimer
{
    private static DateTime loadedTime = new DateTime();
    public static DateTime LoadedTime { get { return loadedTime; } }

    public static bool CompareTime(int minutesToReach)
    {
        TimeSpan timeDifference = DateTime.Now - LoadedTime;
        if (timeDifference.TotalMinutes >= minutesToReach)
        {
            return true;
        }
        return false;
    }

    public static void SaveTime()
    {
        //save timer to server, player prefs temp
        PlayerPrefs.SetInt("timerYear", DateTime.Now.Year);
        PlayerPrefs.SetInt("timerMonth", DateTime.Now.Month);
        PlayerPrefs.SetInt("timerDay", DateTime.Now.Day);
        PlayerPrefs.SetInt("timerHour", DateTime.Now.Hour);
        PlayerPrefs.SetInt("timerMinutes", DateTime.Now.Minute);
        PlayerPrefs.SetInt("timerSeconds", DateTime.Now.Second);
        PlayerPrefs.Save();
    }

    public static void LoadTime()
    {
        //load from server, player prefs temp
        int year = PlayerPrefs.GetInt("timerYear", 1);
        int month = PlayerPrefs.GetInt("timerMonth", 1);
        int day = PlayerPrefs.GetInt("timerDay", 1);
        int hour = PlayerPrefs.GetInt("timerHour", 0);
        int minutes = PlayerPrefs.GetInt("timerMinutes", 0);
        int seconds = PlayerPrefs.GetInt("timerSeconds", 0);

        loadedTime = new DateTime(year, month, day, hour, minutes, seconds);
    }
}
