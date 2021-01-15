using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform[] firePoints;//position to fire the gun from
    public Transform[] renderPoint;//where to show the gun firing from

    public Transform camera;//camera position

    public float minDmg1;//minimum damage to do (when closer to player)
    public float maxDmg;//maximum damage to do
    public float minDmg2;//minimum damage to do (when farther from player)

    //the ramp up start and end range
    public float rampUpStartRange;
    public float rampUpEndRange;

    //the fall off start and end range
    public float fallOffStartRange;
    public float fallOffEndRange;

    public float maxRange;//the maximum range to do damage

    public Vector3 hitPoint;//the position that has been shot

    //the effects and sounds of shooting
    public GameObject impactEffect;
    public GameObject hitSound;
    public GameObject fireSound;
    public GameObject fireEffect;

    //the guns stats: fire rate, magazine size, ammo remaining, and reload time
    public float timeBetweenShots;
    public float magSize;
    public float ammo;
    public float reloadTime;

    public Animator recoil;//the recoil animation

    //public GameObject gunShoot;

    //timers to keep track of fire rate and reload time
    private float shotTimer;
    private float reloadTimer;

    private float dmgToDo;//the damage to do to the enemy

    private float dis;//the distance to the point hit

    public LineRenderer[] lr;//the line renderer to show the bullet trail/laser


    //how long to show the bullet trail/laser for
    public float trailTime;
    private float trailTimer;

    private int counter;

    public bool animateIt;//if it should animate

    // Start is called before the first frame update
    void Start()
    {
        ammo = magSize;//sets current ammo to full when first entering scene
    }

    // Update is called once per frame
    void Update()
    {
        //set each line rendered to come from the gun
        foreach (LineRenderer lines in lr)
        {
            lines.SetPosition(0, firePoints[0].position);
        }
        //track the time to show the line for
        trailTimer -= Time.deltaTime;
        //if the lines been visable for long enough dissable it
        if(trailTimer <= 0)
        {
            foreach (LineRenderer lines in lr)
            {
                lines.SetPosition(0, gameObject.transform.position);
                lines.SetPosition(1, gameObject.transform.position);
            }
        }

        //track the time for the fire rate and reload
        shotTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;
        //if the player wants to reload and reloading is possible then reload
        if(Input.GetKeyDown(KeyCode.R) && shotTimer <= 0 && reloadTimer <= 0 && ammo < magSize)
        {
            reloadTimer = reloadTime;
            ammo = magSize;
            //play the reload animation
            if (animateIt)
            {
                recoil.Play("ReloadRev", -1, 0);
                recoil.Play("ReloadRev");
            }
        }

        //if the player wants to shoot and shooting is possible then shoot
        if (Input.GetMouseButton(0))
        {
                //if the player tried to shoot and doesn't have ammo then reload
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
            //if possible to shoot then shoot
            if (shotTimer <= 0 && reloadTimer <= 0)
            {
                //animate shooting
                if (animateIt)
                {
                    recoil.Play("Fire", -1, 0);
                    recoil.Play("Fire");
                }
                counter = 0;
                ammo--;

                //Instantiate(fireSound, gameObject.transform.position, gameObject.transform.rotation); //play the firing sound

                //at each fire point
                foreach (Transform point in firePoints)
                {
                    //Instantiate(fireEffect, point.position, point.rotation); //play the firing effect

                    RaycastHit hitInfo = new RaycastHit();//create a raycast

                    //Vector3 fwd = point.transform.TransformDirection(Vector3.forward);

                    //send out a raycast and if it hits an enemy calculate damage
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag != "unHittable" && hitInfo.transform.tag != "Player")
                    {
                        hitPoint = hitInfo.point;//set the hit position
                        //if(hitInfo.transform.tag == "enemy")
                        //{
                        dis = Vector3.Distance(hitPoint, point.position);//set the distance to target
                        //set the line renderer
                        lr[counter].SetPosition(0, point.transform.position);
                        lr[counter].SetPosition(1, hitInfo.point);
                        trailTimer = trailTime;

                        //calculate damage
                        if (dis < rampUpStartRange)//if closer than the ramp up start range
                        {
                            dmgToDo = minDmg1;//set the damage to the min damage
                        }
                        else if (dis > fallOffEndRange)//if farther than the fall off range
                        {
                            dmgToDo = minDmg2;//set the damage to the min damage
                        }
                        else//if neither
                        {
                            dmgToDo = maxDmg;//set the max damage for now
                        }

                        if (dis > rampUpStartRange && dis < rampUpEndRange)//if the distance is within both bounds of the ramp up range
                            {
                                dmgToDo = (maxDmg * ((dis - rampUpStartRange) / (rampUpEndRange - rampUpStartRange)));//calculate the damage to do
                                //don't let the damage be less than the minimum
                                if (dmgToDo < minDmg1)
                                {
                                    dmgToDo = minDmg1;
                                }
                            }

                        if (dis > fallOffStartRange && dis < fallOffEndRange)//if the distance is within both bounds of the fall off range
                        {
                                dmgToDo = maxDmg + (((minDmg2 - maxDmg) / (fallOffEndRange - fallOffStartRange)) * (dis - fallOffStartRange));//calculate the damage to do
                            //dont let the damage be less that the minimum
                            if (dmgToDo < minDmg2)
                                {
                                    dmgToDo = minDmg2;
                                }
                            }
                        //if the player hit an enemy do damage to it and play the impact effect
                        if(hitInfo.transform.tag == "Enemy")
                        {
                            hitInfo.transform.GetComponent<Enemy>().takeDamage(dmgToDo);
                            //hitInfo.transform.parent.GetComponent<Enemy>().takeDamage(dmgToDo);
                            //Instantiate(impactEffect, transform.position, transform.rotation);
                        }
                        //if the player hit the crit box of the enemy do double damage and play the impact effect
                        else if (hitInfo.transform.tag == "EnemyCrit")
                        {
                            hitInfo.transform.parent.GetComponent<Enemy>().takeDamage(dmgToDo * 2f);
                            //hitInfo.transform.parent.parent.GetComponent<Enemy>().takeDamage(dmgToDo * 2f);
                            //Instantiate(impactEffect, transform.position, transform.rotation);
                        }
                        //if the player hit a mine explode the mine and play the impact effect
                        else if (hitInfo.transform.tag == "Mine")
                        {
                            hitInfo.transform.GetComponent<Mine>().explode();
                            //Instantiate(impactEffect, transform.position, transform.rotation);
                        }

                        //}
                    }
                    //if the player hits nothing set the trail to this
                    else
                    {
                        lr[counter].SetPosition(0, point.transform.position);
                        lr[counter].SetPosition(1, camera.transform.forward * 1000 + camera.transform.position);
                        trailTimer = trailTime;
                    }
                    counter++;
                }
                //reset the shot timer
                shotTimer = timeBetweenShots;

            }
        }
    }
}
