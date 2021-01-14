using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Factory : MonoBehaviour
{
    public bool selected;
    public int slotNumber;
    public GameObject[] unitToBuild;
    public GameObject[] unitBuilding;
    public float[] timeNeeded;
    private float buildTime;
    public Transform spawnSpot;
    public Transform moveSpot;
    public float[] costs;
    public bool canBuild = false;

    public GameObject factoryMenu;

    private bool startBuild;
    private bool isBuilding;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject unit in unitBuilding)
        {
            unit.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(selected && Input.GetKeyDown(KeyCode.Alpha1) && !isBuilding)
        {
            slotNumber = 0;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha2) && !isBuilding)
        {
            slotNumber = 1;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha3) && !isBuilding)
        {
            slotNumber = 2;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha4) && !isBuilding)
        {
            slotNumber = 3;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha5) && !isBuilding)
        {
            slotNumber = 4;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha6) && !isBuilding)
        {
            slotNumber = 5;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha7) && !isBuilding)
        {
            slotNumber = 6;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha8) && !isBuilding)
        {
            slotNumber = 7;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha9) && !isBuilding)
        {
            slotNumber = 8;
            startBuild = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha0) && !isBuilding)
        {
            slotNumber = 9;
            startBuild = true;
        }
        if (transform.tag == "J Unit")
        {
            if(costs[slotNumber] <= PlayerControl.instance.jResource)
            {
                canBuild = true;
            }
            else
            {
                canBuild = false;
            }
        }
        if (transform.tag == "USA Unit")
        {
            if (costs[slotNumber] <= PlayerControl.instance.usaResource)
            {
                canBuild = true;
            }
            else
            {
                canBuild = false;
            }
        }
        if(selected)
        {
            factoryMenu.SetActive(true);
        }
        else
        {
            factoryMenu.SetActive(false);
        }
        buildTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && selected)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "moveable")
            {
                moveSpot.position = hitInfo.point;
                
            }
        }
        if (selected && !isBuilding && /*Input.GetKeyDown(KeyCode.Alpha1)*/startBuild && canBuild)
        {
            //build();
            isBuilding = true;
            startBuild = false;
            buildTime = timeNeeded[slotNumber];
            if (transform.tag == "J Unit")
            {
                PlayerControl.instance.jResource -= costs[slotNumber];
            }
            if (transform.tag == "USA Unit")
            {
                PlayerControl.instance.usaResource -= costs[slotNumber];
            }
            
            //unitBuilding = GameObject.Instantiate(unitToBuild, new Vector3(spawnSpot.position.x, spawnSpot.position.y + unitBuilding.GetComponent<Move>().offset, spawnSpot.position.z), moveSpot.rotation);
        }
        if(isBuilding)
        {
            unitBuilding[slotNumber].transform.localScale = new Vector3(1 / (buildTime + 1), 1 / (buildTime + 1), 1 / (buildTime + 1));
        }
        if(isBuilding && buildTime <= 0)
        {
            unitBuilding[slotNumber].transform.localScale = new Vector3(0f, 0f, 0f);
            GameObject built = Instantiate(unitToBuild[slotNumber], new Vector3(spawnSpot.position.x, spawnSpot.position.y + unitToBuild[slotNumber].GetComponent<Move>().offset, spawnSpot.position.z), spawnSpot.rotation);
            built.GetComponent<Move>().targetPos = new Vector3(moveSpot.position.x, moveSpot.position.y, moveSpot.position.z);
            built.GetComponent<Move>().moving = true;
            isBuilding = false;
        }
    }

    /*public void build(int which)
    {
        slotNumber = which;
        startBuild = true;
        *//*isBuilding = true;
        buildTime = unitToBuild[slotNumber].GetComponent<Health>().currentHealth;
        unitBuilding[slotNumber] = Instantiate(unitToBuild[slotNumber], new Vector3(spawnSpot.position.x, spawnSpot.position.y + unitBuilding[slotNumber].GetComponent<Move>().offset, spawnSpot.position.z), moveSpot.rotation);*//*
    }*/
}
