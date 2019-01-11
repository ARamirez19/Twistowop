using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectController : MonoBehaviour
{
    [Tooltip("Reference to the World Object in the Hierarchy")]
    [SerializeField]
    private GameObject worldWindow;
    [Tooltip("Reference to the Level Object in the Hierarchy")]
    [SerializeField]
    private GameObject levelWindow;
    [SerializeField]
    private GameObject scrollContent;
    [Tooltip("The StarCount Text for each world")]
    [SerializeField]
    private TextMeshProUGUI[] worldText;
    [Tooltip("The number of worlds the player has access to")]
    [SerializeField]
    private int numberOfWorlds;
    [SerializeField]
    private int worldsAvaliable;
    [Tooltip("The last world visted by the player")]
    [SerializeField]
    private int currentWorld;

    private GameObject[] worlds;
    private GameObject[] worldLevels;


    // Use this for initialization
    void Start()
    {
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * numberOfWorlds, 0);
        scrollContent.GetComponent<RectTransform>().position = new Vector2((numberOfWorlds - currentWorld) * this.GetComponent<RectTransform>().position.x, this.GetComponent<RectTransform>().position.y);

        worlds = new GameObject[numberOfWorlds];
        worldLevels = new GameObject[numberOfWorlds];
        for (int i = 0; i < numberOfWorlds; i++)
        {
            worlds[i] = GameObject.Find("World" + (i + 1));
            worldLevels[i] = GameObject.Find("World" + ((i + 1)) + "Levels");
            worldLevels[i].SetActive(false);
            UpdateWorldText(i);
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

    public void UpdateWorldText(int world)
    {
        int totalStars = 0;
        Button[] levels = worldLevels[world].GetComponentsInChildren<Button>();
        foreach (Button level in levels)
        {
            totalStars += level.GetComponent<LevelButton>().StarCount();
        }
        worldText[world].GetComponent<TextMeshProUGUI>().text = totalStars.ToString() + "/63";
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