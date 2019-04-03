using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicZoom : MonoBehaviour
{
    [SerializeField]
    private GameObject image; // The canvas
    [SerializeField]
    private float zoomSpeed = 0.5f; // The rate of change of the canvas scale factor
    [SerializeField]
    private float minScale = 0.6f; // The minimum scale the image will scale to
    [SerializeField]
    private float maxScale = 2.0f; // The maximum scale the image will scale to

    private ScrollRect scrollRect;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            //Turn off vertical scrolling of the scroll rect
            scrollRect.vertical = false;

            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the image size based on the change in distance between the touches.
            image.transform.localScale += new Vector3(-deltaMagnitudeDiff * zoomSpeed, -deltaMagnitudeDiff * zoomSpeed);

            // Clamp the image to the maximum size
            image.transform.localScale = new Vector3(Mathf.Clamp(image.transform.localScale.x, minScale, maxScale), Mathf.Clamp(image.transform.localScale.y, minScale, maxScale));
        }
        else
        {
            // Restore Vertical Scrolling
            scrollRect.vertical = true;
        }
    }
}
