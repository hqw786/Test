﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour {
    public static bool isForward;
    public static bool isBack;
    public static bool isLeft;
    public static bool isRight;
    //Vector3 moveDirection;
    public static bool isJump;
    public static bool isJumpDown;

    float walkSpeed;
    float runSpeed;
    float rotationHSpeed;
	float rotationVSpeed;
    float jumpHeight;

    float minFOV;
    float maxFOV;
    float FOVSpeed;

    float rate = 1f;

    bool isZoomIn;
    bool isZoomOut;

    CharacterController controller;
    Transform cameraTransform;
    Camera camera;

    Transform autoRoamStart;
    Transform autoRoamEnd;
    Vector3 AutoRoamDir;
    int startNum;
    int endNum;
	// Use this for initialization
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Main Camera");
        camera = cameraTransform.GetComponent<Camera>();
    }
    public void Initial()
    {
        walkSpeed = MainManager.Instance.walkSpeed;
        runSpeed = MainManager.Instance.runSpeed;

        rotationHSpeed = MainManager.Instance.rotationHSpeed;
        rotationVSpeed = MainManager.Instance.rotationVSpeed;

        minFOV = MainManager.Instance.minFOV_firstPerson;
        maxFOV = MainManager.Instance.maxFOV_firstPerson;

        FOVSpeed = MainManager.Instance.FOVSpeed_firstPerson;
        //第一次初始化位置，旋转和视距
        transform.position = MainManager.Instance.initialPosition_firstPerson;
        transform.rotation = MainManager.Instance.initialDir_firstPerson;

        FOVReset();
    }
	void Start () 
    {
		//Cursor.visible = false;//隐藏鼠标
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (isZoomIn)
        {
            isZoomIn = false;
            if (maxFOV == minFOV) return;
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, minFOV, Time.fixedDeltaTime * FOVSpeed);
            if(camera.fieldOfView < minFOV + 1f)
            {
                camera.fieldOfView = minFOV;
            }
            rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        }
        if (isZoomOut)
        {
            isZoomOut = false;
            if (maxFOV == minFOV) return;
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, maxFOV, Time.fixedDeltaTime * FOVSpeed);
            if(camera.fieldOfView > maxFOV -1f)
            {
                camera.fieldOfView = maxFOV;
            }
            rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        }
    }
	void Update () {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Direction();
            if (Input.GetMouseButton(1))
            {
                Rotation();
            }
            FOVChange(Input.GetAxis("Mouse ScrollWheel"));
        }
        if(Input.anyKey)
        {
            MainManager.Instance.isAutoRoam = false;
        }
        if(MainManager.Instance.isAutoRoam)
        {
            AutoRoaming();
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
            transform.Rotate(transform.up, x * rotationHSpeed * rate, Space.World);
        }
        if(y != 0)
        {
            cameraTransform.Rotate(cameraTransform.right, -y * rotationVSpeed * rate, Space.World);
        }
    }
    void FOVChange(float w)
    {
        if (w == 0f) return;
        print(w);
        if (w > 0)
        {
            isZoomIn = true;
        }
        else if (w < 0)
        {
            isZoomOut = true;
        }
    }
    void AutoRoaming()
    {
        //这种方式，先快后慢，不匀速
        //transform.position = Vector3.Lerp(transform.position, autoRoamEnd.position, Time.deltaTime * 0.1f);
        //第二种方式，匀速
        transform.Translate(AutoRoamDir * Time.deltaTime * 5f);
        if(Vector3.Distance(transform.position,autoRoamEnd.position) <= 1f)
        {
            if (!HasNextPosition())
            {
                MainManager.Instance.isAutoRoam = false;
            }
        }
    }
    void LateUpdate()
    {
        if (MainManager.Instance.curView == ViewMode.firstView)
        {
            if (isForward)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
                controller.SimpleMove(transform.forward * Time.deltaTime * v);
            }
            else if (isBack)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
				controller.SimpleMove(-transform.forward * Time.deltaTime * v);
            }
            if (isLeft)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
				controller.SimpleMove(-transform.right * Time.deltaTime * v);
            }
            else if (isRight)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
				controller.SimpleMove(transform.right * Time.deltaTime * v);
            }
        }
    }
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
    //}
    //这个改是是否可以落下，在空中垂直于地面的位置没超出地面范围，就可以落下。
    //public bool IsGround()
    //{


        //return 
    //}
    public void FOVReset()
    {
        camera.fieldOfView = maxFOV;
        rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        //将摄像机放平。
        Quaternion q = cameraTransform.localRotation;
        q.x = 0;
        cameraTransform.localRotation = q;
    }
    public void SetAutoRoamStartAndEndPoint(int s, int e)
    {
        if (s >= 0)
        {
            startNum = s;
            endNum = e;
            HasNextPosition();
        }
    }
    public bool HasNextPosition()
    {
        if(endNum == startNum)
        {
            return false;
        }
        autoRoamStart = ConfigData.Instance.roamPath[startNum];
        autoRoamEnd = ConfigData.Instance.roamPath[endNum];
        MainManager.Instance.isAutoRoam = true;
        transform.position = autoRoamStart.position;
        AutoRoamDir = autoRoamEnd.position - autoRoamStart.position;
        AutoRoamDir = AutoRoamDir.normalized;
        //可能要旋转一些

        startNum++;
        return true;
    }
}
