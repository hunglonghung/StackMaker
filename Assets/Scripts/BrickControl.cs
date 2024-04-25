using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour
{
    [SerializeField] List<GameObject> playerBricks = new List<GameObject>();
    [SerializeField] GameObject instantiateObject;
    [SerializeField] public static int brickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.tag);
        if(other.tag == "Brick")
        {
            Debug.Log("FoundBrick");
            addBrick();
            other.gameObject.SetActive(false);
        }
        if(other.tag == "UnBrick")
        {
            Debug.Log("Collide Unbrick");
            removeBrick();
            if(brickCount >= 1)
            {
                GameObject bridgeBrick = Instantiate(instantiateObject,other.gameObject.transform.position,Quaternion.identity,other.gameObject.transform);
                BoxCollider collider = other.gameObject.GetComponent<BoxCollider>();
                if (collider != null)
                {
                    collider.enabled = false; // Vô hiệu hóa collider
                }
                Quaternion rotation = Quaternion.Euler(-90, 0, 180);
                bridgeBrick.transform.rotation = rotation;
                bridgeBrick.gameObject.GetComponent<BoxCollider>().enabled = false;

            }
        }
        if(other.tag == "FinishBox")
        {
            clearBrick();
        }
    }

    private void clearBrick()
    {
        Debug.Log("Called");
        while(playerBricks.Count > 0)
        {
            gameObject.transform.Translate(Vector3.down * 0.3f);
            brickCount --;
            // Debug.Log(playerBricks[playerBricks.Count - 1].transform.position);
            // Debug.DrawLine(playerBricks[playerBricks.Count - 1].transform.position, playerBricks[playerBricks.Count - 1].transform.position + Vector3.left * 10f,  Color.white, 1f);
            GameObject lastBrick = playerBricks[playerBricks.Count - 1]; 
            // Destroy(lastBrick);
            lastBrick.SetActive(false);
            playerBricks.RemoveAt(playerBricks.Count - 1);
            
            
        }
        gameObject.transform.Translate(Vector3.down * 0.6f);
        brickCount --;
    }

    private void removeBrick()
    {
        if (playerBricks.Count > 0)
        {
            gameObject.transform.Translate(Vector3.down * 0.3f);
            brickCount --;
            // GameObject bridgeBrick = Instantiate(instantiateObject, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            // Quaternion rotation = Quaternion.Euler(-90, 0, 180);
            // bridgeBrick.transform.rotation = rotation;
            // bridgeBrick.transform.position = new Vector3(bridgeBrick.transform.position.x, bridgeBrick.transform.position.y - 0.3f * brickCount, bridgeBrick.transform.position.z);
            // Debug.Log(playerBricks[playerBricks.Count - 1].transform.position);
            Debug.DrawLine(playerBricks[playerBricks.Count - 1].transform.position, playerBricks[playerBricks.Count - 1].transform.position + Vector3.left * 10f,  Color.white, 1f);
            GameObject lastBrick = playerBricks[playerBricks.Count - 1]; 
            Destroy(lastBrick); 
            playerBricks.RemoveAt(playerBricks.Count - 1);
        }
        
    }


    private void addBrick()
    {
        gameObject.transform.Translate(Vector3.up * 0.3f);
        brickCount ++;
        GameObject newBrick = Instantiate(instantiateObject, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        Quaternion rotation = Quaternion.Euler(-90, 0, 180);
        newBrick.transform.rotation = rotation;
        newBrick.transform.position = new Vector3(newBrick.transform.position.x, newBrick.transform.position.y - 0.3f * brickCount, newBrick.transform.position.z);
        playerBricks.Add(newBrick);
    }
    
}
