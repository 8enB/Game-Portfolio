using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] activate;
    public GameObject[] deactivate;
    public string activeTag;
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
        if (other.tag == activeTag)
        {
            foreach (GameObject item in activate)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in deactivate)
            {
                item.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == activeTag)
        {
            foreach (GameObject item in activate)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in deactivate)
            {
                item.SetActive(true);
            }
        }
    }
}
