using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool enemy;
    public GameObject impactEffect;
    public Vector3 aimAt;
    public float flySpeed;

    public float impactDamage;

    public Rigidbody theRB;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(aimAt);
    }

    // Update is called once per frame
    void Update()
    {
        
        theRB.velocity = transform.forward * flySpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Mine")
        {
            other.transform.GetComponent<Mine>().explode();
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (!enemy)
        {
            if (other.transform.tag == "Enemy")
            {
                other.transform.parent.GetComponent<Enemy>().takeDamage(impactDamage);
                //Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (other.transform.tag == "EnemyCrit")
            {
                other.transform.parent.GetComponent<Enemy>().takeDamage(impactDamage * 2f);
                //Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (other.transform.tag != "Player")
            {
                //Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.transform.tag == "Player")
            {
                other.transform.GetComponent<PlayerControl>().hit(impactDamage);
                //Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (other.transform.tag != "Enemy" && other.transform.tag != "EnemyCrit")
            {
                Destroy(gameObject);
            }
        }
    }
}
