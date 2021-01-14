using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 5f;

    public Rigidbody2D theRB;

    public GameObject impactEffect;

    public int damageGive;

    public GameObject bulletToFire;

    public bool pierce;

    public bool control;
    public bool expireEvent;
    public GameObject[] expire;
    public float lifeTime;
    public float timer;
    public int ammoType;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        //move forwards relitive to rotation
        theRB.velocity = transform.right*speed;
        if(theRB.velocity == new Vector2(0,0))
        {
            Destroy(gameObject);
        }
        if(control)
        {
        transform.rotation = PlayerControl.instance.gun.rotation;
        }
        lifeTime -= Time.deltaTime;
        if(lifeTime<=0)
        {
            foreach (GameObject item in expire)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    
            if(pierce && other.tag =="Crate")
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
            else
            {
        if(other.tag != "Player")
        {
            if(other.tag != "Tile")
        {
            if(other.tag != "PickUp")
            {
                if(other.tag != "Room")
            {
                if(other.tag != "Gen")
            {
                if(other.tag != "PBullet" && other.tag != "RBullet" && other.tag != "SGBullet")
            {
                if(other.tag != "EBullet")
                {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
                }
            }
            }
            }
        }
        }
        }
        }
        
        
        // if(other.tag=="EBullet" && pierce)
        // {
        //     Instantiate(bulletToFire, transform.position, transform.rotation);
        // }

        if(other.tag == "Enemy")
        {
        other.GetComponent<EnemyTableControl>().DamageEnemy(damageGive, ammoType);
        }
    }

    private void OnExpire()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
