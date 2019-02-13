using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarMeter : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private int starsNeeded;
    [SerializeField]
    private int currentStars;
    [SerializeField]
    private TextMeshProUGUI starsText;

    void Update()
    {
        starsText.text = currentStars.ToString() + "/" + starsNeeded.ToString();

        if (currentStars == 0)
        {
            progressBar.fillAmount = 0;
        }
        else if (currentStars > starsNeeded)
        {
            progressBar.fillAmount = 1;
        }
        else
        {
            progressBar.fillAmount = (float)currentStars/ (float)starsNeeded;
        }

    }
}
