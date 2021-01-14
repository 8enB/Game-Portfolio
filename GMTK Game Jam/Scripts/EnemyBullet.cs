using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 4f;
    private Vector3 direction;

    public Rigidbody2D theRB;

    public GameObject impactEffect;

    public int damageGive;

    // Start is called before the first frame update
    void Start()
    {

        direction = PlayerControl.instance.transform.position - transform.position;
        direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.up * speed;
        if (PlayerControl.instance.bombTime > 0)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().DamagePlayer(damageGive);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.tag == "Enviroment")
        {
            Destroy(gameObject);
        }
    }
    

}
