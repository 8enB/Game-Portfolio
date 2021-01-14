using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melle : MonoBehaviour
{

    public GameObject owner;

    public float damage;

    public string mySide;

    public GameObject target;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && owner.GetComponent<Unit>().selected == true)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && (hitInfo.transform.tag == "USA Unit"))
            {
                target = hitInfo.transform.gameObject;
                owner.GetComponent<Move>().targetPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                owner.GetComponent<Move>().moveToHit = true;
            }
            else
            {
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject == target)
        {
            owner.GetComponent<Move>().moveToHit = false;
        }
            if (other.transform.tag != mySide)
            {
                other.transform.GetComponent<Health>().hit(damage);
                //Instantiate(hitEffect, transform.position, transform.rotation);
            }
    }
}
