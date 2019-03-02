using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Use360Toggle : MonoBehaviour
{
    public void OnChange(bool s)
    {
        FindObjectOfType<GravityManager>().Use360(s);
    }
}
