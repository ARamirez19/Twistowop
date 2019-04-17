using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField]
    private int baseLives = 25;
    private int deathCounter;
    private int lives;

    private Image heart;

    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        heart = GetComponentInChildren<Image>();
        livesText = GetComponentInChildren<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("Lives"))
        {
            lives = PlayerPrefs.GetInt("Lives");
            UpdateLives();
        }
        else
        {
            livesText.text = baseLives.ToString();
            PlayerPrefs.SetInt("Lives", baseLives);
        }
        if (PlayerPrefs.HasKey("DeathCounter"))
        {
            deathCounter = PlayerPrefs.GetInt("DeathCounter");
            UpdateHeart();
        }
        else
        {
            deathCounter = 3;
            PlayerPrefs.SetInt("DeathCounter", deathCounter);
            UpdateHeart();
        }
    }

    private void Update()
    {
       if(PlayerPrefs.HasKey("DeathCounter"))
       {
           if (PlayerPrefs.GetInt("DeathCounter") != deathCounter)
           {
                deathCounter = PlayerPrefs.GetInt("DeathCounter");
                UpdateHeart();
            }
       }
       if(PlayerPrefs.HasKey("Lives"))
       {
            if (PlayerPrefs.GetInt("Lives") != lives)
            {   
                lives = PlayerPrefs.GetInt("Lives");
                UpdateLives();
            }
       }
    }


    public void UpdateLives()
    {
        livesText.text = PlayerPrefs.GetInt("Lives").ToString();
    }

    public void UpdateHeart()
    {
        heart.fillAmount = ((float)deathCounter/3.0f);
    }
}
