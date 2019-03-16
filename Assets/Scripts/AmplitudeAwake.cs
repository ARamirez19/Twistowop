using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAwake : MonoBehaviour
{
    void Awake()
    {
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("73a08b5059cce282750b903b112fadb7");
    }
}
