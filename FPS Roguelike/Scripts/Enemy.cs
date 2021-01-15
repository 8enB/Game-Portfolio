using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;//the enemies health

    public Rigidbody RB;//controls the enemies movement

    public float speed;//the enemies max speed

    public float stayRange;//range to keep from the player

    public bool careHeight;//if it should fly
    public float heightAbovePlayer;//what height to fly at

    public GameObject gun;//the enemies gun

    public float damage;//the enemies damage

    //efects of firing and the details of what to fire
    public GameObject shootEffect;
    public GameObject hitEffect;
    public GameObject projectileToFire;
    public Transform firePoint;
    public float bulletSpeed;

    private Vector3 shootPoint;//where to fire from

    public float timeBetweenShots;//fire rate

    private float shotTimer;//keeps track of fire rate

    //distance and height from player
    private float distFromPlayer;
    private float heightFromPlayer;

    private Vector3 move;//the move vector of the enemy
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(PlayerControl.instance.transform.position.x, transform.position.y, PlayerControl.instance.transform.position.z));//turn to face the player

        gun.transform.LookAt(new Vector3(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.y + 1f, PlayerControl.instance.transform.position.z));//point gun at the player

        //movement if it doesn't fly
        if (!careHeight)
        {
            distFromPlayer = (new Vector2(transform.position.x, transform.position.z) - new Vector2(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.z)).magnitude;//check the distance from the player

            move = transform.forward;//move the enemy

            //stay at this distance from the player 
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
        else//if it should fly
        {
            distFromPlayer = (transform.position - new Vector3(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.y + heightAbovePlayer, PlayerControl.instance.transform.position.z)).magnitude;//check the distance from the player

            move = (transform.position - new Vector3(PlayerControl.instance.transform.position.x, PlayerControl.instance.transform.position.y + heightAbovePlayer, PlayerControl.instance.transform.position.z)).normalized;//move the enemy

            //stay at this distance from the player
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


        //keep track of the time since last shot
        shotTimer -= Time.deltaTime;
        //if it can shoot then shoot
        if (shotTimer <= 0)
        {
            shootPoint = PlayerControl.instance.transform.position;//sets where to shoot to where the player is

            //target.GetComponent<Health>().hit(damage, hitEffect);
            //Instantiate(shootEffect, firePoint.position, firePoint.rotation);
            GameObject projectile = Instantiate(projectileToFire, firePoint.position, firePoint.rotation);//create the bullet

            //give the bullet it's properties
            projectile.GetComponent<Projectile>().impactEffect = hitEffect;
            projectile.GetComponent<Projectile>().flySpeed = bulletSpeed;
            projectile.GetComponent<Projectile>().aimAt = shootPoint;
            projectile.GetComponent<Projectile>().impactDamage = damage;
            
            shotTimer = timeBetweenShots;//reset the shot time
        }
    }

    //function to take damage, called by the gun script, input is the damage to take
    public void takeDamage(float dmg)
    {
        health -= dmg;
        //if out of health then die
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
}
