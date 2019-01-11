using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private int destroyObstacleCount;
    [SerializeField]
    private int powerUp2Count;
    [SerializeField]
    private int powerUp3Count;
    [SerializeField]
    private Toggle destroyObstacle;
    [SerializeField]
    private Toggle powerUp2;
    [SerializeField]
    private Toggle powerUp3;
    [SerializeField]
    private GameObject purchaseScreen;

    private Toggle itemToPurchase;

	// Use this for initialization
	void Start ()
    {
        UpdateText();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdatePowerUps()
    {
        if(destroyObstacle.isOn)
        {
            destroyObstacle.GetComponent<PowerUp>().usesLeft--;
            destroyObstacle.isOn = false;
        }
        if(powerUp2.isOn)
        {
            powerUp2.GetComponent<PowerUp>().usesLeft--;
            powerUp2.isOn = false;
        }
        if (powerUp3.isOn)
        {
            powerUp3.GetComponent<PowerUp>().usesLeft--;
            powerUp3.isOn = false;
        }
        UpdateText();
    }

    public void CheckIfEmpty(Toggle t)
    {
        if (t.GetComponent<PowerUp>().usesLeft == 0 && t.isOn)
        {
            t.isOn = false;
            purchaseScreen.SetActive(true);
            itemToPurchase = t;
        }
    }

    public void Purchase()
    {
        itemToPurchase.GetComponent<PowerUp>().usesLeft++;
        purchaseScreen.SetActive(false);
        UpdateText();
    }

    public void CancelPurchase()
    {
        purchaseScreen.SetActive(false);
    }

    private void UpdateText()
    {
        destroyObstacle.GetComponentInChildren<TextMeshProUGUI>().text = destroyObstacle.GetComponent<PowerUp>().usesLeft.ToString();
        powerUp2.GetComponentInChildren<TextMeshProUGUI>().text = powerUp2.GetComponent<PowerUp>().usesLeft.ToString();
        powerUp3.GetComponentInChildren<TextMeshProUGUI>().text = powerUp3.GetComponent<PowerUp>().usesLeft.ToString();
    }
}
