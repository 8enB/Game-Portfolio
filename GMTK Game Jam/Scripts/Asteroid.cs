using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float speed = 4f;
    private Vector3 direction;

    public Rigidbody2D theRB;

    public int damageGive;

    public float scaler;

    public int health;

    public int value;

    public GameObject deathEffect;
    public GameObject bombEffect;

    // Start is called before the first frame update
    void Start()
    {

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        speed = Random.Range(0f, 4f);

        scaler = Random.Range(0.5f, 2f);

        transform.localScale = new Vector3(scaler, scaler, 1);

        if(scaler<2f)
        {
            damageGive = 3;
            health = 1;
            value = 75;
        }
        if(scaler<1.5f)
        {
            damageGive = 2;
            health = 1;
            value = 50;
        }
        if(scaler<0.75f)
        {
            damageGive = 1;
            health = 1;
            value = 25;
        }

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.up * speed;
        if(health<=0)
        {

           Instantiate(deathEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        if (PlayerControl.instance.bombTime > 0)
        {
            Instantiate(bombEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().DamagePlayer(1);
            Instantiate(bombEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.tag == "Enviroment")
        {
            health = 0;
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyControl>().DamageEnemy(damageGive);
            Instantiate(bombEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(other.tag == "Bullet")
        {
            health--;
            if(health<=0)
            {
                Instantiate(bombEffect, transform.position, transform.rotation);
                PlayerControl.instance.GainPoints(value);
                Destroy(gameObject);
            }
        }
    }


}
