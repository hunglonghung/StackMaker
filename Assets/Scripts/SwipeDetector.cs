using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector3 fingerDownPosition;
    private Vector3 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touched!");
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            Vector3 direction = fingerUpPosition - fingerDownPosition;
            if (IsVerticalSwipe())
            {
                Debug.DrawLine(Camera.main.ScreenToWorldPoint(fingerDownPosition), Camera.main.ScreenToWorldPoint(fingerDownPosition + direction), Color.green, 2.0f);
                Debug.Log("Vertical Swipe Detected! Direction: " + (fingerDownPosition.y - fingerUpPosition.y > 0 ? "down" : "up"));
            }
            else
            {
                Debug.DrawLine(Camera.main.ScreenToWorldPoint(fingerDownPosition), Camera.main.ScreenToWorldPoint(fingerDownPosition + direction), Color.red, 2.0f);
                Debug.Log("Horizontal Swipe Detected! Direction: " + (fingerDownPosition.x - fingerUpPosition.x > 0 ? "left" : "right"));
            }
            fingerUpPosition = fingerDownPosition;
        }
    }


    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }
}
