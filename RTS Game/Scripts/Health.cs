using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        //Instantiate(hitEffect, hitSpot[Random.Range(0, hitSpot.Length)].position, hitSpot[Random.Range(0, hitSpot.Length)].rotation);
    }
}
