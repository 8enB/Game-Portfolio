using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTableControl : MonoBehaviour
{

    public static EnemyTableControl target;

    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
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

    public int numberOfItemsDroppedSG = 1;
    private int itemsDroppedSG;
    public bool shouldDropItemSG;
    public GameObject[] itemsToDropSG;
    public float itemDropRateSG;

    public int numberOfItemsDroppedR = 1;
    private int itemsDroppedR;
    public bool shouldDropItemR;
    public GameObject[] itemsToDropR;
    public float itemDropRateR;
    private bool fire;
    private int count = 0;

    public GameObject hurtEffect;

   

   // private bool active = true;

    private void Awake()
    {
        target = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fireTimer=fireRate;
    }

    // Update is called once per frame
  
    void Update()
    {
        itemsDroppedP = numberOfItemsDroppedP;
        itemsDroppedSG = numberOfItemsDroppedSG;
        itemsDroppedR = numberOfItemsDroppedR;
        if(1==1)
        {
        if(Vector3.Distance(transform.position, PlayerControl.instance.transform.position)<=rangeToChasePlayer)
        {
            moveDirection = PlayerControl.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection*moveSpeed;

        if(shouldShoot)
        {
            Vector2 offset = new Vector2(PlayerControl.instance.transform.position.x - transform.position.x, PlayerControl.instance.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
            if(PlayerControl.instance.transform.position.x < transform.position.x)
            {
            gun.rotation = Quaternion.Euler(180, 0, -angle);
            }
            else
            {
            gun.rotation = Quaternion.Euler(0, 0, angle);
            }

            if(Vector3.Distance(transform.position, PlayerControl.instance.transform.position)<=fireRange)
            {
            fireTimer -= Time.deltaTime;
            fireDelayTemp -= Time.deltaTime;

            if(fireTimer<=0)
            {
                fire = true;
                if(count >= firePoints.Length)
                {
                    count = 0;
                    fire = false;
                } 
            }
            if(fireDelayTemp<=0 && fire == true)
                    {
                    Instantiate(bullet, firePoints[count].transform.position, firePoints[count].transform.rotation);
                    fireDelayTemp = fireDelay;
                    count++;
                    fireTimer=fireRate;
                    }
            }
        }
         if(Ehealth <= 0)
        {
            if(shouldDropItemP)
            {
                while(itemsDroppedP > 0)
                {
                float dropChance = Random.Range(0, 100f);
                if(dropChance <= itemDropRateP)
                {
                   int randomItem = Random.Range(0, itemsToDropP.Length);
                   Instantiate(itemsToDropP[randomItem], transform.position, transform.rotation);
                   Instantiate(deathEffect, transform.position, transform.rotation);
                }
                itemsDroppedP--;
                }
            }

            if(shouldDropItemSG)
            {
                while(itemsDroppedSG > 0)
                {
                float dropChance = Random.Range(0, 100f);
                if(dropChance <= itemDropRateSG)
                {
                   int randomItem = Random.Range(0, itemsToDropSG.Length);
                   Instantiate(itemsToDropSG[randomItem], transform.position, transform.rotation);
                   Instantiate(deathEffect, transform.position, transform.rotation);
                }
                itemsDroppedSG--;
                }
            }

            if(shouldDropItemR)
            {
                while(itemsDroppedR > 0)
                {
                float dropChance = Random.Range(0, 100f);
                if(dropChance <= itemDropRateR)
                {
                   int randomItem = Random.Range(0, itemsToDropR.Length);
                   Instantiate(itemsToDropR[randomItem], transform.position, transform.rotation);
                   Instantiate(deathEffect, transform.position, transform.rotation);
                }
                itemsDroppedR--;
                }
            }
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            
            //PlayerControl.instance.health ++;
            
        }
        }
    }

    public void DamageEnemy(int damage, int ammoType)
    {
        Ehealth -= damage;
        Instantiate(hurtEffect, transform.position, transform.rotation);

        if(ammoType==0)
        {
            shouldDropItemP = true;
            shouldDropItemSG = false;
            shouldDropItemR = false;
        }
        if(ammoType==1)
        {
            shouldDropItemSG = true;
            shouldDropItemP = false;
            shouldDropItemR = false;
        }
        if(ammoType==2)
        {
            shouldDropItemR = true;
            shouldDropItemP = false;
            shouldDropItemSG = false;
        }
       
    }

}
