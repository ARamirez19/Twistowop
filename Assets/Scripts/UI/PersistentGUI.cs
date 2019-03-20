using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGUI : MonoBehaviour
{
    public static PersistentGUI Instance { get; private set; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "VisualTest")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        PersistantTimer.LoadTime();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
