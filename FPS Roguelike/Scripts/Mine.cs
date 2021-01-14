using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public bool mobile;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!mobile)
        {
            if (other.transform.tag == "Enemy")
            {
                explode();
            }
            if (other.transform.tag == "Player")
            {
                explode();
            }
        }
        else
        {
            if (other.transform.tag == "Player")
            {
                explode();
            }
        }
    }
    public void explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
