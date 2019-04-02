using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantScripts : MonoBehaviour
{
    private void Start()
    {
#if UNITY_ANDROID
        IronSource.Agent.init("8cfc184d", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
#elif UNITY_IOS
        IronSource.Agent.init("8cfc51d5", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
#endif
        //IronSource.Agent.validateIntegration();
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
