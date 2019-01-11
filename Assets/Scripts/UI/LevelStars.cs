using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStars : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<TextMeshProUGUI>().text = this.GetComponentInParent<LevelButton>().StarCount().ToString();
	}
}
