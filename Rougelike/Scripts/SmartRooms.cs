using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartRooms : MonoBehaviour
{
    public static SmartRooms instance;

    public bool closeWhenEntered;

    public bool openWhenEnemiesCleared;

    public GameObject doorUp;
    public GameObject doorDown;
    public GameObject doorLeft;
    public GameObject doorRight;

    public bool doorUpClose;
    public bool doorDownClose;
    public bool doorLeftClose;
    public bool doorRightClose;

    public List<GameObject> enemiesList = new List<GameObject>();

    public GameObject[] onClearActivate;
    public GameObject[] onClearDeActivate;

    public GameObject[] onActiveActivate;
    public GameObject[] onActiveDeactivate;

    public GameObject[] upExits;
    public GameObject[] downExits;
    public GameObject[] leftExits;
    public GameObject[] rightExits;

    public GameObject[] sideRoomUp;
    public GameObject[] sideRoomDown;
    public GameObject[] sideRoomLeft;
    public GameObject[] sideRoomRight;


    public bool generateRight = false, generateLeft = false, generateUp = false, generateDown = false;

    public bool beginGenerate = false;

    private int roomSelect;

    public List<GameObject> usedExits = new List<GameObject>();

    public GameObject upBlock;
    public GameObject downBlock;
    public GameObject leftBlock;
    public GameObject rightBlock;
    
    public int yOffset;
    public int xOffset;
    public float wait;

    public bool firstActivate = true;

    int Spawn = 0;
    private float pause = 5f;

    public bool roomActive;

    bool fadeOutBlack = true;
    public SpriteRenderer fadeScreen;

    public float fadeSpeed;

    public int cameraSize = 5;


    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        beginGenerate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(SmartGeneration.instance.finished && beginGenerate)
        {
            if(generateRight)
            {
                roomSelect = Random.Range(0, sideRoomRight.Length);
                Instantiate(sideRoomRight[roomSelect], rightExits[0].transform.position + new Vector3(9f, 0f, 0f), rightExits[0].transform.rotation);
            }
            if(generateLeft)
            {
                roomSelect = Random.Range(0, sideRoomLeft.Length);
                Instantiate(sideRoomLeft[roomSelect], leftExits[0].transform.position - new Vector3(9f, 0f, 0f), leftExits[0].transform.rotation);
            }
            if(generateUp)
            {
                roomSelect = Random.Range(0, sideRoomUp.Length);
                Instantiate(sideRoomUp[roomSelect], upExits[0].transform.position + new Vector3(0f, 5f, 0f), upExits[0].transform.rotation);
            }
            if(generateDown)
            {
                roomSelect = Random.Range(0, sideRoomDown.Length);
                Instantiate(sideRoomDown[roomSelect], downExits[0].transform.position - new Vector3(0f, 5f, 0f), downExits[0].transform.rotation);
            }
            beginGenerate=false;
        }
        if(roomActive)
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
        if(roomActive)
        {
            if(Camera.main.orthographicSize < cameraSize)
            {
                    Camera.main.orthographicSize += 10*Time.deltaTime;
            }
            if(Camera.main.orthographicSize > cameraSize)
            {
                    Camera.main.orthographicSize -= 10*Time.deltaTime;
            }
        }
        if(roomActive)
        {
            foreach(GameObject activate in onActiveDeactivate)
            {
                activate.SetActive(false);
            }
            wait=0.75f;
        }
        if(roomActive && Spawn==0)
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
        if(fadeOutBlack && roomActive)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed*Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }
        
        

        if(enemiesList.Count > 0 && roomActive && openWhenEnemiesCleared)
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
                OpenDoors();
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
    public void OpenDoors()
    {
        roomActive=false;
                if(doorDownClose)
                {
                    doorDown.SetActive(false);
                }
                if(doorUpClose)
                {
                    doorUp.SetActive(false);
                }
                if(doorLeftClose)
                {
                    doorLeft.SetActive(false);
                }
                if(doorRightClose)
                {
                    doorRight.SetActive(false);
                }
        closeWhenEntered=false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive=true;
            //CameraControl.instance.ChangeTarget(transform);

                
            if(closeWhenEntered)
            {
                if(doorDownClose)
                {
                    doorDown.SetActive(true);
                }
                if(doorUpClose)
                {
                    doorUp.SetActive(true);
                }
                if(doorLeftClose)
                {
                    doorLeft.SetActive(true);
                }
                if(doorRightClose)
                {
                    doorRight.SetActive(true);
                }
            }
                
            }
        }
        private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive=false;
            
        }
    }
}
