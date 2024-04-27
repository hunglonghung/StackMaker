using System;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class PlayerMovementFixed : SwipeDetector
{
    public LayerMask brickLayer;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Vector3 rayStart, rayEnd;
    [SerializeField] bool isMoving = false;
    [SerializeField] bool updateRay = true;
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
        if ( updateRay) 
        {
            
            RaycastHit hit;
            
            rayStart += direction; // chỉ cập nhật khi updateRay là true
            rayEnd += direction; // chỉ cập nhật khi updateRay là true
            Debug.DrawRay(rayStart + Vector3.up, Vector3.down * 5f, Color.red, 3f);
            //  Debug.DrawRay(rayStart + Vector3.up + direction, Vector3.down * 5f, Color.red, 3f);
            // Kiểm tra xem nhân vật có đang di chuyển không
            if (!isMoving)
            {
                RaycastHit hitFirst;
                RaycastHit hitSecond;
                if (Physics.Raycast(rayStart + direction, Vector3.down, out hitFirst, 5f, brickLayer))
                {
                    Debug.Log("direction " + hitFirst.collider.tag);
                }
                if (Physics.Raycast(rayStart , Vector3.down, out hitFirst, 5f, brickLayer))
                {
                    Debug.Log("no direction" + hitFirst.collider.tag);
                }
                // Bắn tia đầu tiên kiểm tra xem có chạm vào "endPoints" hoặc "FinishBox" không
                if (Physics.Raycast(rayStart , Vector3.down, out hitFirst, 5f, brickLayer) && 
                    (hitFirst.collider.tag == "endPoints" || hitFirst.collider.tag == "FinishBox" ))
                {
                    //  Debug.Log("Hit endpoint");
                    if(hitFirst.collider.tag == "FinishBox")
                    {
                        setTargetPosition(hitFirst);
                    }
                    // Bắn tia thứ hai từ điểm tiếp theo để kiểm tra xem có phải là "Wall" không
                    if (Physics.Raycast(rayStart + direction , Vector3.down, out hitSecond, 5f, brickLayer) && 
                        hitSecond.collider.tag == "Wall")
                    {
                        setTargetPosition(hitFirst);
                        
                    }
                    
                } 
                else if (Physics.Raycast(rayStart , Vector3.down, out hitFirst, 5f, brickLayer) && (hitFirst.collider.tag == "Wall"))
                {
                    Debug.Log("Hitwall!");
                    rayStart = targetPosition + Vector3.up * 2f; // Cập nhật lần cuối rayStart tới vị trí của endPoint
                    rayEnd = rayStart + Vector3.down * 5f; 
                }
                
            }
            // Debug.DrawLine(rayStart, rayEnd, Color.red, 1f);

        }

            if(isMoving)
            {
                if(BrickControl.brickCount > 0)
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
                else
                {
                    isMoving = false;
                    updateRay = true; // Bật lại cập nhật rayStart và rayEnd khi đã di chuyển đến targetPosition
                    direction = Vector3.zero;
                    rayStart = gameObject.transform.position + Vector3.up * 2f; 
                    rayEnd = rayStart + Vector3.down * 5f; 
                    BrickControl.brickCount = 1;
                    
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
    void setTargetPosition(RaycastHit hit)
    {
        // Debug.Log("Touched Wall");
        isMoving = true;
        updateRay = false; // Tắt cập nhật rayStart và rayEnd khi đã tìm thấy endPoint
        // Debug.Log("Found endPoint");
        targetPosition = hit.collider.transform.position + Vector3.up * 0.5f;
        rayStart = targetPosition + Vector3.up * 2f; // Cập nhật lần cuối rayStart tới vị trí của endPoint
        rayEnd = rayStart + Vector3.down * 5f; 
        // Debug.Log("Target Endpoint: " + targetPosition);
    }
    

                
            

        

    
}
