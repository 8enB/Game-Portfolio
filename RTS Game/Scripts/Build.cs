using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{

    public bool selected;
    public int slotNumber;
    public GameObject[] outlines;
    public GameObject[] structureToBuild;
    public GameObject[] structureBuilding;
    public float[] timeNeeded;
    private float buildTime;
    public Vector3 buildSpot;
    public float[] costs;
    public bool canBuild = false;

    public GameObject buildMenu;

    public float buildRange;

    private bool close;
    private bool selectBuilding;
    private bool startBuild;
    private bool isBuilding;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject structure in structureBuilding)
        {
            structure.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            selectBuilding = false;
            isBuilding = false;
            startBuild = false;
            foreach (GameObject structure in structureBuilding)
            {
                structure.transform.localScale = new Vector3(0f, 0f, 0f);
            }
            close = false;
            foreach (GameObject outline in outlines)
            {
                outline.SetActive(false);
            }
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha1) && !selectBuilding && !isBuilding)
        {
            slotNumber = 0;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha2) && !selectBuilding && !isBuilding)
        {
            slotNumber = 1;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha3) && !selectBuilding && !isBuilding)
        {
            slotNumber = 2;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha4) && !selectBuilding && !isBuilding)
        {
            slotNumber = 3;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha5) && !selectBuilding && !isBuilding)
        {
            slotNumber = 4;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha6) && !selectBuilding && !isBuilding)
        {
            slotNumber = 5;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha7) && !selectBuilding && !isBuilding)
        {
            slotNumber = 6;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha8) && !selectBuilding && !isBuilding)
        {
            slotNumber = 7;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha9) && !selectBuilding && !isBuilding)
        {
            slotNumber = 8;
            selectBuilding = true;
        }
        if (selected && Input.GetKeyDown(KeyCode.Alpha0) && !selectBuilding && !isBuilding)
        {
            slotNumber = 9;
            selectBuilding = true;
        }

        if (transform.tag == "J Unit")
        {
            if (costs[slotNumber] <= PlayerControl.instance.jResource)
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


        outlines[slotNumber].transform.position = buildSpot;
        if (selectBuilding && selected && !isBuilding && !startBuild)
        {
            outlines[slotNumber].SetActive(true);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "moveable")
            {
                outlines[slotNumber].transform.position = hitInfo.point;
            }
            if(Input.GetMouseButtonDown(1))
            {
                buildSpot = hitInfo.point;
                startBuild = true;
            }
        }
        
        else if(!startBuild)
        {
            foreach (GameObject outline in outlines)
            {
                outline.SetActive(false);
            }
            selectBuilding = false;
        }
        /*if (!isBuilding)
        {
            
        }*/

        if (Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(buildSpot.x, buildSpot.z)) < buildRange && startBuild)
        {
            gameObject.GetComponent<Move>().moveToHit = false;
            gameObject.GetComponent<Move>().moving = false;
            structureBuilding[slotNumber].transform.localScale = new Vector3(0f, 0f, 0f);
            close = true;
        }
        else
        {
            close = false;
        }

        if (/*selected && */!isBuilding && startBuild && canBuild)
        {
            
            if (close)
            {
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
            }
            else
            {
                gameObject.GetComponent<Move>().targetPos = buildSpot;
                gameObject.GetComponent<Move>().moveToHit = true;
            }
            
            //unitBuilding = GameObject.Instantiate(unitToBuild, new Vector3(spawnSpot.position.x, spawnSpot.position.y + unitBuilding.GetComponent<Move>().offset, spawnSpot.position.z), moveSpot.rotation);
        }
        if (isBuilding)
        {
            gameObject.GetComponent<Move>().moving = false;
            buildTime -= Time.deltaTime;
            structureBuilding[slotNumber].transform.localScale = new Vector3(1 / (buildTime + 1), 1 / (buildTime + 1), 1 / (buildTime + 1));
            structureBuilding[slotNumber].transform.position = buildSpot;
        }
        if (isBuilding && buildTime <= 0)
        {
            structureBuilding[slotNumber].transform.localScale = new Vector3(0f, 0f, 0f);
            Instantiate(structureToBuild[slotNumber], new Vector3(buildSpot.x, buildSpot.y, buildSpot.z), transform.rotation);
            isBuilding = false;
        }
    }

    public void StartBuild(GameObject toBuild)
    {
                
    }
}
