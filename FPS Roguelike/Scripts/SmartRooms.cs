using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartRooms : MonoBehaviour
{
    public static SmartRooms instance;//make the most recently generated room the instance

    //if the room should close when entered and open when completed
    public bool closeWhenEntered;
    public bool openWhenEnemiesCleared;

    //each door object
    public GameObject doorUp;
    public GameObject doorDown;
    public GameObject doorLeft;
    public GameObject doorRight;

    //if the doors should be closed
    public bool doorUpClose;
    public bool doorDownClose;
    public bool doorLeftClose;
    public bool doorRightClose;

    //every enemy
    public List<GameObject> enemiesList = new List<GameObject>();

    //what to do when cleared
    public GameObject[] onClearActivate;
    public GameObject[] onClearDeActivate;

    //what to do when first entered
    public GameObject[] onActiveActivate;
    public GameObject[] onActiveDeactivate;

    //each posible exit
    public GameObject[] upExits;
    public GameObject[] downExits;
    public GameObject[] leftExits;
    public GameObject[] rightExits;

    //each posible side room
    public GameObject[] sideRoomUp;
    public GameObject[] sideRoomDown;
    public GameObject[] sideRoomLeft;
    public GameObject[] sideRoomRight;

    public bool generateRight = false, generateLeft = false, generateUp = false, generateDown = false;//where to generate a room

    public bool beginGenerate = false;//if it should start generation

    private int roomSelect;//which room is selected

    public List<GameObject> usedExits = new List<GameObject>();//every used exit

    //what to fill empty door holes with
    public GameObject upBlock;
    public GameObject downBlock;
    public GameObject leftBlock;
    public GameObject rightBlock;

    //the offset from the center of the room
    public float yOffset;
    public float xOffset;

    public float wait;//how long to wait

    public bool firstActivate = true;//if this is the first activation of the room


    int Spawn = 0;//number of enemies spawned
    private float pause = 5f;//how long to pause while swapping rooms if needed

    public bool roomActive;//if the room is currently active

    bool fadeOutBlack = true;//if it should fade to black
    public SpriteRenderer fadeScreen;//the image to fade

    public float fadeSpeed;//the speed at which to fade


    public void Awake()
    {
        instance = this;//set this room to the instance
    }

    // Start is called before the first frame update
    void Start()
    {
        beginGenerate = true;//start generation when it first enters the scene
    }

    // Update is called once per frame
    void Update()
    {
        //when the layout generation is done and if this room has been told to generate more rooms
        if (SmartGeneration.instance.finished && beginGenerate)
        {
            //if any direction has been set to generate a side room generate a room there
            if (generateRight)
            {
                roomSelect = Random.Range(0, sideRoomRight.Length);//choose a random room from this directions array
                Instantiate(sideRoomRight[roomSelect], rightExits[0].transform.position + new Vector3(12.5f, 0f, 0f), rightExits[0].transform.rotation);//create that room
            }
            if (generateLeft)
            {
                roomSelect = Random.Range(0, sideRoomLeft.Length);
                Instantiate(sideRoomLeft[roomSelect], leftExits[0].transform.position - new Vector3(12.5f, 0f, 0f), leftExits[0].transform.rotation);
            }
            if (generateUp)
            {
                roomSelect = Random.Range(0, sideRoomUp.Length);
                Instantiate(sideRoomUp[roomSelect], upExits[0].transform.position + new Vector3(0f, 0f, 12.5f), upExits[0].transform.rotation);
            }
            if (generateDown)
            {
                roomSelect = Random.Range(0, sideRoomDown.Length);
                Instantiate(sideRoomDown[roomSelect], downExits[0].transform.position - new Vector3(0f, 0f, 12.5f), downExits[0].transform.rotation);
            }
            beginGenerate = false;//stop generating
        }

        //used in the 2D version
        /*if (roomActive)
        {
            wait -= Time.deltaTime;
            if (wait <= 0)
            {
                foreach (GameObject activate in onActiveActivate)
                {
                    activate.SetActive(true);
                }
            }
        }*/
        if (roomActive)
        {
            /*if (Camera.main.orthographicSize < cameraSize)
            {
                Camera.main.orthographicSize += 10 * Time.deltaTime;
            }
            if (Camera.main.orthographicSize > cameraSize)
            {
                Camera.main.orthographicSize -= 10 * Time.deltaTime;
            }*/
        }

        //if the room is active
        if (roomActive)
        {
            //set every object in on activate deactivate to false
            foreach (GameObject activate in onActiveDeactivate)
            {
                activate.SetActive(false);
            }
            wait = 0.75f;
        }
        //if the room is active and the current amount of enemies spawned is 0
        if (roomActive && Spawn == 0)
        {
            //if it should open when there are no more enemies
            if (openWhenEnemiesCleared)
            {
                //activate each enemy in the room
                foreach (GameObject enemy in enemiesList)
                {
                    enemy.SetActive(true);
                }
                Spawn++;
            }
        }
        /*if (fadeOutBlack && roomActive)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }*/


        //if there are enemies left in the room
        if (enemiesList.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            //go through each enemy in the list and check if it still exists, if it has died remove it from the list
            for (int i = 0; i < enemiesList.Count; i++)
            {
                if (enemiesList[i] == null)
                {
                    enemiesList.RemoveAt(i);
                    i--;
                }

            }

            //if there are no enemies left in the room open the doors
            if (enemiesList.Count == 0)
            {
                OpenDoors();
                //activate or deactivate everything that should be activated/deactivated when the enemies are defeated
                foreach (GameObject toDo in onClearActivate)
                {
                    toDo.SetActive(true);
                }
                foreach (GameObject Do in onClearDeActivate)
                {
                    Do.SetActive(false);
                }
                openWhenEnemiesCleared = false;

            }
        }
    }

    //this is the function for opening the doors
    public void OpenDoors()
    {
        roomActive = false;//deactivate the room

        //open any closed doors
        if (doorDownClose)
        {
            doorDown.SetActive(false);
        }
        if (doorUpClose)
        {
            doorUp.SetActive(false);
        }
        if (doorLeftClose)
        {
            doorLeft.SetActive(false);
        }
        if (doorRightClose)
        {
            doorRight.SetActive(false);
        }
        closeWhenEntered = false;
    }

    //this function is called when a player enters the room
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")//check that the player activated it
        {
            roomActive = true;//set the room to active
            //CameraControl.instance.ChangeTarget(transform);

            //close all doors 
            if (closeWhenEntered)
            {
                if (doorDownClose)
                {
                    doorDown.SetActive(true);
                }
                if (doorUpClose)
                {
                    doorUp.SetActive(true);
                }
                if (doorLeftClose)
                {
                    doorLeft.SetActive(true);
                }
                if (doorRightClose)
                {
                    doorRight.SetActive(true);
                }
            }

        }
    }

    //called when the player exits the room
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //roomActive = false;

        }
    }
}
