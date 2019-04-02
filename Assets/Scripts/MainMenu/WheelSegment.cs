using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSegment : MonoBehaviour
{
    [SerializeField]
    private string prize;
    [SerializeField]
    private GameObject wheel;

    private void Start()
    {
        wheel = GameObject.Find("PrizeWheel");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Stopper")
        {
            wheel.GetComponent<PrizeWheel>().GetPrize(prize);
        }
    }
}
