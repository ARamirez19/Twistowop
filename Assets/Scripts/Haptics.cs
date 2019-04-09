using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Haptics : MonoBehaviour
{
    public static void HapticSelection()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
    }
    public static void HapticSuccess()
    {
        MMVibrationManager.Haptic(HapticTypes.Success);
    }
    public static void HapticWarning()
    {
        MMVibrationManager.Haptic(HapticTypes.Warning);
    }
    public static void HapticFailure()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure);
    }
    public static void HapticLight()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
    }
    public static void HapticMedium()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }
    public static void HapticHeavy()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }
}