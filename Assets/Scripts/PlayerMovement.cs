using UnityEngine;

public class PlayerMovement : SwipeDetector
{
    public LayerMask whiteTileLayer = LayerMask.NameToLayer("Wall"); 

    void Update()
    {
        
        Vector3 direction = GetSwipeDirection(); 

        if (direction != Vector3.zero) 
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, whiteTileLayer);
            Debug.DrawLine(transform.position, hit.point, Color.green, 2.0f);
            if(hit.collider != null)
            {
                
            }
        }
    }   

    Vector2 GetSwipeDirection()
    {
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
