using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightLine : MonoBehaviour
{
    //public GameObject body;

    public bool shouldFire = true;
    public GameObject emitterPoint;
    public GameObject laserEnd;
    public float rotateTop;
    public Slider rotateControlTop;
    public float rotateBottom;
    public Slider rotateControlBottom;
    public float spinRate = 100f;
    public Slider spinSpeed;

    public bool spin = false;
    public Slider spinControl;
    public bool rotate = true;
    public Slider rotateControl;

    public string endTag;

    public Text whatTag;

    public GameObject[] endPoints;

    public bool useRange;
    public float range;
    public Slider rangeSlide;

    public GameObject start;
    public GameObject end;

    public GameObject field;
    public GameObject fullRangeSlider;
    public GameObject fullTopSlider;
    public GameObject fullBottomSlider;
    public GameObject fullSpeedSlider;
    public GameObject fullTypeSlider;

    public bool changedBounds = false;
    public bool changedSpeed = false;
    public bool changedType = false;
    public bool changedTag = false;
    public bool changedRange = false;


    public bool listenToAi;
    public bool isCamera;

    private bool rangeLocked = false;
    private bool tagLocked = false;
    private bool boundLocked = false;
    private bool speedLocked = false;
    private bool typeLocked = false;

    private LineRenderer lr;

    private bool goingRight = true;

    private float angle;

    public bool boundVspinRate;

    public GameObject owner;
    public float circleAngle;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        rotateControlTop.value = rotateTop;
        rotateControlBottom.value = rotateBottom;
        rangeSlide.value = range;
        spinSpeed.value = spinRate;
       

        if(listenToAi)
        {
            if(isCamera)
            {
                if(AiTracking.instance.disableRange)
                {
                    rangeLocked = true;
                    fullRangeSlider.SetActive(false);
                }
                if(AiTracking.instance.disableTag)
                {
                    tagLocked = true;
                    field.SetActive(false);
                }
            }
            else
            {
                if(AiTracking.instance.disableBounds && boundVspinRate)
                {
                    boundLocked = true;
                    fullBottomSlider.SetActive(false);
                    fullTopSlider.SetActive(false);
                }
                else if(AiTracking.instance.disableSpinSpeed && boundVspinRate)
                {
                    speedLocked = true;
                    fullSpeedSlider.SetActive(false);
                }
                if(AiTracking.instance.disableTag)
                {
                    tagLocked = true;
                    field.SetActive(false);
                }
                if(AiTracking.instance.disableSpinType)
                {
                    typeLocked = true;
                    fullTypeSlider.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateTop != rotateControlTop.value || rotateBottom != rotateControlBottom.value)
        {
            if(!changedBounds)
            {
                changedBounds = true;
                AiTracking.instance.bounds++;
                AiTracking.instance.boundsTotal++;
            }
        }

        if (spinRate != spinSpeed.value)
        {
            if (!changedSpeed)
            {
                changedSpeed = true;
                AiTracking.instance.spinSpeed++;
                AiTracking.instance.spinSpeedTotal++;
            }
        }

        if (range != rangeSlide.value)
        {
            if (!changedRange)
            {
                changedRange = true;
                AiTracking.instance.range++;
                AiTracking.instance.rangeTotal++;
            }
        }

        if (spinControl.value == 1 && spin == false || spinControl.value == 0 && spin == true)
        {
            changedType = true;
            AiTracking.instance.spinType++;
            AiTracking.instance.spinTypeTotal++;
        }

        if(endTag != "Enemy" && !tagLocked)
        {
            if (!changedTag)
            {
                changedTag = true;
                AiTracking.instance.tag++;
                AiTracking.instance.tagTotal++;
            }
        }

        spinRate = spinSpeed.value;
        rotateTop = rotateControlTop.value;
        rotateBottom = rotateControlBottom.value;
        if (!rangeLocked)
        {
            range = rangeSlide.value;
        }
        else
        {
            rangeSlide.value = range;

        }

        if (spinControl.value == 1)
        {
            spin = true;
            rotate = false;
        }
        else
        {
            spin = false;
            rotate = true;
        }
        if (whatTag.text == "" || tagLocked)
        {
            endTag = "Enemy";
            foreach(GameObject points in endPoints)
            {
                points.tag = endTag;
            }

            lr.SetColors(new Color(255, 0, 0), new Color(255, 0, 0));
        }
        else
        {
            endTag = "lol";
            foreach (GameObject points in endPoints)
            {
                points.tag = endTag;
            }
            lr.SetColors(new Color(0, 255, 255), new Color(0, 255, 255));
        }
        
        /*if (rotateControl.value == 1)
        {
            rotate = true;
        }
        else
        {
            rotate = false;
        }*/
        if (!PlayerControl.instance.hacking)
        {
            angle = transform.localEulerAngles.z;
            angle = (angle > 180) ? angle - 360 : angle;

            circleAngle = transform.localEulerAngles.z + owner.transform.localEulerAngles.z + 90f;
            circleAngle = (circleAngle > 180) ? circleAngle - 360 : circleAngle;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, range);

            if (Physics2D.Raycast(transform.position, transform.up, range))
            {
                //laserEnd.transform.position = hit.point;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, hit.point);
                if (hit.transform.tag == "Player")
                {
                    PlayerControl.instance.DamagePlayer(10);
                }
            }
            else
            {
                
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, new Vector3(transform.position.x + (range * Mathf.Cos(circleAngle * (3.1415f / 180))), transform.position.y + (range * Mathf.Sin(circleAngle * (3.1415f / 180))), transform.position.z));
            }

            

            

            if (spin)
            {
                transform.Rotate(0f, 0.0f, 1f * spinRate * Time.deltaTime, Space.Self);
                //end.transform.Rotate(0f, 0.0f, 1f * spinRate * Time.deltaTime, Space.Self);
                //transform.Rotate(0f, 0f, 0f);
                //body.transform.Rotate(0f, 0.0f, 1f * spinRate * Time.deltaTime, Space.Self);
            }
            if (rotate)
            {
                if (goingRight)
                {
                    transform.Rotate(0f, 0.0f, -1f * spinRate * Time.deltaTime, Space.Self);
                    //end.transform.Rotate(0f, 0.0f, -1f * spinRate * Time.deltaTime, Space.Self);
                }
                if (!goingRight)
                {
                    transform.Rotate(0f, 0.0f, 1f * spinRate * Time.deltaTime, Space.Self);
                    //end.transform.Rotate(0f, 0.0f, 1f * spinRate * Time.deltaTime, Space.Self);
                }
                if (goingRight && angle < rotateBottom)
                {
                    goingRight = false;
                }
                if (!goingRight && angle > rotateTop)
                {
                    goingRight = true;
                }
            }
        }
    }
}
