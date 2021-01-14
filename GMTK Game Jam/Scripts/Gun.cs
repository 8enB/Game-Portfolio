using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform[] firePoint;

    public float fireForce;

    public float timeBetweenShots;
    private float shotTimer;

    public Camera playerCamera;
    public float changeSpeed;
    public int cameraSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerCamera.orthographicSize = Mathf.MoveTowards(playerCamera.orthographicSize, cameraSize, changeSpeed * Time.deltaTime);
        if (PlayerControl.instance.canMove && !Manager.instance.isPaused)
        {
            shotTimer -= Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                if (shotTimer <= 0)
                {
                    foreach (Transform point in firePoint)
                    {
                        Instantiate(bulletToFire, point.position, point.rotation);
                    }
                    PlayerControl.instance.theRB.velocity += /*shotDirection*/ PlayerControl.instance.offset.normalized * -fireForce;
                    shotTimer = timeBetweenShots;
                }
            }

        }
    }
}
