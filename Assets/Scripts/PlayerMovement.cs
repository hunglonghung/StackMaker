using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : SwipeDetector
{
    public LayerMask brickLayer;
    Vector3 direction, rayStart, rayEnd;
    private void Start() 
    {
        direction = GetSwipeDirection(); 
        // Debug.Log(direction);
        brickLayer = LayerMask.GetMask("BrickLayer");
        rayStart = transform.position + direction; 
        rayEnd = rayStart + Vector3.down * 5f; 
    }
    

    void LateUpdate()
    {
        direction = GetSwipeDirection(); 
        if (direction != Vector3.zero) 
        {
            RaycastHit hit;
            
            // Debug.Log("Raystart: " + rayStart);
            // Debug.Log("Rayend: " + rayEnd);

            Debug.DrawLine(rayStart, rayEnd, Color.red, 1f);

            if(Physics.Raycast(rayStart, Vector3.down, out hit, 5f, brickLayer))
            {
                Debug.Log("Hit");
                Debug.Log(hit.collider.tag); 
                if(hit.collider.tag == "endPoints")
                {
                    Debug.Log("Moving");
                    transform.position = Vector3.MoveTowards(transform.position,hit.collider.transform.position,1f);
                    Debug.Log("Moved");
                }
            }
            rayStart += direction;
            rayEnd += direction;

        }
    }   

    Vector3 GetSwipeDirection()
    {
        // Debug.Log("Current Swipe Direction: " + swipeDirection.ToString());
        switch (swipeDirection)
        {
            
            case SwipeDirection.Left:
                return Vector3.left ;
            case SwipeDirection.Right:
                return Vector3.right ;
            case SwipeDirection.Up:
                return Vector3.forward ;
            case SwipeDirection.Down:
                return Vector3.back ;
            default:
                return Vector3.zero;
        }
    }
}
