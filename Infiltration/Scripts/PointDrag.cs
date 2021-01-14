using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class PointDrag : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public BoxCollider2D bc2;

    public SpriteRenderer spriteRenderer;

    public bool listenToAi;

    public bool locked;

    public bool changed = false;

    public bool loaded = false;

    void start()
    {
        
    }

    void Update()
    {
        if (listenToAi && !loaded)
        {
            if (AiTracking.instance.disableRoute)
            {
                locked = true;
            }
        }
        loaded = true;
        if (locked)
        {
            spriteRenderer.color = new Color(255, 0, 0);
        }
        if(PlayerControl.instance.hacking == false)
        {
            if(!locked)
            {
                spriteRenderer.color = new Color(255, 255, 255);
            }   
            
            bc.enabled = false;
            bc2.enabled = false;
        }
        else
        {
            if (!locked)
            {
                spriteRenderer.color = new Color(0, 255, 255);
            }
            bc2.enabled = true;
        }
    }
        void OnMouseDown()
    {
        
        if (PlayerControl.instance.hacking && !locked)
        {
            if (!changed)
            {
                AiTracking.instance.route++;
                AiTracking.instance.routeTotal++;
                changed = true;
            }

            bc.enabled = true;
            
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (PlayerControl.instance.hacking && !locked)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            /*transform.position = curPosition;*/
            rb.MovePosition(curPosition);
        }
    }

    void OnMouseUp()
    {
        bc.enabled = false;
    }

}
