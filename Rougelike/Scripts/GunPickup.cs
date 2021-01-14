using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public int gunType;
    public int gun;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerControl.instance.guns[gunType] = PlayerControl.instance.allGuns[gun];
            Destroy(gameObject);
        }
    }
}
