using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerController : MonoBehaviour
{
    public void OnChangeJetpack(bool s)
    {
        FindObjectOfType<PlayerController>().UseJetpack(s);
     
    }
    public void OnChangeCooldown(bool s)
    {
        FindObjectOfType<PlayerController>().UseCooldown(s);
    }

    public void OnChangeRotate(bool s)
    {
        FindObjectOfType<PlayerController>().RotatePlayer(s);

    }
}
