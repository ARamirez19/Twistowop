using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCompleteController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTakenText = null;
    [SerializeField] private TextMeshProUGUI recommendedTimeText = null;
    [SerializeField] private TextMeshProUGUI freezedUsedText = null;
    [SerializeField] private Image[] stars = null;

    private int starIndex = 0;
    private int starsToAdd = 0;

    public void OnEnable()
    {
        this.GetComponent<FadeEffect>().onFadeInComplete += AnimateStars;
    }

    public void CompleteLevel(int level, double timeTaken, double recommendedTime, int timesFrozen)
    {
        Scene currScene = SceneManager.GetActiveScene();
        ResetData();
        starsToAdd++;
        timeTakenText.text = timeTaken.ToString();
        recommendedTimeText.text = recommendedTime.ToString();
        freezedUsedText.text = timesFrozen.ToString();

        if (timeTaken < recommendedTime)
        {
            starsToAdd++;
        }

        if (LevelManager.GetInstance().CurrentCollectableCount == LevelManager.GetInstance().CollectableAmount)
        {
            starsToAdd++;
        }

        Debug.Log(currScene.name + "stars: " + starsToAdd);
        if (PlayerPrefs.HasKey(currScene.name + "stars"))
        {
            if (PlayerPrefs.GetInt(currScene.name + "stars") < starsToAdd)
            {
                Debug.Log("Added Stars: Key Already Exists");
                PlayerPrefs.SetInt(currScene.name + "stars", starsToAdd);
            }
        }
        else
        {
            Debug.Log("Added Stars: Key Created");
            PlayerPrefs.SetInt(currScene.name + "stars", starsToAdd);
            Debug.Log("Stars Added Successfull if stars here: " + PlayerPrefs.GetInt(currScene.name + "stars"));
        }
        SaveManager.Instance.SaveStars(level, starsToAdd);
    }

    public void NextStar()
    {
        if (starIndex < starsToAdd - 1)
        {
            starIndex++;
            stars[starIndex].gameObject.SetActive(true);
        }
    }

    private void AnimateStars()
    {
        stars[0].gameObject.SetActive(true);
    }

    private void ResetData()
    {
        starIndex = 0;
        starsToAdd = 0;
        timeTakenText.text = "";
        recommendedTimeText.text = "";
        freezedUsedText.text = "";
    }
}
