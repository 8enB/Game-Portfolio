using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{

    public Transform[] positions;
    public float speed;
    private int steps;
    private int currentStep = 1;
    public GameObject head;
    public Slider moveSpeed;
    public GameObject fullSpeedSlider;
    public CircleCollider2D bc;
    public bool changedSpeed = false;
    public int minSpeed;
    public int maxSpeed;
    public bool listenToAi;
    public Rigidbody2D theRB;

    public CircleCollider2D inFront;
    private bool turn = false;
    private bool left;
    private bool right;

    private bool speedLocked = false;

    private float angle;

    private float velocityRB;

    

    void Start()
    {
        steps = positions.Length;
        
        moveSpeed.maxValue = maxSpeed;
        moveSpeed.minValue = minSpeed;
        moveSpeed.value = speed;
        if (listenToAi)
        {
            if (AiTracking.instance.disableMoveSpeed)
            {
                speedLocked = true;
                fullSpeedSlider.SetActive(false);
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        velocityRB = theRB.velocity.x + theRB.velocity.y;
        if (speed != moveSpeed.value)
        {
            if (!changedSpeed)
            {
                changedSpeed = true;
                AiTracking.instance.moveSpeed++;
                AiTracking.instance.moveSpeedTotal++;
            }
        }
        speed = moveSpeed.value;
        if (!PlayerControl.instance.hacking)
        {
            bc.enabled = true;
            if (currentStep <= steps)
            {
                //transform.position = Vector3.MoveTowards(transform.position, positions[currentStep - 1].position, speed * Time.deltaTime);
                /*directionalVector = (positions[currentStep - 1].position - transform.position).normalized * speed;
                GetComponent<Rigidbody2D>().velocity = directionalVector;*/
                theRB.velocity = transform.up * speed/4;
                // if (positions[currentStep - 1].position == transform.position)
                if (inFront.bounds.Contains(positions[currentStep - 1].position))
                    {
                    currentStep++;
                    if ((((Mathf.Atan2(positions[currentStep - 1].position.y - transform.position.y, positions[currentStep - 1].position.x - transform.position.x) * 180) / Mathf.PI) - 90) > -90)
                    {
                        left = true;
                        right = false;
                    }
                    else
                    {
                        left = false;
                        right = true;
                    }
                }
            }
            else
            {
                currentStep = 1;
            }
            if (turn == false)
            {
                angle = transform.localEulerAngles.z;
                transform.rotation = Quaternion.Euler(0, 0, ((Mathf.Atan2(positions[currentStep - 1].position.y - transform.position.y, positions[currentStep - 1].position.x - transform.position.x) * 180) / Mathf.PI) - 90);
                
            }
            else if(left)
            {
                if (theRB.velocity.x + theRB.velocity.y < 0.2f && speed >= 0.2f && theRB.velocity.x + theRB.velocity.y > -0.2f)
                {
                    angle = transform.localEulerAngles.z;
                }
                if (angle >= 270f && angle < 360f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (angle >= 180f && angle < 270f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                if (angle >= 90f && angle < 180f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                if (angle >= 0f && angle < 90f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                //transform.rotation = Quaternion.Euler(0, 0, ((Mathf.Atan2(positions[currentStep - 1].position.y - transform.position.y, positions[currentStep - 1].position.x - transform.position.x) * 180) / Mathf.PI) - 0);
                //transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                //transform.rotation = Quaternion.Euler(0, 0, ((Mathf.Atan2(positions[currentStep - 1].position.y - transform.position.y, positions[currentStep - 1].position.x - transform.position.x) * 180) / Mathf.PI) - 180);
                if (theRB.velocity.x + theRB.velocity.y < 0.2f && speed >= 0.2f && theRB.velocity.x + theRB.velocity.y > -0.2f)
                {
                    angle = transform.localEulerAngles.z;
                }
                if (angle > 270f && angle <= 360f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                if (angle > 180f && angle <= 270f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                if (angle > 90f && angle <= 180f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                if (angle > 0f && angle <= 90f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            
            
        }
        else
        {
            bc.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Obstical")
        {
            turn = true;
            inFront.radius = 1.1f;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Obstical")
        {
            turn = false;
            inFront.radius = 0.7f;
        }
    }

}
