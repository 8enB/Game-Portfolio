using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    public GameObject One;
    public GameObject Two;
    public GameObject Three;

    public GameObject Middle;
    public GameObject Left;
    public GameObject Right;
    
    public BoxCollider2D MiddleC;
    public BoxCollider2D LeftC;
    public BoxCollider2D RightC;

    public bool hit;

    public float moveSpeed;
    private Vector2 moveInput;

    public float shotTime;

    public Rigidbody2D theRB;


    public Animator anim;

    public int shotgunAmmo;

    public int rifleAmmo;



    // public GameObject bulletToFire;
    // public Transform firePoint;

    // public float timeBetweenShots;
    // private float shotTimer;

    public int health;
    public int maxHealth;
    public float deathEffect;

    public int currentCoins;

    public bool canMove = true;

    private float hurtTimer;

    public float sizeScale;

    public AudioSource hurtSound;

    // public Image hurtScreen;
    // public GameObject hurt;
    // public float hurtFadeSpeed;
    // private bool hurtFadeIn, hurtFadeOut;


    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = new Vector3(0.75f + health * sizeScale, 0.75f + health * sizeScale, 1);


        UI.instance.coinText.text = currentCoins.ToString();
 
        hurtTimer -= Time.deltaTime;
        if (hurtTimer <= 0)
        {
            hit = false;
        }

        if (health > 30)
        {
            health--;
        }
        if (health < 20)
        {
            Three.SetActive(false);
            Right.SetActive(false);
            RightC.enabled = false;
        }
        else
        {
            Three.SetActive(true);
            Right.SetActive(true);
            RightC.enabled = true;
        }
        if (health < 10)
        {
            Two.SetActive(false);
            Left.SetActive(false);
            LeftC.enabled = false;
        }
        else
        {
            Two.SetActive(true);
            Left.SetActive(true);
            LeftC.enabled = true;
        }

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
        if (canMove && !LevelManager.instance.isPaused)
        {
            if (moveInput.x != 0 & moveInput.y != 0)
            {
                theRB.velocity = moveInput * moveSpeed / (1.4f);
            }
            else
            {
                theRB.velocity = moveInput * moveSpeed;
            }


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
    public void DamagePlayer(int damage)
    {
        // if (hit == false)
        // {
        hurtSound.Play();
        health -= damage;
            hurtTimer = 1;
            UI.instance.Hurt();
        //}
        //hit = true;

        if (health <= 0)
        {
            //Instantiate(deathEffect, transform.position, transform.rotation);
            UI.instance.deathScreen.SetActive(true);
            UI.instance.healthSlide.enabled = false ;
            Destroy(gameObject);
        }
        else
        {
            UI.instance.deathScreen.SetActive(false);
        }
    }

    public void GainCoin(int ammount)
    {
        currentCoins += ammount;
    }
    public void LoseCoin(int ammount)
    {
        currentCoins -= ammount;


        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            DamagePlayer(5);

        }
        if (other.tag == "Boss")
        {
            DamagePlayer(5);

        }

    }

}
