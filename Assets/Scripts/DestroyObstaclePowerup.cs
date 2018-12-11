using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyObstaclePowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject helpText;
    private GameObject destroyedObstacle;
    private bool objectDestroyed = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(this.GetComponent<Toggle>().isOn && !objectDestroyed)
        {
            helpText.SetActive(true);
            
            if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Debug.Log("Touched");
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit");
                    Debug.Log(hit.collider.name);
                    if(hit.collider.CompareTag("Enemy"))
                    {
                        Debug.Log("enemy found");
                        destroyedObstacle = hit.collider.gameObject;
                        destroyedObstacle.SetActive(false);
                        objectDestroyed = true;

                    }
                }
                
            }
        }
        if(!this.GetComponent<Toggle>().isOn)
        {
            if(destroyedObstacle != null)
            {
                destroyedObstacle.SetActive(true);
                objectDestroyed = false;
            }
            helpText.SetActive(false);
        }
	}
}
