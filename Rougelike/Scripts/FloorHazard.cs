using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().DamagePlayer(1);
        }
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyTableControl>().DamageEnemy(150, 0);
        }
    }


}
