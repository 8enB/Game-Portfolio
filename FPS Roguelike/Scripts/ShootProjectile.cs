using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Transform camera;

    public float damage;

    public GameObject shootEffect;
    public GameObject hitEffect;
    public GameObject projectileToFire;
    public Transform firePoint;
    public float bulletSpeed;

    private Vector3 shootPoint;

    public float timeBetweenShots;
    private float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer -= Time.deltaTime;
        if (Input.GetMouseButton(1))
        {
            if (shotTimer <= 0)
            {
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    shootPoint = hitInfo.point;
                    //firePoint.transform.rotation = Quaternion.Euler(0, (((Mathf.Atan2(hitInfo.point.x - transform.position.x, hitInfo.point.z - transform.position.z) * 180) / Mathf.PI) ), 0);
                }
                else
                {
                    shootPoint = camera.transform.forward * 50 + camera.transform.position;
                }
                    //target.GetComponent<Health>().hit(damage, hitEffect);
                //Instantiate(shootEffect, firePoint.position, firePoint.rotation);
                GameObject projectile = Instantiate(projectileToFire, firePoint.position, firePoint.rotation);

                projectile.GetComponent<Projectile>().impactEffect = hitEffect;
                projectile.GetComponent<Projectile>().flySpeed = bulletSpeed;
                projectile.GetComponent<Projectile>().aimAt = shootPoint;
                projectile.GetComponent<Projectile>().impactDamage = damage;
                shotTimer = timeBetweenShots;
            }
        }
    }
}
