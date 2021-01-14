using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactEffect;
    public GameObject aimAt;
    public float flySpeed;

    public float impactDamage;

    public string mySide;

    public Rigidbody theRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(aimAt.transform);
        theRB.velocity = transform.forward * flySpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag != mySide)
        {
            aimAt.GetComponent<Health>().hit(impactDamage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
