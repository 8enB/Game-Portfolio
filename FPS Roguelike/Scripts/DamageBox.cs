using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public float damageToDo;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.transform.parent.GetComponent<Enemy>().takeDamage(damageToDo);
        }
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<PlayerControl>().hit(damageToDo);
        }
        if(other.transform.tag == "Mine")
        {
            other.transform.GetComponent<Mine>().explode();
        }
    }
}
