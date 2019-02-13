using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    bool open = false;
    [SerializeField]
    private GameObject optionsMenu;
    GameObject options;

    public void OpenOptions()
    {
        if(!open)
        {
            optionsMenu.SetActive(true);
            open = true;
        }
        else
        {
            optionsMenu.SetActive(false);
            open = false;
        }
    }
}
