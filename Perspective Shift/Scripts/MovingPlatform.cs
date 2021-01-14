using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject cameraTwo;

    private Vector3 startPos;
    public Vector3 endPos;

    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool forward;
    public bool backwards;

    public float speed;

    public bool isMoving;
    public bool shouldMoveBack;

    private bool gotThere = false;

    private bool touchingPlayer;

    float xPos;
    float yPos;
    float zPos;

    private GameObject target = null;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;
        if (isMoving && !cameraTwo.active)
        {
            if (!gotThere)
            {
                if (up)
                {
                    if (transform.position.y <= endPos.y)
                    {
                        transform.position = new Vector3(transform.position.x, yPos += Time.deltaTime * speed, transform.position.z);
                    }
                    else
                    {
                        gotThere = true;
                    }
                }
                if (down)
                {
                    if (transform.position.y >= endPos.y)
                    {
                        transform.position = new Vector3(transform.position.x, yPos -= Time.deltaTime * speed, transform.position.z);
                    }
                    else
                    {
                        gotThere = true;
                    }
                }
                if (left)
                {
                    
                    if (transform.position.x >= endPos.x)
                    {
                        transform.position = new Vector3(xPos -= Time.deltaTime * speed, transform.position.y, transform.position.z);
                        if(touchingPlayer)
                        {
                            Vector3 move = (-transform.right * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = true;
                    }
                }
                if (right)
                {
                    
                    if (transform.position.x <= endPos.x)
                    {
                        transform.position = new Vector3(xPos += Time.deltaTime * speed, transform.position.y, transform.position.z);
                        if (touchingPlayer)
                        {
                            Vector3 move = (transform.right * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    
                    else
                    {
                        gotThere = true;
                    }
                }
                if (forward)
                {
                    
                    if (transform.position.z <= endPos.z)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, zPos += Time.deltaTime * speed);
                        if (touchingPlayer)
                        {
                            Vector3 move = (transform.forward * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = true;
                    }
                }
                if (backwards)
                {
                    
                    if (transform.position.z >= endPos.z)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, zPos -= Time.deltaTime * speed);
                        if (touchingPlayer)
                        {
                            Vector3 move = (-transform.forward * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = true;
                    }
                }
            }
            
            if(shouldMoveBack && gotThere)
            {
                if (down)
                {
                    if (transform.position.y <= startPos.y)
                    {
                        transform.position = new Vector3(transform.position.x, yPos += Time.deltaTime * speed, transform.position.z);
                    }
                    else
                    {
                        gotThere = false;
                    }
                }
                if (up)
                {
                    if (transform.position.y >= startPos.y)
                    {
                        transform.position = new Vector3(transform.position.x, yPos -= Time.deltaTime * speed, transform.position.z);
                    }
                    else
                    {
                        gotThere = false;
                    }
                }
                if (right)
                {

                    if (transform.position.x >= startPos.x)
                    {
                        transform.position = new Vector3(xPos -= Time.deltaTime * speed, transform.position.y, transform.position.z);
                        if (touchingPlayer)
                        {
                            Vector3 move = (-transform.right * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = false;
                    }
                }
                if (left)
                {

                    if (transform.position.x <= startPos.x)
                    {
                        transform.position = new Vector3(xPos += Time.deltaTime * speed, transform.position.y, transform.position.z);
                        if (touchingPlayer)
                        {
                            Vector3 move = (transform.right * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = false;
                    }
                }
                if (backwards)
                {

                    if (transform.position.z <= startPos.z)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, zPos += Time.deltaTime * speed);
                        if (touchingPlayer)
                        {
                            Vector3 move = (transform.forward * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = false;
                    }
                }
                if (forward)
                {

                    if (transform.position.z >= startPos.z)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, zPos -= Time.deltaTime * speed);
                        if (touchingPlayer)
                        {
                            Vector3 move = (-transform.forward * Time.deltaTime * speed);
                            controller.Move(move);
                        }
                    }
                    else
                    {
                        gotThere = false;
                    }
                }
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            touchingPlayer = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        target = null;
        if(col.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
}
