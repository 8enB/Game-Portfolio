using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public Rigidbody RB;

    public float speed;

    public float stayRange;
    public bool careHeight;
    public float heightAbovePlayer;

    public GameObject gun;

    public float damage;

    public GameObject shootEffect;
    public GameObject hitEffect;
    public GameObject projectileToFire;
    public Transform firePoint;
    public float bulletSpeed;

    private Vector3 shootPoint;

    public float timeBetweenShots;

    private float shotTimer;

    private float distFromPlayer;
    private float heightFromPlayer;

    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(PlayerControl.instance.transform.position.x, transform.position.y, PlayerControl.instance.transform.position.z));

        gun.transform.LookAt(new Vector3(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.y + 1f, PlayerControl.instance.transform.position.z));

        if (!careHeight)
        {
            distFromPlayer = (new Vector2(transform.position.x, transform.position.z) - new Vector2(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.z)).magnitude;

            move = transform.forward;

            if (distFromPlayer < stayRange - 0.05 * stayRange)
            {
                RB.velocity = move * -speed;
            }
            else if (distFromPlayer > stayRange + 0.05 * stayRange)
            {
                RB.velocity = move * speed;
            }
            else
            {
                RB.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            distFromPlayer = (transform.position - new Vector3(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.y + heightAbovePlayer, PlayerControl.instance.transform.position.z)).magnitude;

            move = (transform.position - new Vector3(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.y + heightAbovePlayer, PlayerControl.instance.transform.position.z)).normalized;

            if (distFromPlayer < stayRange - 0.05 * stayRange)
            {
                RB.velocity = move * speed;
            }
            else if (distFromPlayer > stayRange + 0.05 * stayRange)
            {
                RB.velocity = move * -speed;
            }
            else
            {
                RB.velocity = new Vector3(0, 0, 0);
            }
        }



        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0)
        {
            shootPoint = PlayerControl.instance.transform.position;

            //target.GetComponent<Health>().hit(damage, hitEffect);
            //Instantiate(shootEffect, firePoint.position, firePoint.rotation);
            GameObject projectile = Instantiate(projectileToFire, firePoint.position, firePoint.rotation);

            projectile.GetComponent<Projectile>().impactEffect = hitEffect;
            projectile.GetComponent<Projectile>().flySpeed = bulletSpeed;
            projectile.GetComponent<Projectile>().aimAt = shootPoint;
            projectile.GetComponent<Projectile>().impactDamage = damage;
            shotTimer = timeBetweenShots;
        }
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
}
