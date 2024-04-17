using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : SwipeDetector
{
    public LayerMask brickLayer;
    [SerializeField] float moveSpeed = 1f;
    Vector3 direction, rayStart, rayEnd;
    bool isMoving = false;
    [SerializeField]bool updateRay = true;
    Vector3 targetPosition;
    private void Start() 
    {
        direction = GetSwipeDirection(); 
        // Debug.Log(direction);
        brickLayer = LayerMask.GetMask("BrickLayer");
        rayStart = transform.position + direction ; 
        rayEnd = rayStart + Vector3.down * 5f; 
    }
    

    void LateUpdate()
    {
        
        Debug.Log(isMoving);
        direction = GetSwipeDirection(); 
        if (direction != Vector3.zero) 
        {
            RaycastHit hit;
            Debug.DrawLine(rayStart, rayEnd, Color.red, 1f);
            if(!isMoving)
            {
                if(Physics.Raycast(rayStart + direction, Vector3.down, out hit, 5f, brickLayer) && hit.collider.tag == "endPoints")
                {
                    isMoving = true;
                    updateRay = false;
                    // Debug.Log("Found endPoint");
                    targetPosition = hit.collider.transform.position;
                    Debug.Log("Target Endpoint: " + targetPosition);
                }
                
                
            }
            if(isMoving)
                {
                    transform.position = Vector3.MoveTowards(transform.position,targetPosition,moveSpeed * Time.deltaTime);
                    Debug.Log("CurrentPos: " + transform.position);
                    Debug.Log("Moving");
                    if(Vector3.Distance(transform.position,targetPosition) < 0.1f)
                    {
                        isMoving = false;
                        updateRay = true;
                        direction = Vector3.zero;
                        
                    }
                }
                if (updateRay)
                {   
                    rayStart += direction;
                    rayEnd += direction;
                }
                
            

            

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
