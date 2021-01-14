using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject core;

    public float speedFade;

    public SpriteRenderer body;

    public GameObject one;
    public GameObject two;
    public GameObject three;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(core == null)
        {
            body.color = new Color(body.color.r, body.color.g, body.color.b, Mathf.MoveTowards(body.color.a, 0f, speedFade/2 * Time.deltaTime));
            UI.instance.fadeSpeed = 0.35f;
            LevelManager.instance.waitToLoad = 3;
            Destroy(one);
            Destroy(two);
            Destroy(three);
            LevelManager.instance.bossDed = true;
        }
    }
}
