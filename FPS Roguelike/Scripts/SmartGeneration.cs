using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartGeneration : MonoBehaviour
{
    //public GameObject layoutRoom;
    //public Color startColor, endColor;
    public static SmartGeneration instance;//mark this generator as the instance

    public int distanceToEnd;//shortest distance to the end room
    public int totalRooms;//total rooms to generate

    public Transform generatorPoint;//the position to generate the room

    //the possible, selected direction, and last direction
    public enum Direction { up, down, right, left };
    public int selectedAxis;
    public Direction selectedDirection;
    public Direction selectedDirectionX;
    public Direction selectedDirectionY;
    public Direction lastDirection;

    // public float xOffset; 
    // public float yOffset;

    public LayerMask whatIsRoom;//what layer the rooms on

    private List<GameObject> layoutRoomObjects = new List<GameObject>();//list of every room

    //exits
    public GameObject[] currentExitsUp;
    public GameObject[] currentExitsRight;
    public GameObject[] currentExitsDown;
    public GameObject[] currentExitsLeft;
    public int selectedExit;

    private GameObject newRoom;//the newest room

    //unused for now
    // public RoomPrefabsUp roomsUp;
    // public RoomPrefabsDown roomsDown;
    // public RoomPrefabsLeft roomsLeft;
    // public RoomPrefabsRight roomsRight;

    //private List<GameObject> generatedOutlines = new List<GameObject>();

    public GameObject startRoom;//the starting room

    //arrays of possible boss rooms
    public GameObject[] bossRoomsUp;
    public GameObject[] bossRoomsDown;
    public GameObject[] bossRoomsLeft;
    public GameObject[] bossRoomsRight;

    //arrays of possible rooms
    public GameObject[] potentialRoomsUp;
    public GameObject[] potentialRoomsDown;
    public GameObject[] potentialRoomsLeft;
    public GameObject[] potentialRoomsRight;

    //arrays of possible corridoors
    public GameObject corridoorRight;
    public GameObject corridoorLeft;
    public GameObject corridoorUp;
    public GameObject corridoorDown;

    private int distanceSinceSide = 1;//how long it's been since side rooms were last generated

    public bool finished = false;//if generation is over

    private int roomSelect;//selected room

    public void Awake()
    {
        instance = this;//set this to the instance
    }

    // Start is called before the first frame update
    void Start()
    {
        newRoom = Instantiate(startRoom, generatorPoint.position, generatorPoint.rotation);//set the first room to the start room

        //generate the first room
        selectedDirectionX = (Direction)Random.Range(2, 4);//select a horizontal direction
        selectedDirectionY = (Direction)Random.Range(0, 2);//select a vertical direction
        selectedAxis = Random.Range(0, 2);//choose one of the selected directions

        //determain what direction is selected
        if (selectedAxis == 0)
        {
            selectedDirection = selectedDirectionX;
        }
        if (selectedAxis == 1)
        {
            selectedDirection = selectedDirectionY;
        }
        //depending on what direction is selected create an exit hole in the previous room
        if (selectedDirection == Direction.up)
        {
            SmartRooms.instance.upBlock.SetActive(false);
        }
        if (selectedDirection == Direction.down)
        {
            SmartRooms.instance.downBlock.SetActive(false);
        }
        if (selectedDirection == Direction.left)
        {
            SmartRooms.instance.leftBlock.SetActive(false);
        }
        if (selectedDirection == Direction.right)
        {
            SmartRooms.instance.rightBlock.SetActive(false);
        }

        //lastDirection = selectedDirection;

        MoveGenerationPoint();//move the position to make the room

        //generate the remaining rooms
        for (int i = 0; i < distanceToEnd; i++)
        {
            if (i + 1 != distanceToEnd)
            {
                if (selectedDirection == Direction.up)
                {
                    if (lastDirection == selectedDirection && distanceSinceSide > 1)
                    {
                        distanceSinceSide = 0;
                        SmartRooms.instance.generateLeft = true;
                        SmartRooms.instance.doorLeftClose = true;
                        SmartRooms.instance.doorRightClose = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.leftExits[0]);
                        SmartRooms.instance.leftBlock.SetActive(false);
                        SmartRooms.instance.generateRight = true;
                        // SmartRooms.instance.usedExits.Add(SmartRooms.instance.rightExits[0]);
                        SmartRooms.instance.rightBlock.SetActive(false);

                    }
                    else
                    {
                        distanceSinceSide++;
                    }
                    roomSelect = Random.Range(0, potentialRoomsUp.Length);
                    /*newRoom = Instantiate(corridoorUp, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(potentialRoomsUp[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                if (selectedDirection == Direction.down)
                {
                    if (lastDirection == selectedDirection && distanceSinceSide > 1)
                    {
                        distanceSinceSide = 0;
                        SmartRooms.instance.generateLeft = true;
                        SmartRooms.instance.doorLeftClose = true;
                        SmartRooms.instance.doorRightClose = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.leftExits[0]);
                        SmartRooms.instance.leftBlock.SetActive(false);
                        SmartRooms.instance.generateRight = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.rightExits[0]);
                        SmartRooms.instance.rightBlock.SetActive(false);
                    }
                    else
                    {
                        distanceSinceSide++;
                    }
                    roomSelect = Random.Range(0, potentialRoomsDown.Length);
                    /*newRoom = Instantiate(corridoorDown, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(potentialRoomsDown[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                if (selectedDirection == Direction.left)
                {
                    if (lastDirection == selectedDirection && distanceSinceSide > 1)
                    {
                        distanceSinceSide = 0;
                        SmartRooms.instance.generateUp = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.upExits[0]);
                        SmartRooms.instance.upBlock.SetActive(false);
                        SmartRooms.instance.doorUpClose = true;
                        SmartRooms.instance.doorDownClose = true;
                        SmartRooms.instance.generateDown = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.downExits[0]);
                        SmartRooms.instance.downBlock.SetActive(false);
                    }
                    else
                    {
                        distanceSinceSide++;
                    }
                    roomSelect = Random.Range(0, potentialRoomsLeft.Length);
                    /*newRoom = Instantiate(corridoorLeft, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(potentialRoomsLeft[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                if (selectedDirection == Direction.right)
                {
                    if (lastDirection == selectedDirection && distanceSinceSide > 1)
                    {
                        distanceSinceSide = 0;
                        SmartRooms.instance.generateUp = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.upExits[0]);
                        SmartRooms.instance.upBlock.SetActive(false);
                        SmartRooms.instance.generateDown = true;
                        SmartRooms.instance.doorUpClose = true;
                        SmartRooms.instance.doorDownClose = true;
                        //SmartRooms.instance.usedExits.Add(SmartRooms.instance.downExits[0]);
                        SmartRooms.instance.downBlock.SetActive(false);
                    }
                    else
                    {
                        distanceSinceSide++;
                    }
                    roomSelect = Random.Range(0, potentialRoomsRight.Length);
                    /*newRoom = Instantiate(corridoorRight, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(potentialRoomsRight[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                //GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
                //newRoom.GetComponent<SpriteRenderer>().color = startColor;

                layoutRoomObjects.Add(newRoom);
            }
            else
            {
                if (selectedDirection == Direction.up)
                {
                    roomSelect = Random.Range(0, bossRoomsUp.Length);
                    /*newRoom = Instantiate(corridoorUp, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(bossRoomsUp[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                if (selectedDirection == Direction.down)
                {
                    roomSelect = Random.Range(0, bossRoomsDown.Length);
                    /*newRoom = Instantiate(corridoorDown, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(bossRoomsDown[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                if (selectedDirection == Direction.left)
                {
                    roomSelect = Random.Range(0, bossRoomsLeft.Length);
                    /*newRoom = Instantiate(corridoorLeft, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(bossRoomsLeft[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                if (selectedDirection == Direction.right)
                {
                    roomSelect = Random.Range(0, bossRoomsRight.Length);
                    /*newRoom = Instantiate(corridoorRight, generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);*/
                    newRoom = Instantiate(bossRoomsRight[roomSelect], generatorPoint.position, generatorPoint.rotation);
                    newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, 0f, SmartRooms.instance.yOffset);
                }
                layoutRoomObjects.Add(newRoom);

            }

            //unused for now
            // if(i+1==distanceToEnd)
            // {
            //      //    newRoom.GetComponent<SpriteRenderer>().color = endColor;
            //          layoutRoomObjects.RemoveAt(layoutRoomObjects.Count-1);
            //         roomSelect = Random.Range(0, bossRooms.Length);
            //         newRoom = Instantiate(bossRooms[roomSelect], generatorPoint.position, generatorPoint.rotation);
            //         layoutRoomObjects.Add(newRoom);
            //     }

            //selectedDirectionX = (Direction)Random.Range(2,4);
            //selectedDirectionY = (Direction)Random.Range(0,2);


            lastDirection = selectedDirection;
            selectedAxis = Random.Range(0, 2);
            if (selectedAxis == 0)
            {
                selectedDirection = selectedDirectionX;
            }
            if (selectedAxis == 1)
            {
                selectedDirection = selectedDirectionY;
            }

            if (lastDirection == Direction.up)
            {
                SmartRooms.instance.downBlock.SetActive(false);
                SmartRooms.instance.doorDownClose = true;
            }
            if (lastDirection == Direction.down)
            {
                SmartRooms.instance.upBlock.SetActive(false);
                SmartRooms.instance.doorUpClose = true;
            }
            if (lastDirection == Direction.left)
            {
                SmartRooms.instance.rightBlock.SetActive(false);
                SmartRooms.instance.doorRightClose = true;
            }
            if (lastDirection == Direction.right)
            {
                SmartRooms.instance.leftBlock.SetActive(false);
                SmartRooms.instance.doorLeftClose = true;
            }

            if (selectedDirection == Direction.up && i + 1 != distanceToEnd)
            {
                SmartRooms.instance.upBlock.SetActive(false);
                SmartRooms.instance.doorUpClose = true;
            }
            if (selectedDirection == Direction.down && i + 1 != distanceToEnd)
            {
                SmartRooms.instance.downBlock.SetActive(false);
                SmartRooms.instance.doorDownClose = true;
            }
            if (selectedDirection == Direction.left && i + 1 != distanceToEnd)
            {
                SmartRooms.instance.leftBlock.SetActive(false);
                SmartRooms.instance.doorLeftClose = true;
            }
            if (selectedDirection == Direction.right && i + 1 != distanceToEnd)
            {
                SmartRooms.instance.rightBlock.SetActive(false);
                SmartRooms.instance.doorRightClose = true;
            }


            MoveGenerationPoint();

            // while(Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
            // {
            //     MoveGenerationPoint();
            // }
        }
        finished = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveGenerationPoint()
    {
        currentExitsUp = SmartRooms.instance.upExits;
        currentExitsDown = SmartRooms.instance.downExits;
        currentExitsLeft = SmartRooms.instance.leftExits;
        currentExitsRight = SmartRooms.instance.rightExits;

        switch (selectedDirection)
        {
            case Direction.up:
                selectedExit = Random.Range(0, currentExitsRight.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.upExits[selectedExit]);
                generatorPoint.position = (currentExitsUp[selectedExit].transform.position);
                break;
            case Direction.down:
                selectedExit = Random.Range(0, currentExitsRight.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.downExits[selectedExit]);
                generatorPoint.position = (currentExitsDown[selectedExit].transform.position);
                break;
            case Direction.right:
                selectedExit = Random.Range(0, currentExitsRight.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.rightExits[selectedExit]);
                generatorPoint.position = (currentExitsRight[selectedExit].transform.position);
                break;
            case Direction.left:
                selectedExit = Random.Range(0, currentExitsLeft.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.leftExits[selectedExit]);
                generatorPoint.position = (currentExitsLeft[selectedExit].transform.position);
                break;
        }
    }
}
