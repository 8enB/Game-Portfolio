using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    public GameObject vision;

    public bool canMove;
    public float moveSpeed;
    private Vector3 moveInput;
    private Vector3 lookInput;

    public Rigidbody theRB;

    //scrolling
    float timer;
    float translation;
    float position;
    float target;
    float falloff;
    float input;

    // Increases the target translation
    public float speed = 100.0f;
    // Maximum acceleration possible by scrolling the wheel faster
    public float maxAcceleration = 2.0f;
    // How quickly to follow the target
    public float followSpeed = 10.0f;
    // Across which Vector to translate (Default is Y axis)
    public Vector3 translationVector = new Vector3(0, 1, 0);
    // Whether or not to use the scrollwheel acceleration
    public bool scrollWheelAcceleration = true;
    //end scrolling

    //camera movement
    public float lookSens = 100f;

    public float jResource = 0f;
    public Text jRText;
    public float usaResource = 0f;
    public Text usaRText;

    //

    private void Awake()
    {
        instance = this;
    }
        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        jRText.text = jResource.ToString();
        usaRText.text = usaResource.ToString();
        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.z = Input.GetAxisRaw("Vertical");
            timer += Time.deltaTime;
            input = Input.GetAxis("Mouse ScrollWheel");
            lookInput.x = -Input.GetAxisRaw("RotateY");
            lookInput.y = Input.GetAxisRaw("RotateX");

            transform.Rotate(Vector3.up * lookInput.y * lookSens);
            vision.transform.Rotate(Vector3.right * lookInput.x * lookSens);
            /*xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            vision.transform.localRotation = Quaternion.Euler(xRotation, mouseX, 0f);
            vision.transform.Rotate(Vector3.up * mouseX);*/

            // This is the acceleration according to the time difference between the "clicks" of the mousewheel
            // If you leave that out, it will be more like Opera scrolling (larger discrete steps but smooth follow)
            // The 300 could be adjusted (lower means larger steps, stronger acceleration)
            if (scrollWheelAcceleration)
            {
                if (input != 0)
                {
                    target += Mathf.Clamp((input / (timer * 300)) * speed, maxAcceleration * -1, maxAcceleration);
                    timer = 0;
                }
            }
            else
            {
                target += Mathf.Clamp(input * speed, maxAcceleration * -1, maxAcceleration);
            }
            // As a falloff we use the distance between position and target
            // results in faster Movement at higher distances
            falloff = Mathf.Abs(position - target);

            // Determine the amount of translation for this frame
            translation = Time.deltaTime * falloff * followSpeed;

            // 0.001 is our deadzone
            if (position + 0.001 < target)
            {
                this.GetComponent<Transform>().Translate(translationVector * translation * -1);
                position += translation;
            }
            if (position - 0.001 > target)
            {
                this.GetComponent<Transform>().Translate(translationVector * translation);
                position -= translation;
            }
        }
        else
        {
            moveInput.x = 0;
            moveInput.z = 0;
            moveInput.y = 0;
        }

        //set velocity equal to wasd input times the move speed
        if (canMove /*&& !LevelManager.instance.isPaused*/)
        {
                transform.position += transform.right * Time.deltaTime * moveSpeed * moveInput.x;
            transform.position += transform.forward * Time.deltaTime * moveSpeed * moveInput.z;
            /*if (moveInput.x != 0 & moveInput.y != 0)
            {
                //theRB.velocity = moveInput * moveSpeed * transform.rotation.normalize / (1.4f);#
            }
            else
            {
                transform.localPosition += moveInput * moveSpeed / (1.4f);
                //theRB.velocity = moveInput * moveSpeed * transform.rotation.normalize;
            }*/
        }
    }
}
