using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerBullet : MonoBehaviour
{

    public float speed = 10f;
    private Vector3 direction;

    public Rigidbody2D theRB;

    public GameObject impactEffect;

    public int damageGive = 50;

    public int ammoType;

    // Start is called before the first frame update
    void Start()
    {
        
        direction = EnemyTableControl.target.transform.position - transform.position;
        direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        //move forwards relitive to rotation
        theRB.velocity = transform.right*speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && other.tag != "PBullet" && other.tag != "RBullet" && other.tag != "SGBullet")
        {
        other.GetComponent<EnemyTableControl>().DamageEnemy(damageGive, ammoType);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
