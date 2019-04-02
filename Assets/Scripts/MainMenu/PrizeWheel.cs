using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeWheel : MonoBehaviour
{
    [SerializeField]
    private GameObject wheel;
    [SerializeField]
    private GameObject screen;
    [SerializeField]
    private float torque;
    private float spinSpeed;
    [SerializeField]
    private int minSpinSpeed = 1;
    [SerializeField]
    private int maxSpinSpeed = 2;
    private bool hasSpun = false;
    [SerializeField]
    private string currentPrize;
    [SerializeField]
    private string prizeWon;



    // Start is called before the first frame update
    void Start()
    {
        wheel = GameObject.Find("ColorWheel");
        screen = GameObject.Find("HideBackground");
        
    }

    // Update is called once per frame
    void Update()
    {
        spinSpeed = wheel.GetComponent<Rigidbody2D>().angularVelocity;
        if(spinSpeed < 0.5f && hasSpun == true)
        {
            prizeWon = currentPrize;
        }
    }

    public void SpinWheel()
    {
        if (!hasSpun)
        {
            torque = Random.Range(minSpinSpeed, maxSpinSpeed);
            wheel.GetComponent<Rigidbody2D>().AddTorque(this.transform.up.magnitude * torque * 100, ForceMode2D.Impulse);
        }
        hasSpun = true;
    }

    public void ShowWheel()
    {
        this.gameObject.SetActive(true);
        screen.SetActive(true);
    }

    public void GetPrize(string prize)
    {
        currentPrize = prize;
    }

}
