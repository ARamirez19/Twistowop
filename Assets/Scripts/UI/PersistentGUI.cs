using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGUI : MonoBehaviour
{
    public static PersistentGUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        PersistantTimer.LoadTime();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            PlayerPrefs.DeleteKey("timerYear");
            PlayerPrefs.DeleteKey("timerMonth");
            PlayerPrefs.DeleteKey("timerDay");
            PlayerPrefs.DeleteKey("timerHour");
            PlayerPrefs.DeleteKey("timerMinutes");
            PlayerPrefs.DeleteKey("timerSeconds");
        }
        else if (Input.GetKeyDown(KeyCode.Home))
        {
            PersistantTimer.SaveTime();
        }
    }
}
