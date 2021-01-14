using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;

    public int highScore;

    public bool newHighScore;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.instance.currentPoints>highScore)
        {
            highScore = PlayerControl.instance.currentPoints;
            newHighScore = true;
        }
        else
        {
            newHighScore = false;
        }
    }
}
