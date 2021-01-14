using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform[] firePoint;
    public int gunType;

    public float timeBetweenShots;
    private float shotTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.instance.canMove && !LevelManager.instance.isPaused)
    {
        shotTimer -= Time.deltaTime;
        if(Input.GetMouseButton(0) && gunType==0)
        {
            if(shotTimer<=0)
            {
                foreach (Transform point in firePoint)
                {
                Instantiate(bulletToFire, point.position, point.rotation);
                }
                shotTimer=timeBetweenShots;
                
            }
        }
        if(Input.GetMouseButton(0) && gunType==1 && PlayerControl.instance.shotgunAmmo>0)
        {
            if(shotTimer<=0)
            {
                foreach (Transform point in firePoint)
                {
                Instantiate(bulletToFire, point.position, point.rotation);
                }
                shotTimer=timeBetweenShots;
                PlayerControl.instance.shotgunAmmo--;
                
            }
        }
    if(Input.GetMouseButton(0) && gunType==2 && PlayerControl.instance.rifleAmmo>0)
        {
            if(shotTimer<=0)
            {
                foreach (Transform point in firePoint)
                {
                Instantiate(bulletToFire, point.position, point.rotation);
                }
                shotTimer=timeBetweenShots;
                PlayerControl.instance.rifleAmmo--;
                
            }
        }
    }
  }
}
