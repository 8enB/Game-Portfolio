using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] activate;
    public string activeTag;
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
        if(other.tag == activeTag)
        {
            foreach (GameObject item in activate)
            {
                item.SetActive(true);
            }
        }
    }
}
