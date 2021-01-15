using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public bool mobile;//set to true if the mine is mobile

    public GameObject explosion;//the explosion object/effect

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //this function checks what object has collided with the mine and calls the explode function if needed
    //called when anything collides with this object
    //input is the object that has collided with this object
    private void OnTriggerEnter(Collider other)
    {
        if (!mobile) //if not mobile
        {
            if (other.transform.tag == "Enemy")//if hit by an enemy explode
            {
                explode();
            }
            if (other.transform.tag == "Player")//if hit by a player explode
            {
                explode();
            }
        }
        else//if mobile
        {
            if (other.transform.tag == "Player")//if hit by a player explode
            {
                explode();
            }
        }
    }

    //function to create explosion and destroy this object
    public void explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);//create the explosion
        Destroy(gameObject);//delete the mine
    }
}
