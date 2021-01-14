using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackIndicator : MonoBehaviour
{
    public bool hackable;

    public bool useServer;
    public GameObject server;

    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(useServer)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, server.transform.position);
        }
        else if(hackable)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, PlayerControl.instance.transform.position);
        }
        else
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position);
        }
    }
}
