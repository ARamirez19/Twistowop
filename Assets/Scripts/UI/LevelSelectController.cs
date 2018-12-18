using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField]
    private GameObject worldWindow;
    [SerializeField]
    private GameObject levelWindow;
    [SerializeField]
    private GameObject scrollContent;
    [SerializeField]
    private TextMeshProUGUI[] worldText;
    [SerializeField]
    private int numberOfWorlds;
    [SerializeField]
    private int currentWorld;
    [SerializeField]
    private int worldsAvaliable;

    private GameObject[] worlds;
    private GameObject[] worldLevels;

 
	// Use this for initialization
	void Start ()
    {
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * numberOfWorlds, 0);
        scrollContent.GetComponent<RectTransform>().position = new Vector2((numberOfWorlds-currentWorld)*this.GetComponent<RectTransform>().position.x, this.GetComponent<RectTransform>().position.y);

        worlds = new GameObject[numberOfWorlds];
        worldLevels = new GameObject[numberOfWorlds];
        for (int i=0; i<numberOfWorlds; i++)
        {
            worlds[i] = GameObject.Find("World" + (i+1));
            worldLevels[i] = GameObject.Find("World" + ((i+1)) + "Levels");
            worldLevels[i].SetActive(false);
        }
        levelWindow.SetActive(false);
        WorldsUnlocked();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void BackToWorldSelect()
    {
        worldWindow.SetActive(true);
        levelWindow.SetActive(false);
        foreach (GameObject level in worldLevels)
        {
            level.SetActive(false);
        }
        scrollContent.GetComponent<RectTransform>().position = new Vector2((numberOfWorlds - currentWorld) * this.GetComponent<RectTransform>().position.x, this.GetComponent<RectTransform>().position.y);
    }
    public void WorldSelect(int worldNumber)
    {
        currentWorld = worldNumber;
        levelWindow.SetActive(true);
        worldLevels[worldNumber - 1].SetActive(true);
        worldWindow.SetActive(false);
    }

    public void UpdateWorldText()
    {
        for (int i = 0; i < numberOfWorlds; i++)
        {
            worldText[i].GetComponent<TextMeshProUGUI>().text = worlds[i].GetComponent<WorldButton>().StarCount().ToString() + "/63";
        }
    }

    private void WorldsUnlocked()
    {
        for(int i=0; i<worldsAvaliable; i++)
        {
            worlds[i].GetComponent<Button>().interactable = true;
        }
    }

    public void StartLevel(int levelNum)
    {
        SceneManager.LoadScene("World" + currentWorld + "Level" + levelNum);
    }
}