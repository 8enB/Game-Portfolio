using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{

    public GameObject owner;
    public bool goToOwner = true;

    public bool justFlew;
    public float floatHeight;

    public GameObject cameraTwo;

    public Vector3 pointBelow;
    public LayerMask hitMask;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, hitMask);
        pointBelow = hit.point;

        owner.transform.rotation = transform.rotation;

        if (cameraTwo.active)
        {
            justFlew = true;
        }
        if(!cameraTwo.active && justFlew)
        {
            goToOwner = false;
            owner.transform.position = new Vector3(owner.transform.position.x, pointBelow.y + floatHeight, owner.transform.position.z);
            transform.position = new Vector3(transform.position.x, pointBelow.y + floatHeight, transform.position.z);
            if (transform.position.y <= pointBelow.y + floatHeight)
            {
                justFlew = false;
                goToOwner = true;
            }
            
        }

        if (goToOwner)
        {
            transform.position = owner.transform.position;
        }
        
        owner.transform.localRotation = transform.rotation;

       
    }
    
}
