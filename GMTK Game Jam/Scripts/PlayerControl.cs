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
    //private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gun;

    public Animator anim;

    public int ammo;

    public Vector2 offset;



    // public GameObject bulletToFire;
    // public Transform firePoint;

    // public float timeBetweenShots;
    // private float shotTimer;

    public int health;
    public int maxHealth;
    public float deathEffect;

    public int currentPoints;

    public bool canMove = true;

    public float bombTime = 0;

    public GameObject bombEffect;


    private float hurtTimer;

    // public Image hurtScreen;
    // public GameObject hurt;
    // public float hurtFadeSpeed;
    // private bool hurtFadeIn, hurtFadeOut;


    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in allGuns)
        {
            item.SetActive(false);
        }
        allGuns[WeaponSelect.instance.slot1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UI.instance.pointText.text = currentPoints.ToString();
        UI.instance.bombText.text = ammo.ToString();

        hurtTimer -= Time.deltaTime;
        bombTime -= Time.deltaTime;

        if (hurtTimer <= 0)
        {
            hit = false;
        }

        if (health > 3)
        {
            health--;
        }
        if (health < 3)
        {
            Three.SetActive(false);
        }
        else
        {
            Three.SetActive(true);
        }
        if (health < 2)
        {
            Two.SetActive(false);
        }
        else
        {
            Two.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (GameObject item in allGuns)
            {
                item.SetActive(false);
            }
            allGuns[WeaponSelect.instance.slot1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (GameObject item in allGuns)
            {
                item.SetActive(false);
            }
            allGuns[WeaponSelect.instance.slot2].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (GameObject item in allGuns)
            {
                item.SetActive(false);
            }
            allGuns[WeaponSelect.instance.slot3].SetActive(true);
        }

        //get wasd input
        if (canMove)
        {
            //moveInput.x = Input.GetAxisRaw("Horizontal");
            //moveInput.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
           
        }

        //set velocity equal to wasd input times the move speed
        if (canMove && !Manager.instance.isPaused)
        {
            //get mouse position
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

            //rotate gun
            offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;


            gun.rotation = Quaternion.Euler(0, 0, angle-90);

            if(Input.GetKeyDown("space") && ammo > 0)
            {
                ammo--;
                bombTime = 0.5f;
                Instantiate(bombEffect, transform.position, transform.rotation);
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
            //find a way to stop movement but keep momentum
        }
    }
    public void DamagePlayer(int damage)
    {
        if (hit == false)
        {
            health -= damage;
            hurtTimer = 1;
            UI.instance.Hurt();
        }
        hit = true;

        if (health <= 0)
        {
            //Instantiate(deathEffect, transform.position, transform.rotation);
            UI.instance.deathScreen.SetActive(true);
            Manager.instance.isPaused = true;

            Time.timeScale = 0f;
            //Destroy(gameObject);
        }
        else
        {
            //UI.instance.deathScreen.SetActive(false);
        }
    }




    public void GainPoints(int ammount)
    {
        currentPoints += ammount;
        Manager.instance.currentProgress += ammount;
    }
    public void GainBomb()
    {
        ammo++;
    }
}
