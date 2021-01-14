using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float RPS;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(transform.tag == "J Unit" && time <= 0)
        {
            PlayerControl.instance.jResource += RPS;
            time = 1;
        }
        if (transform.tag == "USA Unit" && time <= 0)
        {
            PlayerControl.instance.usaResource += RPS;
            time = 1;
        }
    }
}
