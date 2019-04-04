using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicZoom : MonoBehaviour
{
    [SerializeField]
    private GameObject image; // The image to be scaled
    [SerializeField]
    private float zoomSpeed = 0.5f; // The rate of change of the canvas scale factor
    [SerializeField]
    private float minScale = 0.6f; // The minimum scale the image will scale to
    [SerializeField]
    private float maxScale = 2.0f; // The maximum scale the image will scale to
    [SerializeField]
    private Sprite[] comics;

    public int currentComic = 0;

    private ScrollRect scrollRect;

    private Vector2 startPos;
    private Vector2 direction;
    private bool directionChosen;
    private bool resizing = false;

    void Start()
    {
        image.GetComponent<Image>().sprite = comics[currentComic];
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        Debug.Log(Input.touchCount);
        // If there is one touch on the device
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
            if(directionChosen && !resizing)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    scrollRect.vertical = false;
                    // Check if the swipe if left and update comic page
                    if (direction.x < -50)
                    {
                        if (currentComic < comics.Length - 1)
                        {
                            currentComic++;
                            image.transform.localScale = new Vector3(1, 1, 1);
                            image.GetComponent<Image>().sprite = comics[currentComic];
                        }
                    }
                    // Check if the swipe is right and update comic page
                    else if (direction.x > 50)
                    {
                        if (currentComic > 0)
                        {
                            currentComic--;
                            image.transform.localScale = new Vector3(1, 1, 1);
                            image.GetComponent<Image>().sprite = comics[currentComic];
                        }
                    }
                }
            }
            directionChosen = false;
        }
        // If there are two touches on the device
        else if (Input.touchCount == 2)
        {
            resizing = true;
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
            StartCoroutine(PauseSwipe());
        }
        else
        {
            // Restore Vertical Scrolling
            scrollRect.vertical = true;
        }
    }

    IEnumerator PauseSwipe()
    {
        yield return new WaitForSeconds(0.5f);
        resizing = false;
    }
}
