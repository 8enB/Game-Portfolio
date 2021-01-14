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
        theRB.velocity = transform.right*speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().DamagePlayer(damageGive);
        }
        if(other.tag != "Enemy")
        {
            if(other.tag != "Tile")
        {
            if(other.tag != "PickUp")
            {
                if(other.tag != "Room")
            {
                if(other.tag != "Gen")
            {
                if(other.tag != "PBullet" && other.tag != "RBullet" && other.tag != "SGBullet" && other.tag != "EBullet")
            {
        Destroy(gameObject);
            }
            }
            }
            }
        }
        }

        

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
