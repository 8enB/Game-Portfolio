using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject outline;
    public GameObject parent;
    public bool unit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.gameObject == this.transform.gameObject)
            {
                outline.SetActive(true);
                if (unit)
                {
                    parent.GetComponent<Unit>().selected = true;
                    parent.GetComponent<Build>().selected = true;
                }
                else
                {
                    parent.GetComponent<Factory>().selected = true;
                }
            }
            else if(!Input.GetKey(KeyCode.LeftControl))
            {
                outline.SetActive(false);
                if (unit)
                {
                    parent.GetComponent<Unit>().selected = false;
                    parent.GetComponent<Build>().selected = false;
                }
                else
                {
                    parent.GetComponent<Factory>().selected = false;
                }
            }
        }
    }
}
