using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    /*public GameObject handThree;
    public GameObject handTwo;*/
    public GameObject camera2;
    public bool canPickUp = true;

    public GameObject body;

    private bool pickedUp = false;

    private bool touching = false;

    private GameObject pickUp = null;

    public string activeTag;
    public string groundTag;

    private bool justSwapped = false;

    private bool twoD = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!camera2.active && twoD)
        {
            justSwapped = true;
        }
        if (camera2.active && !twoD)
        {
            justSwapped = true;
        }
        float dist = Vector3.Distance(pickUp.GetComponent<Pickupable>().owner.transform.position, transform.position);
        if (pickedUp)
        {
            pickUp.transform.position = transform.position;
            pickUp.transform.rotation = body.transform.rotation;
            /*pickUp.transform.SetParent(gameObject.transform);
            pickUp.transform.position = transform.position;*/
            
        }
        else
        {
           // pickUp.transform.SetParent(null);
        }
        if (Input.GetButtonDown("Fire2") )
        {
            pickedUp = false;
        }
        if (dist > 5)
        {
            pickedUp = false;
        }
        if(justSwapped)
        {
            pickedUp = false;
        }
        if (Input.GetButtonDown("Fire1") && touching && canPickUp)
        {
            pickedUp = true;
        }
        if(camera2.active)
        {
            twoD = true;
        }
        if (!camera2.active)
        {
            twoD = false;
        }
        justSwapped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == activeTag && !pickedUp)
        {
            touching = true;
            pickUp = other.gameObject;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == activeTag && !pickedUp)
        {
            touching = false;
            pickUp = null;
        }
    }
}
