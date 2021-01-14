using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject turret;
    public GameObject gun;

    public GameObject target;

    public GameObject owner;

    public float damage;

    public GameObject shootEffect;
    public GameObject hitEffect;
    public GameObject projectileToFire;
    public Transform[] firePoint;

    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotTimer;

    public float range;

    private float targetDistance;
    private Transform hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && owner.GetComponent<Unit>().selected == true)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && (hitInfo.transform.tag == "J Unit"))
            {
                target = hitInfo.transform.gameObject;
                owner.GetComponent<Move>().targetPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                owner.GetComponent<Move>().moveToHit = true;
            }
            else
            {
            }
        }
        if (target != null)
        {
            targetDistance = Vector2.Distance(new Vector2(owner.transform.position.x, owner.transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z));
        }
        if (target != null && targetDistance < range)
        {
            owner.GetComponent<Move>().moveToHit = false;
            turret.transform.rotation = Quaternion.Euler(0, (((Mathf.Atan2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z) * 180) / Mathf.PI) + 90), 0);
            if (shotTimer <= 0)
            {
                foreach (Transform fire in firePoint)
                {
                    //target.GetComponent<Health>().hit(damage, hitEffect);

                    Instantiate(shootEffect, fire.position, fire.rotation);
                    GameObject projectile = Instantiate(projectileToFire, fire.position, fire.rotation);

                    projectile.GetComponent<Projectile>().impactEffect = hitEffect;
                    projectile.GetComponent<Projectile>().flySpeed = bulletSpeed;
                    projectile.GetComponent<Projectile>().aimAt = target;
                    projectile.GetComponent<Projectile>().mySide = owner.tag;
                    projectile.GetComponent<Projectile>().impactDamage = damage;

                }
                shotTimer = timeBetweenShots;

            }
            //gun.transform.rotation = Quaternion.Euler((((Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * 180) / Mathf.PI) + 90), 0, 0);
        }
        else
        {
            turret.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
