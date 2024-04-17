using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : SwipeDetector
{
    public LayerMask brickLayer;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Vector3 rayStart, rayEnd;
    [SerializeField] bool isMoving = false;
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
    Debug.Log("RayStart position: " + rayStart);
    Debug.Log(isMoving);
    if (direction != Vector3.zero && updateRay) 
    {
        
        RaycastHit hit;
        Debug.DrawLine(rayStart, rayEnd, Color.red, 1f);
        rayStart += direction; // chỉ cập nhật khi updateRay là true
        rayEnd += direction; // chỉ cập nhật khi updateRay là true

        if (!isMoving)
        {
            if (Physics.Raycast(rayStart + direction, Vector3.down, out hit, 5f, brickLayer) && hit.collider.tag == "endPoints")
            {
                isMoving = true;
                updateRay = false; // Tắt cập nhật rayStart và rayEnd khi đã tìm thấy endPoint
                // Debug.Log("Found endPoint");
                targetPosition = hit.collider.transform.position;
                rayStart = targetPosition; // Cập nhật lần cuối rayStart tới vị trí của endPoint
                Debug.Log("Target Endpoint: " + targetPosition);
            }
        }
    }

        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("CurrentPos: " + transform.position);
            Debug.Log("Moving");
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                updateRay = true; // Bật lại cập nhật rayStart và rayEnd khi đã di chuyển đến targetPosition
                direction = Vector3.zero;
            }
        }
}   
                
            

        

    
}
