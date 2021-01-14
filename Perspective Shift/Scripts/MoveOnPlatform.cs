using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPlatform : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    public CharacterController controller;
    void Start()
    {
        target = null;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
        }
    }
    void OnTriggerExit(Collider col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            if (target.tag == "Player")
            {
                controller.Move(offset*0.01f);
            }
        }
        
    }
}
