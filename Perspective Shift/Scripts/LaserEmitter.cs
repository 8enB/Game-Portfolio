using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{

    public bool shouldFire = true;
    public GameObject emitterPoint;
    public GameObject laserEnd;
    private LineRenderer lr;
    public LayerMask laser;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (emitterPoint.active)
        {
            lr.enabled = true;
            if (shouldFire)
            {
                lr.SetPosition(0, emitterPoint.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(emitterPoint.transform.position, transform.forward, out hit))
                {
                    if (hit.collider != laserEnd)
                    {
                        lr.SetPosition(1, hit.point);
                        laserEnd.transform.position = hit.point;
                    }
                }
                else lr.SetPosition(1, transform.forward * 5000);
            }
        }
        else
        {
            
            lr.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            emitterPoint.SetActive(true);
            shouldFire = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Laser")
        {
            emitterPoint.SetActive(false);
            shouldFire = false;
        }
    }
}
