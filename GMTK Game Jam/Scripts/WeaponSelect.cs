using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public static WeaponSelect instance;

    public int slot1 = 0;
    public int slot2 = 1;
    public int slot3 = 2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    { 
    }
    public void Pick1(int pick)
    {
        slot1 = pick;
    }
    public void Pick2(int pick)
    {
        slot2 = pick;
    }
    public void Pick3(int pick)
    {
        slot3 = pick;
    }
}
