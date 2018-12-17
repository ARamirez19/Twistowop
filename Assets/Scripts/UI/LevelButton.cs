using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private int starCount;

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
