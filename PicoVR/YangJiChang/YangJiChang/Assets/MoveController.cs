using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour {
    public static bool isForward;
    public static bool isBack;
    public static bool isLeft;
    public static bool isRight;
    Vector3 moveDirection;
    public static bool isJump;
    public static bool isJumpDown;

    public float speed;
    public float rotationHSpeed;
	public float rotationVSpeed;
    public float jumpHeight;

    CharacterController controller;
    Transform camera;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        camera = transform.Find("Main Camera");
		//Cursor.visible = false;//隐藏鼠标
	}
	
	// Update is called once per frame
	void Update () {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Direction();
            Rotation();
        }
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
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), transform.position.y, Input.GetAxis("Vertical"));
        //航拍模式不能跳
        //if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        //{
        //    isJump = true;
        //}

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
            transform.Rotate(transform.up, x * rotationHSpeed, Space.World);
        }
        if(y != 0)
        {
            camera.Rotate(camera.right, -y * rotationVSpeed, Space.World);
        }
    }
    void LateUpdate()
    {
        if (MainManager.Instance.curView == ViewMode.firstView)
        {
            if (isForward)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.SimpleMove(transform.forward * Time.deltaTime * v);
            }
            else if (isBack)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.SimpleMove(-transform.forward * Time.deltaTime * v);
            }
            if (isLeft)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.SimpleMove(-transform.right * Time.deltaTime * v);
            }
            else if (isRight)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.SimpleMove(transform.right * Time.deltaTime * v);
            }
        }
        else
        {
            if (isForward)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.Move(transform.forward * Time.deltaTime * v);
            }
            else if (isBack)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.Move(-transform.forward * Time.deltaTime * v);
            }
            if (isLeft)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.Move(-transform.right * Time.deltaTime * v);
            }
            else if (isRight)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
                controller.Move(transform.right * Time.deltaTime * v);
            }
        }
        //if(MainManager.Instance.curView == ViewMode.firstView)
        //{
        //    float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
        //    controller.SimpleMove(moveDirection * v * Time.deltaTime);
        //}
        //else
        //{
        //    float v = Input.GetKey(KeyCode.LeftShift) ? 2f * speed : speed;
        //    controller.Move(moveDirection * v * Time.deltaTime);
        //}

        //if(isJump)
        //{
        //    if (!isJumpDown)
        //    {
        //        float y = transform.position.y;
        //        y = Mathf.Lerp(y, jumpHeight, 0.05f);
        //        if (y >= 0.95f * jumpHeight)
        //        {
        //            isJumpDown = true;
        //            y = jumpHeight;
        //        }
        //        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        //    }
        //    else
        //    {
        //        float y = transform.position.y;
        //        y = Mathf.Lerp(y, 0f, 0.05f);
        //        if(y<=0.05f * jumpHeight)
        //        {
        //            isJump = false;
        //            isJumpDown = false;
        //            y = 0f;
        //        }
        //        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        //    }
        //}
        //if(controller.isGrounded && isJump)
        //{
        //    isJump = false;
        //}
    }
}
