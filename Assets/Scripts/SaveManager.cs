using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    private const string prefix = "Level ";

    private static SaveManager _instance = null;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }

    public void SaveStars(int level, int stars)
    {
        PlayerPrefs.SetInt(prefix + level.ToString(), stars);
        PlayerPrefs.Save();
    }

    public int LoadStars(int level)
    {
        return PlayerPrefs.GetInt(prefix + level.ToString());
    }

#if UNITY_EDITOR
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
