using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public CharacterController controller;
    Vector3 velocity;
    public float speed = 12f;
    public float jumpPower = 1f;
    public float gravity = -4f;

    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundMask;
    bool isGrounded;

    public int camera = 3;
    public Transform player;

    public GameObject body;


    public GameObject cameraThree;
    public GameObject cameraTwo;
    public Transform cameraTwoCurrentPos;
    public float camera2OGPos = 100f;

    private Vector3 pointAbove;
    private Vector3 pointBelow;

    public float camera2Size;

    public GameObject[] twoActivate;

    public GameObject[] twoDeactivate;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //3D Mode
        if (camera == 3)
        {


            Cursor.lockState = CursorLockMode.Locked;
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, groundMask);

            pointAbove = hit.point;

            cameraTwo.transform.position = pointAbove;
            //Movement
            if (1 == 1)
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }


                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = (transform.right * x + transform.forward * z);

                controller.Move(move * speed * Time.deltaTime);

                if (isGrounded && Input.GetButtonDown("Jump"))
                {
                    velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
                }

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);
            }
            //Perspective Swap
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //controller = GetComponent<CharacterController>();
                cameraThree.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                body.SetActive(false);
                controller.height = 0f;
                controller.radius = 0f;

                cameraThree.SetActive(false);
                cameraTwo.SetActive(true);

                Vector3 move = (transform.up * ((cameraTwo.transform.position.y - 0.5f) - transform.position.y));
                controller.Move(move);

                move = (transform.up * -1);
                controller.Move(move);

                body.SetActive(true);
                controller.height = 2f;
                controller.radius = 0.5f;

                foreach (GameObject item in twoActivate)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in twoDeactivate)
                {
                    item.SetActive(false);
                }

                camera = 2;

            }
        }
        //2D Mode
        if (camera == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            //Camera.main.orthographicSize = camera2Size;
            //Movement
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = cameraTwo.GetComponent<Camera>().WorldToScreenPoint(transform.localPosition);

            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            body.transform.rotation = Quaternion.Euler(0f, -angle + 90f, 0f);


            Vector3 move = (transform.up * ((cameraTwo.transform.position.y - 1f) - transform.position.y));
            controller.Move(move);

            move = (transform.up * -1);
            controller.Move(move);

            if (1 == 1)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                cameraTwo.transform.position = new Vector3(transform.position.x, cameraTwo.transform.position.y, transform.position.z);

                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");
                move = (transform.right * x + transform.forward * z);

                if (x != 0 && z != 0)
                {
                    controller.Move(move * speed / (1.4f) * Time.deltaTime);
                }
                else
                {
                    controller.Move(move * speed * Time.deltaTime);
                }
                RaycastHit hit;
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, groundMask);

                pointBelow = hit.point;

            }
            //Perspective Swap
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {


                move = (transform.up * -(transform.position.y - pointBelow.y));
                controller.Move(move);

                cameraThree.SetActive(true);
                cameraTwo.transform.position = new Vector3(cameraTwo.transform.position.x, camera2OGPos, cameraTwo.transform.position.z);
                cameraTwo.SetActive(false);
                body.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                foreach (GameObject item in twoActivate)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in twoDeactivate)
                {
                    item.SetActive(true);
                }
                camera = 3;
            }
        }
    }
        
}
