using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoostPanel : MonoBehaviour
{
    public TextMeshProUGUI cooldownNumber;
    public TextMeshProUGUI boostNumber;

    private PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnCooldownSlide(float value)
    {
        player.boostDelay = value;
        cooldownNumber.text = value + "s";
    }

    public void OnBoostSlide(float value)
    {
        player.boostSpeed = value;
        boostNumber.text = value.ToString();
    }
}
