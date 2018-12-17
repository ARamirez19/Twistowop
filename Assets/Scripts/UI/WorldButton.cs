using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldButton : MonoBehaviour
{
    [SerializeField]
    private int starCount = 0;
    [SerializeField]
    private Button[] levels;
    [SerializeField]
    private GameObject levelSelect;
	// Use this for initialization
	void Start ()
    {
	    foreach(Button button in levels)
        {
            starCount += button.GetComponent<LevelButton>().StarCount();
        }
        levelSelect.GetComponent<LevelSelectController>().UpdateWorldText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int StarCount()
    {
        return starCount;
    }
}
