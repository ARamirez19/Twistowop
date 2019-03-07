using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool onlyPlayer = false;
    [SerializeField] private GameObject otherPortal;
    private float timer = 0.5f;

    private void Start()
    {
        if(otherPortal.GetComponent<Portal>().GetOnlyPlayerStatus() == true)
        {
            onlyPlayer = true;
        }
        else
        {
            onlyPlayer = false;
        }
    }

    private void Update()
    {
        if(timer < 0.5f)
        {
            timer += Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(onlyPlayer)
        {
            if(other.tag != "Player")
            {
                return;
            }
        }
        if (timer >= 0.5f)
        {
            otherPortal.GetComponent<Portal>().Pause();
            other.transform.position = otherPortal.transform.position;
        }
    }

    public void Pause()
    {
        timer = 0;
    }

    public bool GetOnlyPlayerStatus()
    {
        return onlyPlayer;
    }
}
