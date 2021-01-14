using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Animator anim;

    public bool hit = false;
    public int health;
    public int maxHealth;
    public float deathEffect;

    public int currentCoins;

    public GameObject[] locked;

    public bool canMove = true;

    public bool hacking = false;

    public BoxCollider2D bc;

    public string thing;


    private LineRenderer lr;

    private GameObject currentServer;

    private void Awake()
    {
        instance = this;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        currentServer = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, currentServer.transform.position);
        //get wasd input
        if (canMove)
        {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
        moveInput.x = 0;
        moveInput.y = 0;
        }

        //set velocity equal to wasd input times the move speed
    if(canMove/* && !LevelManager.instance.isPaused*/)
    {

        if(Input.GetKeyDown(KeyCode.H))
            {
                if(!hacking)
                {
                    hacking = true;
                    bc.enabled = false;
                }
                else
                {
                    hacking = false;
                    bc.enabled = true;
                }
            }

            if (!hacking)
            {
                if (moveInput.x != 0 & moveInput.y != 0)
                {
                    theRB.velocity = moveInput * moveSpeed / (1.4f);
                }
                else
                {
                    theRB.velocity = moveInput * moveSpeed;
                }
            }
            else
            {
                theRB.velocity = new Vector2 (0,0);
            }

        //get mouse position
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

/*        //rotate gun
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
        if(mousePos.x < screenPoint.x)
        {
            gun.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else
        {
            gun.rotation = Quaternion.Euler(0, 0, angle);
        }*/

        //fire bullet
        // if(Input.GetMouseButtonDown(0))
        // {
        //     Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
        // }
        // shotTimer -= Time.deltaTime;
        // if(Input.GetMouseButton(0))
        // {
        //     if(shotTimer<=0)
        //     {
        //         Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
        //         shotTimer=timeBetweenShots;
        //     }
        // }
    }
    else
    {
        theRB.velocity = new Vector2(0f, 0f);
    }
}
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            DamagePlayer(1);
        }
        if(other.tag == "Server")
        {
            foreach (GameObject locke in locked)
            {
                locke.SetActive(false);
            }
            currentServer = other.gameObject;
        }
        if(other.tag == "Room")
        {
            thing = other.tag;
            other.gameObject.GetComponent<RoomActivate>().OnActivate();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!hacking)
        {
            if (other.tag == "Server")
            {
                foreach (GameObject locke in locked)
                {
                    locke.SetActive(true);
                }
                currentServer = gameObject;
            }
            if(other.tag == "Room")
            {
                other.gameObject.GetComponent<RoomActivate>().OnDeActivate();
                thing = "123";
            }
        }
    }
    public void DamagePlayer(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            SceneManager.LoadScene("TestScene");
        }

    }
    public void GainCoin(int ammount)
    {
        currentCoins += ammount;
    }
    public void LoseCoin(int ammount)
    {
        currentCoins -= ammount;


        if(currentCoins<0)
        {
            currentCoins = 0;
        }
    }
    
}
