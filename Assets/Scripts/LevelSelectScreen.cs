using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    [SerializeField]
    private RectTransform screen;
    [SerializeField]
    private List<GameObject> themes;
    //public int current=0;
    public float size;
    // Use this for initialization
    void Start()
    {
        foreach (Transform theme in GetComponentsInChildren<Transform>())
        {
            if (theme.tag == "Theme")
            {
                themes.Add(theme.gameObject);
            }
        }
        float width = screen.GetComponent<RectTransform>().sizeDelta.x;
        for(int i=0; i<themes.Count; i++)
        {
            size += themes[i].GetComponent<RectTransform>().sizeDelta.x;
        }
        Debug.Log(size);
       GetComponent<RectTransform>().offsetMax = new Vector2(size-width, 0);
    }
}
