using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivate : MonoBehaviour
{
    public GameObject[] activate;
    public GameObject[] deActivate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActivate()
    {
        PlayerControl.instance.hacking = true;
        PlayerControl.instance.hacking = false;
        foreach (GameObject item in activate)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in deActivate)
        {
            item.SetActive(false);
        }
    }
    public void OnDeActivate()
    {
        foreach (GameObject item in activate)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in deActivate)
        {
            item.SetActive(true);
        }
    }
}
