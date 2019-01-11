using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private int starCount;
    [SerializeField]
    private List<GameObject> stars;

    void Start()
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

    public void SetStarCount(int count)
    {
        if (count <= 3)
            starCount = count;
        else
            Debug.Log("Only 3 Stars may be passed per level");
            
    }
    public int StarCount()
    {
        return starCount;
    }

}
