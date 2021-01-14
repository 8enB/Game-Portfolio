using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    public float rangeToStopChasing;
    public float fireRange;
    private Vector3 moveDirection;

    public GameObject deathEffect;

    public int Ehealth = 100;

    public bool shouldShoot;

    public GameObject bullet;
    public Transform gun;
    public GameObject[] firePositions;
    public GameObject[] firePoints;
    public float fireDelay;
    private float fireDelayTemp;
    public float fireRate;
    private float fireTimer;
    private int cycle = 0;

    public int numberOfItemsDroppedP = 1;
    private int itemsDroppedP;
    public bool shouldDropItemP;
    public GameObject[] itemsToDropP;
    public float itemDropRateP;

    
    private bool fire;
    private int count = 0;

    public int value;

    public bool disabled;

    private float disableTime;

    private int spinDirection;

    public float spinSpeed = 1;

    public GameObject hurtEffect;


    // private bool active = true;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        fireTimer = fireRate;
    }

    // Update is called once per frame

    void Update()
    {
        itemsDroppedP = numberOfItemsDroppedP;
        
        if (1 == 1)
        {
            if (!disabled)
            {
                if (Vector3.Distance(transform.position, PlayerControl.instance.transform.position) <= rangeToChasePlayer && Vector3.Distance(transform.position, PlayerControl.instance.transform.position) > rangeToStopChasing)
                {
                    moveDirection = PlayerControl.instance.transform.position - transform.position;
                }
                else
                {
                    moveDirection = Vector3.zero;
                }

                moveDirection.Normalize();

                theRB.velocity = moveDirection * moveSpeed;

                if (shouldShoot)
                {
                    Vector2 offset = new Vector2(PlayerControl.instance.transform.position.x - transform.position.x, PlayerControl.instance.transform.position.y - transform.position.y);
                    float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;


                    gun.rotation = Quaternion.Euler(0, 0, angle - 90);
                    if (PlayerControl.instance.bombTime > 0)
                    {
                        Instantiate(deathEffect, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }


                    if (Vector3.Distance(transform.position, PlayerControl.instance.transform.position) <= fireRange)
                    {
                        fireTimer -= Time.deltaTime;
                        fireDelayTemp -= Time.deltaTime;

                        if (PlayerControl.instance.bombTime > 0)
                        {
                            Instantiate(deathEffect, transform.position, transform.rotation);
                            Destroy(gameObject);
                        }

                        if (fireTimer <= 0)
                        {
                            fire = true;
                            if (count >= firePoints.Length)
                            {
                                count = 0;
                                fire = false;
                            }
                        }
                        if (fireDelayTemp <= 0 && fire == true)
                        {
                            Instantiate(bullet, firePoints[count].transform.position, firePoints[count].transform.rotation);
                            fireDelayTemp = fireDelay;
                            count++;
                            fireTimer = fireRate;
                        }
                    }
                }
            }
            else
            {

                disableTime -= Time.deltaTime;
                if (disableTime <= 0)
                {
                    //GameObject.Animation.enabled = true;
                    disabled = false;
                }
            }
            if (Ehealth <= 0)
            {

                if (shouldDropItemP)
                {
                    while (itemsDroppedP > 0)
                    {
                        float dropChance = Random.Range(0, 100f);
                        if (dropChance <= itemDropRateP)
                        {
                            int randomItem = Random.Range(0, itemsToDropP.Length);
                            Instantiate(itemsToDropP[randomItem], transform.position, transform.rotation);
                        }
                        itemsDroppedP--;
                    }
                }
                PlayerControl.instance.GainPoints(value);
                Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                //PlayerControl.instance.health ++;
            }
            if(PlayerControl.instance.bombTime > 0)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void DamageEnemy(int damage)
    {
        Ehealth -= damage;
        Instantiate(hurtEffect, transform.position, transform.rotation);
        shouldDropItemP = true;
            

    }
    public void DisableEnemy(float DTime)
    {
        Instantiate(hurtEffect, transform.position, transform.rotation);
        disabled = true;
        disableTime = DTime;
        // theRB.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        theRB.velocity = new Vector2(0, 0);
        if(theRB.velocity.x < 0)
        {
            spinDirection = 1;
        }
        else
        {
            spinDirection = -1;
        }
        //GameObject.Animation.enabled = false;

    }

}
