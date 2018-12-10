using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    [SerializeField]
    RectTransform screen;
    List<GameObject> themes;
	// Use this for initialization
	void Start ()
    {
        float width = screen.GetComponent<RectTransform>().sizeDelta.x;
        this.GetComponent<RectTransform>().offsetMax = new Vector2(width * 4, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

   
}
