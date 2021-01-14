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

    public bool hit;

    public GameObject[] guns;
    public GameObject[] allGuns;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gun;

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

    // public Image hurtScreen;
    // public GameObject hurt;
    // public float hurtFadeSpeed;
    // private bool hurtFadeIn, hurtFadeOut;

    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    


    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        UI.instance.coinText.text = currentCoins.ToString();
        UI.instance.SGText.text = shotgunAmmo.ToString();
        UI.instance.RifleText.text = rifleAmmo.ToString();
            hurtTimer -= Time.deltaTime;
        if(hurtTimer<=0)
        {
            hit = false;
        }

        if(health > 3)
        {
            health--;
        }
        if(health < 3)
        {
            Three.SetActive(false);
        }
        else
        {
            Three.SetActive(true);
        }
        if(health < 2)
        {
            Two.SetActive(false);
        }
        else
        {
            Two.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (GameObject item in allGuns)
            {
                item.SetActive(false);
            }
            guns[0].SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (GameObject item in allGuns)
            {
                item.SetActive(false);
            }
            guns[1].SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (GameObject item in allGuns)
            {
                item.SetActive(false);
            }
            guns[2].SetActive(true);
        }

        //get wasd input
        if(canMove)
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
    if(canMove && !LevelManager.instance.isPaused)
    {
        if(moveInput.x!=0 & moveInput.y!=0)
        {
           theRB.velocity = moveInput * moveSpeed/(1.4f); 
        }
        else
        {
        theRB.velocity = moveInput * moveSpeed;
        }

        //get mouse position
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        //rotate gun
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
        if(mousePos.x < screenPoint.x)
        {
            gun.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else
        {
            gun.rotation = Quaternion.Euler(0, 0, angle);
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
        if(hit == false)
        {
        health -= damage;
        hurtTimer=1;
        UI.instance.Hurt();
        }
        hit = true;

        if(health <= 0)
        {
            //Instantiate(deathEffect, transform.position, transform.rotation);
            UI.instance.deathScreen.SetActive(true);
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


        if(currentCoins<0)
        {
            currentCoins = 0;
        }
    }
    public void GainAmmo(int type)
    {
        if(type==1)
        {
            shotgunAmmo++;
        }
        if(type==2)
        {
            rifleAmmo++;
        }
    }
}
