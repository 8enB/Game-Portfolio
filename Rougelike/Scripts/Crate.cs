using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    public int health;
    private int currentHealth;
    public bool canDie;

    public GameObject deathEffect;
    

    public bool shouldDropItemP;
    public GameObject[] itemsToDropP;
    public float itemDropRateP;

    public bool shouldDropItemSG;
    public GameObject[] itemsToDropSG;
    public float itemDropRateSG;

    public bool shouldDropItemR;
    public GameObject[] itemsToDropR;
    public float itemDropRateR;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentHealth <= 0 && canDie)
        {
            
            if(shouldDropItemP)
            {
                float dropChance = Random.Range(0, 100f);
                if(dropChance <= itemDropRateP)
                {
                   int randomItemP = Random.Range(0, itemsToDropP.Length);
                   Instantiate(itemsToDropP[randomItemP], transform.position, transform.rotation);
                }
            }
            if(shouldDropItemSG)
            {
                float dropChance = Random.Range(0, 100f);
                if(dropChance <= itemDropRateSG)
                {
                   int randomItemSG = Random.Range(0, itemsToDropSG.Length);
                   Instantiate(itemsToDropSG[randomItemSG], transform.position, transform.rotation);
                }
            }
            if(shouldDropItemR)
            {
                float dropChance = Random.Range(0, 100f);
                if(dropChance <= itemDropRateR)
                {
                   int randomItemR = Random.Range(0, itemsToDropR.Length);
                   Instantiate(itemsToDropR[randomItemR], transform.position, transform.rotation);
                }
            }
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            shouldDropItemP = true;
            shouldDropItemSG = false;
            shouldDropItemR = false;
            currentHealth --;
        }
        if(other.tag == "EBullet")
        {
            shouldDropItemP = true;
            shouldDropItemSG = false;
            shouldDropItemR = false;
            currentHealth --;
        }
        if(other.tag == "PBullet")
        {
            shouldDropItemP = true;
            shouldDropItemSG = false;
            shouldDropItemR = false;
            currentHealth --;
        }
        if(other.tag == "SGBullet")
        {
            shouldDropItemP = false;
            shouldDropItemSG = true;
            shouldDropItemR = false;
            currentHealth --;
        }
        if(other.tag == "RBullet")
        {
            shouldDropItemP = false;
            shouldDropItemSG = false;
            shouldDropItemR = true;
            currentHealth --;
        }
    }
}
