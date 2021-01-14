using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed;
    public bool moving;
    public bool moveToHit;
    public float offset;
    public BoxCollider checkGoal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != new Vector3(0, 0, 0) && moving)
        {
            transform.rotation = Quaternion.Euler(0, (((Mathf.Atan2(targetPos.x - transform.position.x, targetPos.z - transform.position.z) * 180) / Mathf.PI) + 90), 0);
        }
        if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<Unit>().selected == true)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "moveable")
            {
                targetPos = hitInfo.point;
                moving = true;
            }
        }
        if (moving || moveToHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, targetPos.y + offset, targetPos.z), speed * Time.deltaTime);
            if(checkGoal.bounds.Contains(targetPos))
            {
                moving = false;
            }
        }
    }
}
