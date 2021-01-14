using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserHitbox : MonoBehaviour
{

    public GameObject owner;
    public int damageGive;
    public GameObject deathEffect;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            other.GetComponent<PlayerControl>().DamagePlayer(damageGive);
            Destroy(owner);
        }
    }
}
