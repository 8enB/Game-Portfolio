﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartGeneration : MonoBehaviour
{
    //public GameObject layoutRoom;
    //public Color startColor, endColor;
    public static SmartGeneration instance;

    public int distanceToEnd;
    public int totalRooms;

    public Transform generatorPoint;

    public enum Direction {up,down,right,left};
    public int selectedAxis;
    public Direction selectedDirection;
    public Direction selectedDirectionX;
    public Direction selectedDirectionY;
    public Direction lastDirection;

    // public float xOffset; 
    // public float yOffset;
    
    public LayerMask whatIsRoom;

    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    public GameObject[] currentExitsUp;
    public GameObject[] currentExitsRight;
    public GameObject[] currentExitsDown;
    public GameObject[] currentExitsLeft;
    public int selectedExit;

    private GameObject newRoom;

//unused
    // public RoomPrefabsUp roomsUp;
    // public RoomPrefabsDown roomsDown;
    // public RoomPrefabsLeft roomsLeft;
    // public RoomPrefabsRight roomsRight;

    //private List<GameObject> generatedOutlines = new List<GameObject>();

    public GameObject startRoom;

    public GameObject[] bossRoomsUp;
    public GameObject[] bossRoomsDown;
    public GameObject[] bossRoomsLeft;
    public GameObject[] bossRoomsRight;

    public GameObject[] potentialRoomsUp;
    public GameObject[] potentialRoomsDown;
    public GameObject[] potentialRoomsLeft;
    public GameObject[] potentialRoomsRight;

    public GameObject corridoorRight;
    public GameObject corridoorLeft;
    public GameObject corridoorUp;
    public GameObject corridoorDown;

    private int distanceSinceSide = 1;

    public bool finished = false;

    private int roomSelect;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        newRoom = Instantiate(startRoom, generatorPoint.position, generatorPoint.rotation);
        

        selectedDirectionX = (Direction)Random.Range(2,4);
        selectedDirectionY = (Direction)Random.Range(0,2);
        selectedAxis = Random.Range(0,2);
        if(selectedAxis == 0)
        {
            selectedDirection = selectedDirectionX;
        }
        if(selectedAxis == 1)
        {
            selectedDirection = selectedDirectionY;
        }
        if(selectedDirection == Direction.up)
            {
            SmartRooms.instance.upBlock.SetActive(false);
            }
            if(selectedDirection == Direction.down)
            {
            SmartRooms.instance.downBlock.SetActive(false);
            }
            if(selectedDirection == Direction.left)
            {
            SmartRooms.instance.leftBlock.SetActive(false);
            }
            if(selectedDirection == Direction.right)
            {
            SmartRooms.instance.rightBlock.SetActive(false);
            }
        //lastDirection = selectedDirection;
        
        MoveGenerationPoint();

        for(int i = 0; i < distanceToEnd; i++)
        {
            if(i+1!=distanceToEnd)
        {
            if(selectedDirection == Direction.up)
            {
                if(lastDirection == selectedDirection && distanceSinceSide>1)
                {
                    distanceSinceSide = 0;
                    SmartRooms.instance.generateLeft=true;
                    SmartRooms.instance.doorLeftClose = true;
                    SmartRooms.instance.doorRightClose = true;
                    //SmartRooms.instance.usedExits.Add(SmartRooms.instance.leftExits[0]);
                    SmartRooms.instance.leftBlock.SetActive(false);
                    SmartRooms.instance.generateRight=true;
                   // SmartRooms.instance.usedExits.Add(SmartRooms.instance.rightExits[0]);
                    SmartRooms.instance.rightBlock.SetActive(false);
                    
                }
                else
                {
                    distanceSinceSide++;
                }
                roomSelect = Random.Range(0, potentialRoomsUp.Length);
                newRoom = Instantiate(corridoorUp, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(potentialRoomsUp[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            if(selectedDirection == Direction.down)
            {
                if(lastDirection == selectedDirection && distanceSinceSide>1)
                {
                    distanceSinceSide = 0;
                    SmartRooms.instance.generateLeft=true;
                    SmartRooms.instance.doorLeftClose = true;
                    SmartRooms.instance.doorRightClose = true;
                    //SmartRooms.instance.usedExits.Add(SmartRooms.instance.leftExits[0]);
                    SmartRooms.instance.leftBlock.SetActive(false);
                    SmartRooms.instance.generateRight=true;
                    //SmartRooms.instance.usedExits.Add(SmartRooms.instance.rightExits[0]);
                    SmartRooms.instance.rightBlock.SetActive(false);
                }
                else
                {
                    distanceSinceSide++;
                }
                roomSelect = Random.Range(0, potentialRoomsDown.Length);
                newRoom = Instantiate(corridoorDown, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(potentialRoomsDown[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            if(selectedDirection == Direction.left)
            {
                if(lastDirection == selectedDirection && distanceSinceSide>1)
                {
                    distanceSinceSide = 0;
                    SmartRooms.instance.generateUp=true;
                    //SmartRooms.instance.usedExits.Add(SmartRooms.instance.upExits[0]);
                    SmartRooms.instance.upBlock.SetActive(false);
                    SmartRooms.instance.doorUpClose = true;
                    SmartRooms.instance.doorDownClose = true;
                    SmartRooms.instance.generateDown=true;
                   //SmartRooms.instance.usedExits.Add(SmartRooms.instance.downExits[0]);
                    SmartRooms.instance.downBlock.SetActive(false);
                }
                else
                {
                    distanceSinceSide++;
                }
                roomSelect = Random.Range(0, potentialRoomsLeft.Length);
                newRoom = Instantiate(corridoorLeft, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(potentialRoomsLeft[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            if(selectedDirection == Direction.right)
            {
                if(lastDirection == selectedDirection && distanceSinceSide>1)
                {
                    distanceSinceSide = 0;
                    SmartRooms.instance.generateUp=true;
                    //SmartRooms.instance.usedExits.Add(SmartRooms.instance.upExits[0]);
                    SmartRooms.instance.upBlock.SetActive(false);
                    SmartRooms.instance.generateDown=true;
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
                newRoom = Instantiate(corridoorRight, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(potentialRoomsRight[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            //GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            //newRoom.GetComponent<SpriteRenderer>().color = startColor;

            layoutRoomObjects.Add(newRoom);
        }
        else
        {
            if(selectedDirection == Direction.up)
            {
                roomSelect = Random.Range(0, bossRoomsUp.Length);
                newRoom = Instantiate(corridoorUp, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(bossRoomsUp[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            if(selectedDirection == Direction.down)
            {
                roomSelect = Random.Range(0, bossRoomsDown.Length);
                newRoom = Instantiate(corridoorDown, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(bossRoomsDown[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            if(selectedDirection == Direction.left)
            {
                roomSelect = Random.Range(0, bossRoomsLeft.Length);
                newRoom = Instantiate(corridoorLeft, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(bossRoomsLeft[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            if(selectedDirection == Direction.right)
            {
                roomSelect = Random.Range(0, bossRoomsRight.Length);
                newRoom = Instantiate(corridoorRight, generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
                newRoom = Instantiate(bossRoomsRight[roomSelect], generatorPoint.position, generatorPoint.rotation);
                newRoom.transform.position += new Vector3(SmartRooms.instance.xOffset, SmartRooms.instance.yOffset, 0f);
            }
            layoutRoomObjects.Add(newRoom);
            
        }
        
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
            selectedAxis = Random.Range(0,2);
            if(selectedAxis == 0)
            {
            selectedDirection = selectedDirectionX;
            }
            if(selectedAxis == 1)
            {
            selectedDirection = selectedDirectionY;
            }

            if(lastDirection == Direction.up)
            {
            SmartRooms.instance.downBlock.SetActive(false);
            SmartRooms.instance.doorDownClose = true;
            }
            if(lastDirection == Direction.down)
            {
            SmartRooms.instance.upBlock.SetActive(false);
            SmartRooms.instance.doorUpClose = true;
            }
            if(lastDirection == Direction.left)
            {
            SmartRooms.instance.rightBlock.SetActive(false);
            SmartRooms.instance.doorRightClose = true;
            }
            if(lastDirection == Direction.right)
            {
            SmartRooms.instance.leftBlock.SetActive(false);
            SmartRooms.instance.doorLeftClose = true;
            }

            if(selectedDirection == Direction.up && i+1!=distanceToEnd)
            {
            SmartRooms.instance.upBlock.SetActive(false);
            SmartRooms.instance.doorUpClose = true;
            }
            if(selectedDirection == Direction.down && i+1!=distanceToEnd)
            {
            SmartRooms.instance.downBlock.SetActive(false);
            SmartRooms.instance.doorDownClose = true;
            }
            if(selectedDirection == Direction.left && i+1!=distanceToEnd)
            {
            SmartRooms.instance.leftBlock.SetActive(false);
            SmartRooms.instance.doorLeftClose = true;
            }
            if(selectedDirection == Direction.right && i+1!=distanceToEnd)
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

        switch(selectedDirection)
        {
            case Direction.up:
                selectedExit = Random.Range(0, currentExitsRight.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.upExits[selectedExit]);
                generatorPoint.position =(currentExitsUp[selectedExit].transform.position);
                break;
            case Direction.down:
                selectedExit = Random.Range(0, currentExitsRight.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.downExits[selectedExit]);
                generatorPoint.position =(currentExitsDown[selectedExit].transform.position);
                break;
            case Direction.right:
                selectedExit = Random.Range(0, currentExitsRight.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.rightExits[selectedExit]);
                generatorPoint.position =(currentExitsRight[selectedExit].transform.position);
                break;
            case Direction.left:
                selectedExit = Random.Range(0, currentExitsLeft.Length);
                //SmartRooms.instance.usedExits.Add(SmartRooms.instance.leftExits[selectedExit]);
                generatorPoint.position =(currentExitsLeft[selectedExit].transform.position);
                break;
        }
    }
    // public void CreateRoomOutline(Vector3 roomPosition)
    // {
    //     bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), 0.2f, whatIsRoom);
    //     bool roomBelow = Physics2D.OverlapCircle(roomPosition - new Vector3(0f, yOffset, 0f), 0.2f, whatIsRoom);
    //     bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), 0.2f, whatIsRoom);
    //     bool roomLeft = Physics2D.OverlapCircle(roomPosition - new Vector3(xOffset, 0f, 0f), 0.2f, whatIsRoom);

    //     int directionCount = 0;

    //     if(roomAbove)
    //     {
    //         directionCount++;
    //     }
    //     if(roomBelow)
    //     {
    //         directionCount++;
    //     }
    //     if(roomRight)
    //     {
    //         directionCount++;
    //     }
    //     if(roomLeft)
    //     {
    //         directionCount++;
    //     }

    //     switch(directionCount)
    //     {
    //         case 0:
    //             Debug.LogError("Found no room exists!");
    //         break;
    //         case 1:
    //             if(roomAbove)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
    //             }
    //             if(roomBelow)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
    //             }
    //             if(roomLeft)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
    //             }
    //             if(roomRight)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
    //             }
    //             break;
    //         case 2:
    //             if(roomAbove && roomBelow)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
    //             }
    //             if(roomLeft && roomRight)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
    //             }
    //             if(roomAbove && roomRight)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
    //             }
    //             if(roomRight && roomBelow)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
    //             }
    //             if(roomBelow && roomLeft)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
    //             }
    //             if(roomLeft && roomAbove)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
    //             }
    //             break;
    //         case 3:
    //             if(roomAbove && roomRight && roomBelow)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
    //             }
    //             if(roomBelow && roomRight && roomLeft)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
    //             }
    //             if(roomLeft && roomBelow && roomAbove)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
    //             }
    //             if(roomRight && roomLeft && roomAbove)
    //             {
    //                generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
    //             }
    //             break;
    //         case 4:
    //             generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, transform.rotation));
    //             break;
    //     }

    //}
}

// [System.Serializable]
// public class RoomPrefabsUp
// {
//     public GameObject singleUp, singleDown, singleRight, singleLeft,
//     doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp, 
//     tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
//     fourway;
// }
// public class RoomPrefabsDown
// {
//     public GameObject singleUp, singleDown, singleRight, singleLeft,
//     doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp, 
//     tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
//     fourway;
// }
// public class RoomPrefabsLeft
// {
//     public GameObject singleUp, singleDown, singleRight, singleLeft,
//     doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp, 
//     tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
//     fourway;
// }
// public class RoomPrefabsRight
// {
//     public GameObject singleUp, singleDown, singleRight, singleLeft,
//     doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp, 
//     tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
//     fourway;
// }
