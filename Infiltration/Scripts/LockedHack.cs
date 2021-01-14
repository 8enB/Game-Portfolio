using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedHack : MonoBehaviour
{

    public GameObject locked;

    public string thing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        thing = other.tag;
        if (other.tag == "Player")
        {
            locked.SetActive(false);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        thing = other.tag;
        if (other.tag == "Player")
        {
            locked.SetActive(false);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        thing = other.tag;
        if (other.tag == "Player")
        {
            locked.SetActive(true);
        }
    }
}
