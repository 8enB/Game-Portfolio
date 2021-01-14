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

   // public bool control;
    public bool expireEvent;
    public GameObject[] expire;
    //public float lifeTime;
    public float timer;
    // public int ammoType;
    public AudioSource hurtSound;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        //move forwards relitive to rotation
        theRB.velocity = transform.up * speed;
        if (theRB.velocity == new Vector2(0, 0))
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(0.75f + PlayerControl.instance.health * PlayerControl.instance.sizeScale, 0.75f + PlayerControl.instance.health * PlayerControl.instance.sizeScale, 1);
        /*        if (control)
                {
                    transform.rotation = PlayerControl.instance.gun.rotation;
                }*/
        /*lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            foreach (GameObject item in expire)
            {
                Instantiate(item, transform.position, transform.rotation);
            }
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }*/

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            hurtSound.Play();
            other.GetComponent<EnemyControl>().DamageEnemy(damageGive);

            Destroy(gameObject);
        }
/*             if (other.tag == "Enemy")
              {
                //Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
              }*/


        // if(other.tag=="EBullet" && pierce)
        // {
        //     Instantiate(bulletToFire, transform.position, transform.rotation);
        // }


    }

    private void OnExpire()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
