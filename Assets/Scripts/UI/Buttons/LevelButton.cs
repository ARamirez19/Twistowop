using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private int starCount;
    [SerializeField]
    private List<GameObject> stars;

    private int b_world;
    private int b_level;

    private void CreateStars()
    {

        RectTransform[] s = GetComponentsInChildren<RectTransform>();
        for (int i=0; i<s.Length; i++)
        {
            if(s[i].name == "Star1" || s[i].name == "Star2" || s[i].name == "Star3")
            {
                stars.Add(s[i].gameObject);
            }
        }
        foreach(GameObject star in stars)
        {
            star.SetActive(false);
        }
        for(int i=0; i<starCount; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void SetStarCount()
    {
        if (PlayerPrefs.HasKey("World" + b_world + "Level" + b_level + "stars"))
        {
            starCount = PlayerPrefs.GetInt("World" + b_world + "Level" + b_level + "stars");
        }
        else
        {
            starCount = 0;
        }
        CreateStars();
    }
    public int StarCount()
    {
        return starCount;
    }

    public void SetLevelInfo(int world, int level)
    {
        b_world = world;
        b_level = level;
    }
}
