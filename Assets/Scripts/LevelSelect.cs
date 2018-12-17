using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{ 
	public void StartLevel(int levelNum)
    {
        SceneManager.LoadScene("Level" + levelNum);
    }
}
