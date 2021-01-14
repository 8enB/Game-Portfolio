using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform Parent; // Whatever you want the camera locked to
    public Transform Obj; // The object to place the camera on
                          //public float Radius = 4.5f;
    float Dist;
    Vector3 MousePos1, MousePos2, ScreenMouse, MouseOffset;
    public void Update()
    {
        if (!Manager.instance.isPaused)
        {
            MousePos1 = Input.mousePosition;
            // the below line assumes this script is attached to a camera object
            ScreenMouse = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(MousePos1.x, MousePos1.y, Obj.position.z - GetComponent<Camera>().transform.position.z));
            MouseOffset = ScreenMouse - Parent.position;
            MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -transform.position.z));
            //Obj.position.y = ((MousePos2.y - Parent.position.y) / 2.0) + Parent.position.y;
            //Obj.position.x = ((MousePos2.x - Parent.position.x) / 2.0) + Parent.position.x;
            Obj.position = new Vector3((MousePos2.x + Parent.position.x * 4) / 5.0f, (MousePos2.y + Parent.position.y * 4) / 5.0f, Obj.position.z);

            Dist = Vector2.Distance(new Vector2(Obj.position.x, Obj.position.y), new Vector2(Parent.position.x, Parent.position.y));

            //if (Dist > Radius)
            //{
            //var norm = MouseOffset.normalized;
            //Obj.position.x = norm.x * Radius + Parent.position.x;
            //Obj.position.y = norm.y * Radius + Parent.position.y;
            // Obj.position = new Vector3(norm.x * Radius + Parent.position.x, norm.y * Radius + Parent.position.y, Obj.position.z);
            //}
        }
    }
}
