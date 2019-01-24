using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Feelers : MonoBehaviour
{
    [SerializeField] private TriggerType type = TriggerType.None;
    [SerializeField] private UnityEvent callback;

    private enum TriggerType { None, Enter, Exit, Stay };

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (type != TriggerType.Enter)
        {
            return;
        }

        if (other.gameObject.layer == 8)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (type != TriggerType.Exit)
        {
            return;
        }

        if (other.gameObject.layer == 8)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }
}
