using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public GameObject cameraTwo;
    public GameObject player;
    public LayerMask hitMask;

    private bool isBelow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (cameraTwo.active && transform.position.y < cameraTwo.transform.position.y && !(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), cameraTwo.transform.position.y)))
        {
                transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }

    }
}
