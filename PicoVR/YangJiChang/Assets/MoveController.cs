using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
    public static bool isForward;
    public static bool isBack;
    public static bool isLeft;
    public static bool isRight;

    public float speed;
    public float rotationSpeed;

    CharacterController controller;
    Transform camera;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        camera = transform.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        Direction();
        Rotation();
	}
    void Direction()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isForward = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isBack = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isLeft = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isRight = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isForward = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isBack = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isRight = false;
        }
    }
    void Rotation()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if(x != 0)
        {
            transform.Rotate(transform.up, x * rotationSpeed, Space.World);
        }
        if(y != 0)
        {
            camera.Rotate(camera.right, y * rotationSpeed, Space.World);
        }
    }
    void LateUpdate()
    {
        if(isForward)
        {
            controller.SimpleMove(transform.forward * Time.deltaTime * speed);
        }
        else if(isBack)
        {
            controller.SimpleMove(-transform.forward * Time.deltaTime * speed);
        }
        if(isLeft)
        {
            controller.SimpleMove(-transform.right * Time.deltaTime * speed);
        }
        else if(isRight)
        {
            controller.SimpleMove(transform.right * Time.deltaTime * speed);
        }
    }
}
