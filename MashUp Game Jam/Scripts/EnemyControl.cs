using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{



    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    public float fireRange;
    private Vector3 moveDirection;

    public GameObject deathEffect;

    public int Ehealth = 100;
    public float sizeScale;

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
    public bool shouldDropItemP = true;
    public GameObject[] itemsToDropP;
    public float itemDropRateP;
    private bool fire;
    private int count = 0;

    public GameObject hurtEffect;

    public GameObject deathSound;



    // private bool active = true;


    // Start is called before the first frame update
    void Start()
    {
        fireTimer = fireRate;
    }

    // Update is called once per frame

    void Update()
    {
        transform.localScale = new Vector3(0.75f + Ehealth * sizeScale, 0.75f + Ehealth * sizeScale, 1);
        if (!gameObject.GetComponent<Renderer>().isVisible)
        {
            shouldShoot = false;
        }
        else
        {
            shouldShoot = true;
        }
        //itemsDroppedP = numberOfItemsDroppedP;
        if (1 == 1)
        {
            /*            if (Vector3.Distance(transform.position, PlayerControl.instance.transform.position) <= rangeToChasePlayer)
                        {
                            moveDirection = PlayerControl.instance.transform.position - transform.position;
                        }
                        else
                        {
                            moveDirection = Vector3.zero;
                        }*/

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;

            if (shouldShoot)
            {
                Vector2 offset = new Vector2(PlayerControl.instance.transform.position.x - transform.position.x, PlayerControl.instance.transform.position.y - transform.position.y);
                float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;


                    gun.rotation = Quaternion.Euler(0, 0, angle + 90);
                

                if (Vector3.Distance(transform.position, PlayerControl.instance.transform.position) <= fireRange)
                {
                    fireTimer -= Time.deltaTime;
                    fireDelayTemp -= Time.deltaTime;

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
            if (Ehealth <= 0)
            {
                    float dropChance = Random.Range(0, 100f);
                    if (dropChance <= itemDropRateP)
                    {
                        int randomItem = Random.Range(0, itemsToDropP.Length);
                        Instantiate(itemsToDropP[randomItem], transform.position, transform.rotation);
                    }

                Instantiate(deathSound, transform.position, transform.rotation);
                Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(gameObject);

                //PlayerControl.instance.health ++;

            }
        }
    }

    public void DamageEnemy(int damage)
    {
        Ehealth -= damage;
        Instantiate(hurtEffect, transform.position, transform.rotation);

    }




}
