using System.Collections;
using System.Collections.Generic;
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

    public void CompleteLevel(double timeTaken, double recommendedTime, int timesFrozen)
    {
        ResetData();
        starsToAdd++;
        timeTakenText.text = timeTaken.ToString();
        recommendedTimeText.text = recommendedTime.ToString();
        freezedUsedText.text = timesFrozen.ToString();

        if (timeTaken < recommendedTime)
        {
            starsToAdd++;
        }
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
