using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 4f;
    private Vector3 direction;

    public Rigidbody2D theRB;

    public GameObject impactEffect;


    // Start is called before the first frame update
    void Start()
    {

        direction = PlayerControl.instance.transform.position - transform.position;
        direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = -transform.up * speed;
        if(LevelManager.instance.destroyBullets == true)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().DamagePlayer(1);
            Destroy(gameObject);
        }




    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
