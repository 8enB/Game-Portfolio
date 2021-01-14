using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform[] firePoints;
    public Transform[] renderPoint;

    public Transform camera;

    public float minDmg1;
    public float maxDmg;
    public float minDmg2;

    public float rampUpStartRange;
    public float rampUpEndRange;

    public float fallOffStartRange;
    public float fallOffEndRange;

    public float maxRange;

    public Vector3 hitPoint;

    public GameObject impactEffect;
    public GameObject hitSound;
    public GameObject fireSound;
    public GameObject fireEffect;

    public float timeBetweenShots;
    public float magSize;
    public float ammo;
    public float reloadTime;

    public Animator recoil;

    //public GameObject gunShoot;

    private float shotTimer;
    private float reloadTimer;

    private float dmgToDo;

    private float dis;

    public LineRenderer[] lr;



    public float trailTime;
    private float trailTimer;

    private int counter;

    public bool animateIt;
    // Start is called before the first frame update
    void Start()
    {
        ammo = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (LineRenderer lines in lr)
        {
            lines.SetPosition(0, firePoints[0].position);
        }
        trailTimer -= Time.deltaTime;
        if(trailTimer <= 0)
        {
            foreach (LineRenderer lines in lr)
            {
                lines.SetPosition(0, gameObject.transform.position);
                lines.SetPosition(1, gameObject.transform.position);
            }
        }
        shotTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.R) && shotTimer <= 0 && reloadTimer <= 0 && ammo < magSize)
        {
            reloadTimer = reloadTime;
            ammo = magSize;
            if (animateIt)
            {
                recoil.Play("ReloadRev", -1, 0);
                recoil.Play("ReloadRev");
            }
        }
        if (Input.GetMouseButton(0))
        {
                if (ammo <= 0 && shotTimer <= 0)
                {
                    reloadTimer = reloadTime;
                    ammo = magSize;
                if (animateIt)
                {
                    recoil.Play("ReloadRev", -1, 0);
                    recoil.Play("ReloadRev");
                }
                }
            if (shotTimer <= 0 && reloadTimer <= 0)
            {
                if (animateIt)
                {
                    recoil.Play("Fire", -1, 0);
                    recoil.Play("Fire");
                }
                counter = 0;
                ammo--;

                //Instantiate(fireSound, gameObject.transform.position, gameObject.transform.rotation);
                foreach (Transform point in firePoints)
                {
                    //Instantiate(fireEffect, point.position, point.rotation);

                    RaycastHit hitInfo = new RaycastHit();
                    //Vector3 fwd = point.transform.TransformDirection(Vector3.forward);
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag != "unHittable" && hitInfo.transform.tag != "Player")
                    {
                        hitPoint = hitInfo.point;
                        //if(hitInfo.transform.tag == "enemy")
                        //{
                        dis = Vector3.Distance(hitPoint, point.position);
                        lr[counter].SetPosition(0, point.transform.position);
                        lr[counter].SetPosition(1, hitInfo.point);
                        trailTimer = trailTime;
                        if (dis < rampUpStartRange)
                        {
                            dmgToDo = minDmg1;
                        }
                        else if (dis > fallOffEndRange)
                        {
                            dmgToDo = minDmg2;
                        }
                        else
                        {
                            dmgToDo = maxDmg;
                        }
                        if (dis > rampUpStartRange && dis < rampUpEndRange)
                            {
                                dmgToDo = (maxDmg * ((dis - rampUpStartRange) / (rampUpEndRange - rampUpStartRange)));
                                if (dmgToDo < minDmg1)
                                {
                                    dmgToDo = minDmg1;
                                }
                            }
                        if (dis > fallOffStartRange && dis < fallOffEndRange)
                            {
                            // 2 10 30 15
                                dmgToDo = maxDmg + (((minDmg2 - maxDmg) / (fallOffEndRange - fallOffStartRange)) * (dis - fallOffStartRange));
                                if (dmgToDo < minDmg2)
                                {
                                    dmgToDo = minDmg2;
                                }
                            }
                        if(hitInfo.transform.tag == "Enemy")
                        {
                            hitInfo.transform.GetComponent<Enemy>().takeDamage(dmgToDo);
                            //hitInfo.transform.parent.GetComponent<Enemy>().takeDamage(dmgToDo);
                            //Instantiate(impactEffect, transform.position, transform.rotation);
                        }
                        else if (hitInfo.transform.tag == "EnemyCrit")
                        {
                            hitInfo.transform.parent.GetComponent<Enemy>().takeDamage(dmgToDo * 2f);
                            //hitInfo.transform.parent.parent.GetComponent<Enemy>().takeDamage(dmgToDo * 2f);
                            //Instantiate(impactEffect, transform.position, transform.rotation);
                        }
                        else if (hitInfo.transform.tag == "Mine")
                        {
                            hitInfo.transform.GetComponent<Mine>().explode();
                            //Instantiate(impactEffect, transform.position, transform.rotation);
                        }

                        //}
                    }
                    else
                    {
                        lr[counter].SetPosition(0, point.transform.position);
                        lr[counter].SetPosition(1, camera.transform.forward * 1000 + camera.transform.position);
                        trailTimer = trailTime;
                    }
                    counter++;
                }
                shotTimer = timeBetweenShots;

            }
        }
    }
}
