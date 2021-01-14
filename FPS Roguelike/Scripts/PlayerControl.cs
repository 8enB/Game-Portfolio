using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    //public CharacterController controller;

    public Rigidbody theRB;
    private float downforce;
    public float speed = 12f;
    public float health;
    public float airSpeed;
    public float jumpPower = 1f;
    public int numOfJumps = 1;
    public float gravity = -4f;

    public GameObject[] guns;

    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundMask;
    bool isGrounded;

    public int camera = 3;
    public Transform player;

    public GameObject body;

    public Vector3 speedOnJump;

    private Vector3 move;

    private Vector3 airMove;

    private bool justJumped;

    private int jumpsLeft;

    private Vector3 moveInput;

    public bool cap;

    private float velY;

    private float trueSpeed;

    private float hitTimer = 0;



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
        hitTimer -= Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        trueSpeed = Mathf.Sqrt(Mathf.Pow(theRB.velocity.x, 2f) + Mathf.Pow(theRB.velocity.z, 2f));

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach(GameObject gun in guns)
            {
                gun.SetActive(false);
            }
            guns[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (GameObject gun in guns)
            {
                gun.SetActive(false);
            }
            guns[1].SetActive(true);
        }

        velY = theRB.velocity.y;
                if (isGrounded && downforce < 0)
                {
                    downforce = -2f;
                    jumpsLeft = numOfJumps;
                }
                if(trueSpeed >= speed / (1.4f) || trueSpeed <= -speed / (1.4f))
                {
                    cap = true;
                }
                else
                {
                    cap = false;
                }

                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.z = Input.GetAxisRaw("Vertical");

                move = (transform.right * moveInput.x + transform.forward * moveInput.z);
                
                airMove = (transform.right * moveInput.x);

                if (isGrounded || justJumped)
                {
                    if (moveInput.x != 0 & moveInput.z != 0)
                    {
                        theRB.velocity = new Vector3(move.x * speed / (1.4f), velY, move.z * speed / (1.4f));
                    }
                    else
                    {
                        theRB.velocity = new Vector3(move.x * speed, velY, move.z * speed);
                    }
                    speedOnJump = theRB.velocity;
                }
                else
                {
                    if (moveInput.x != 0 && moveInput.z != 0)
                    {
                        if (!cap || moveInput.z != 1)
                        {
                            theRB.velocity += new Vector3((move.x * airSpeed / (1.4f)) * Time.deltaTime, 0, (move.z * airSpeed / (1.4f)) * Time.deltaTime);
                        }
                        else
                        {
                            theRB.velocity += new Vector3((airMove.x * airSpeed / (1.4f)) * Time.deltaTime, 0, (airMove.z * airSpeed / (1.4f)) * Time.deltaTime);
                        }
                        if (theRB.velocity.x > speed / (1.4f))
                        {
                            theRB.velocity = new Vector3(speed / (1.4f), theRB.velocity.y, theRB.velocity.z);
                        }
                        if (theRB.velocity.x < -speed / (1.4f))
                        {
                            theRB.velocity = new Vector3(-speed / (1.4f), theRB.velocity.y, theRB.velocity.z);
                        }
                        if (theRB.velocity.z > speed / (1.4f))
                        {
                            theRB.velocity = new Vector3(theRB.velocity.x, theRB.velocity.y, speed / (1.4f));
                        }
                        if (theRB.velocity.z < -speed / (1.4f))
                        {
                            theRB.velocity = new Vector3(theRB.velocity.x, theRB.velocity.y, -speed / (1.4f));
                        }
                    }
                    else
                    {
                        if (!cap || moveInput.z != 1)
                        {
                            theRB.velocity += new Vector3((move.x * airSpeed) * Time.deltaTime, 0, (move.z * airSpeed) * Time.deltaTime);
                        }
                        else
                        {
                            theRB.velocity += new Vector3((airMove.x * airSpeed) * Time.deltaTime, 0, (airMove.z * airSpeed) * Time.deltaTime);
                        }
                        
                        if (theRB.velocity.x > speed)
                        {
                            theRB.velocity = new Vector3(speed, theRB.velocity.y, theRB.velocity.z);
                        }
                        if (theRB.velocity.x < -speed)
                        {
                            theRB.velocity = new Vector3(-speed, theRB.velocity.y, theRB.velocity.z);
                        }
                        if (theRB.velocity.z > speed)
                        {
                            theRB.velocity = new Vector3(theRB.velocity.x, theRB.velocity.y, speed);
                            cap = true;
                        }
                        else
                        {
                            cap = false;
                        }
                        if (theRB.velocity.z < -speed)
                        {
                            theRB.velocity = new Vector3(theRB.velocity.x, theRB.velocity.y, -speed);
                            cap = true;
                        }
                        else
                        {
                            cap = false;
                        }
                    }
                    
                }

                
                //controller.Move(move * speed * Time.deltaTime);

                if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
                {
                    theRB.velocity = new Vector3(theRB.velocity.x, Mathf.Sqrt(jumpPower * -2f * gravity), theRB.velocity.z);
                    jumpsLeft--;
                    downforce = 0;
                    justJumped = true;
                }
                else
                {
                    justJumped = false;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
                {
                    downforce = -500f;
                }

                downforce += gravity * Time.deltaTime;

                theRB.velocity = new Vector3 (theRB.velocity.x, theRB.velocity.y + (downforce * Time.deltaTime), theRB.velocity.z);
    }
    public void hit(float dmg)
    {
        if (hitTimer <= 0)
        {
            hitTimer = 1.5f;
            health -= dmg;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
