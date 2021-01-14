using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSpeedUp : MonoBehaviour
{

    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(sound, transform.position, transform.rotation);
            PlayerControl.instance.shotTime -= 0.025f;
            Destroy(gameObject);
        }
    }
}
