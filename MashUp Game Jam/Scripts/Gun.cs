using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletToFire;
    public Transform[] firePoint;

    public AudioSource shotSound;

    public float timeBetweenShots;
    private float shotTimer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShots = PlayerControl.instance.shotTime;
        if (PlayerControl.instance.canMove && !LevelManager.instance.isPaused)
        {
            shotTimer -= Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                if (shotTimer <= 0)
                {
                    shotSound.Play();
                    foreach (Transform point in firePoint)
                    {
                        Instantiate(bulletToFire, point.position, point.rotation);
                    }
                    shotTimer = timeBetweenShots;

                }
            }
            
        }
    }
}
