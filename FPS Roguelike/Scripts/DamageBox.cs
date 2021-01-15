using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public float damageToDo;//damage to give
    public float duration;//duration to exist
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;//keep track of duration
        //when out of time destroy this object
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    //this function is called when something collides with this object, the input is the other object
    private void OnTriggerEnter(Collider other)
    {
        //if the other object is an enemy damage it
        if (other.transform.tag == "Enemy")
        {
            other.transform.parent.GetComponent<Enemy>().takeDamage(damageToDo);
        }
        //if the other object is the player damage it
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<PlayerControl>().hit(damageToDo);
        }
        //if the other object is a mine explode it
        if (other.transform.tag == "Mine")
        {
            other.transform.GetComponent<Mine>().explode();
        }
    }
}
