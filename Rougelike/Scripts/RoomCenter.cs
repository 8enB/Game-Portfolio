using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{

    public bool openWhenEnemiesCleared;

    public List<GameObject> enemiesList = new List<GameObject>();

    // public GameObject[] enemies;
    // private int enemyCount;

    public GameObject[] onClearActivate;
    public GameObject[] onClearDeActivate;

    public GameObject[] onActiveActivate;
    public GameObject[] onActiveDeactivate;
    public float wait;

    int Spawn = 0;

    public Room theRoom;

    // Start is called before the first frame update
    void Start()
    {
         if(openWhenEnemiesCleared)
         {
             theRoom.closeWhenEntered = true;
         }
        //  foreach(GameObject enemy in enemiesList)
        //         {
        //             enemy.SetActive(true);
        //         }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(theRoom.roomActive)
        {
            wait-=Time.deltaTime;
            if(wait<=0)
            {
            foreach(GameObject activate in onActiveActivate)
            {
                activate.SetActive(true);
            }
            }
        }
        if(!theRoom.roomActive)
        {
            foreach(GameObject activate in onActiveActivate)
            {
                activate.SetActive(false);
            }
            wait=0.75f;
        }
        if(theRoom.roomActive && Spawn==0)
        {
            if(openWhenEnemiesCleared)
            {
                foreach(GameObject enemy in enemiesList)
                {
                    enemy.SetActive(true);
                }
                Spawn++;
            }
        }
        

        if(enemiesList.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for(int i = 0; i < enemiesList.Count; i++)
            {
                if(enemiesList[i] == null)
                {
                    enemiesList.RemoveAt(i);
                    i--;
                }
                
            }
            if(enemiesList.Count==0)
            {
                theRoom.OpenDoors();
             foreach(GameObject toDo in onClearActivate)
                 {
                    toDo.SetActive(true);
                }  
            foreach(GameObject Do in onClearDeActivate)
                {
                    Do.SetActive(false);
                }   
                openWhenEnemiesCleared=false;
                
            }
        }
    }
}
