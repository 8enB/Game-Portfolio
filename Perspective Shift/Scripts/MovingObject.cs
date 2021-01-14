using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public GameObject cameraTwo;
    public GameObject player;
    public LayerMask hitMask;
    public float floatHeight;
    public float floatSpeed;
    public float floatDownSpeed;
    float floatFactor;

    private Vector3 pointAbove;
    public Vector3 pointBelow;

    private bool justFlew;

    private bool shouldGoUp;
    private bool isBelow;

    private bool up = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, hitMask);

        pointAbove = hit.point;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, hitMask);
        pointBelow = hit.point;

        if (pointAbove.y < cameraTwo.transform.position.y)
        {
            shouldGoUp = false;
        }
        else
        {
            if (transform.position.y > cameraTwo.transform.position.y)
            {
                shouldGoUp = false;
            }
            else
            {
                shouldGoUp = true;
            }
        }

        if(cameraTwo.active && shouldGoUp)
        {
            transform.position = new Vector3(transform.position.x, cameraTwo.transform.position.y - 2, transform.position.z);
            justFlew = true;
        }
        if(!cameraTwo.active && justFlew)
        {
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, hitMask);
            pointBelow = hit.point;

            transform.position = new Vector3(pointBelow.x, pointBelow.y + floatHeight, pointBelow.z);
            justFlew = false;
        }

        if (!cameraTwo.active && transform.position.y <= pointBelow.y + floatHeight + 1f && !up)
        {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * floatSpeed, transform.position.z);
            //floatFactor += floatSpeed * Time.deltaTime;
        }
        if(transform.position.y <= pointBelow.y + floatHeight && up)
        {
            up = false;
            floatFactor = 0f;
        }
        if (transform.position.y >= pointBelow.y + floatHeight + 0.5f && !up)
        {
            up = true;
            //floatFactor = floatSpeed;
            
        }
        if (!cameraTwo.active && transform.position.y >= floatHeight - 1f && up)
        {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * floatSpeed * floatFactor, transform.position.z);
            floatFactor += floatDownSpeed * floatDownSpeed * Time.deltaTime;
        }
    }
}
