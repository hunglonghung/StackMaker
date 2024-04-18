using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementFixed : SwipeDetector
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
        rayStart = transform.position + direction + Vector3.up * 2f; 
        rayEnd = rayStart + Vector3.down * 5f; 
    }
    

    void LateUpdate()
    {
        // Debug.Log("RayStart position: " + rayStart);
        // Debug.Log(isMoving);
        Debug.DrawRay(rayStart + Vector3.up, Vector3.down * 5f, Color.green, 3f);
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
                    Debug.Log("Found endPoint");
                    targetPosition = hit.collider.transform.position + Vector3.up * 0.5f;
                    rayStart = targetPosition + Vector3.up * 2f; // Cập nhật lần cuối rayStart tới vị trí của endPoint
                    rayEnd =  rayStart + Vector3.down * 5f; 
                    // Debug.Log("Target Endpoint: " + targetPosition);
                }
                else if (Physics.Raycast(rayStart + direction, Vector3.down, out hit, 5f, brickLayer) && hit.collider.tag == "Wall")
                {
                    // Debug.Log("Touched wall");
                    rayStart = targetPosition + Vector3.up * 2f; // Cập nhật lần cuối rayStart tới vị trí của endPoint
                    rayEnd =  rayStart + Vector3.down * 5f; 
                }
            }
        }

            if(isMoving)
            {
                rotatePlayer();
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetPosition.x, moveSpeed * Time.deltaTime), 
                                            transform.position.y, // Giữ nguyên giá trị y
                                            Mathf.MoveTowards(transform.position.z, targetPosition.z, moveSpeed * Time.deltaTime));
                
                // Debug.Log("CurrentPos: " + transform.position);
                // Debug.Log("Moving");
                if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f && Mathf.Abs(transform.position.z - targetPosition.z) < 0.1f)
                {
                    isMoving = false;
                    updateRay = true; // Bật lại cập nhật rayStart và rayEnd khi đã di chuyển đến targetPosition
                    direction = Vector3.zero;
                }
            }
    }   
    void rotatePlayer()
{
    float rotationY = 0f;
    if (direction == Vector3.left)
    {
        rotationY = 90f;
    }
    else if (direction == Vector3.right)
    {
        rotationY = -90f;
    }
    else if (direction == Vector3.forward)
    {
        rotationY = 180f;
    }
    else if (direction == Vector3.back)
    {
        rotationY = 0f;
    }
    
    // Tạo một Quaternion dựa trên góc quay trên trục y
    Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
    transform.rotation = rotation;
}

                
            

        

    
}
