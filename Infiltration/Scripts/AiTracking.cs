using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTracking : MonoBehaviour
{
    public static AiTracking instance;


    public int range = 0;
    public int tag = 0;
    public int moveSpeed = 0;
    public int bounds = 0;
    public int spinSpeed = 0;
    public int spinType = 0;
    public int route = 0;

    public int rangeTotal = 0;
    public int tagTotal = 0;
    public int moveSpeedTotal = 0;
    public int boundsTotal = 0;
    public int spinSpeedTotal = 0;
    public int spinTypeTotal = 0;
    public int routeTotal = 0;

    public bool disableRange = false;
    public bool disableTag = false;
    public bool disableMoveSpeed = false;
    public bool disableBounds = false;
    public bool disableSpinSpeed = false;
    public bool disableSpinType = false;
    public bool disableRoute = false;

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rangeTotal > tagTotal)
        {
            disableRange = true;
            disableTag = false;
        }
        else if(tagTotal > rangeTotal)
        {
            disableRange = false;
            disableTag = true;
        }

        if(moveSpeedTotal > routeTotal/2 && moveSpeed != 0)
        {
            disableMoveSpeed = true;
            disableRoute = false;
        }
        else if(route != 0)
        {
            disableRoute = true;
            disableMoveSpeed = false;
        }

        if(boundsTotal/2 > spinSpeedTotal && boundsTotal !=0)
        {
            disableSpinSpeed = false;
            disableBounds = true;
        }
        else if(spinSpeed != 0)
        {
            disableSpinSpeed = true;
            disableBounds = false;
        }
        if(spinTypeTotal > boundsTotal / 2 && spinType > 0)
        {
            disableSpinType = true;
        }
        else
        {
            disableSpinType = false;
        }

    }
}
