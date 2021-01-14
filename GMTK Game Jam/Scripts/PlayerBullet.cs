using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 5f;

    public Rigidbody2D theRB;

    public GameObject impactEffect;

    private Vector3 moveDirection;

    public int damageGive;

    public GameObject bulletToFire;

    public bool pierce;
    public bool explode;
    public bool explosion;
    public bool control;
    public bool expireEvent;
    public GameObject[] expire;
    public float lifeTime;
    public float timer;
    public int ammoType;

    public bool expand;
    public float expandSize;
    public float expandTime;
    public bool disable;
    public float disableTime;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.up * speed;
        //move forwards relitive to rotation
        /*if (!control)
        {
            theRB.velocity = transform.up * speed;
        }
        else
        {
            moveDirection = Input.mousePosition - transform.position;
            moveDirection.Normalize();

            theRB.velocity = moveDirection * speed;
        }*/
        /*if (theRB.velocity == new Vector2(0, 0))
        {
            Destroy(gameObject);
        }*/
        if (expand)
        {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, expandSize, Time.deltaTime / expandTime), transform.localScale.y, transform.localScale.z);
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            foreach (GameObject item in expire)
            {
                Instantiate(item, transform.position, transform.rotation);
            }
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (explode)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if ((explosion || pierce) && !disable)
            {
                other.GetComponent<EnemyControl>().DamageEnemy(damageGive);
            }
            else if(!disable)
            {
                other.GetComponent<EnemyControl>().DamageEnemy(damageGive);
                Destroy(gameObject);
            }
            else if(disable && pierce)
            {
                other.GetComponent<EnemyControl>().DisableEnemy(disableTime);
            }
            else if(disable)
            {
                other.GetComponent<EnemyControl>().DisableEnemy(disableTime);
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);

            }
            
        }
        if(other.tag == "Enviroment")
        {
            if (explode)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (explosion || pierce)
            {
            
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnExpire()
    {

    }

}
