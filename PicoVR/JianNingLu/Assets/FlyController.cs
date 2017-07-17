using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyController : MonoBehaviour {
    public static bool isForward;
    public static bool isBack;
    public static bool isLeft;
    public static bool isRight;
    //Vector3 moveDirection;
    public static bool isJump;
    public static bool isJumpDown;

    float yHeight;
    float normalSpeed;
    float fastSpeed;
    float rotationHSpeed;
    float rotationVSpeed;

    float minFOV;
    float maxFOV;
    float FOVSpeed;
    bool isZoomIn;
    bool isZoomOut;

    float rate = 1f;

    Camera camera;
    CharacterController controller;
    Transform cameraTransform;
    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Main Camera");
        camera = cameraTransform.GetComponent<Camera>();
    }
    public void Initial()
    {
        yHeight = MainManager.Instance.flyYHeight;
        normalSpeed = MainManager.Instance.walkSpeed_Fly;
        fastSpeed = MainManager.Instance.runSpeed_Fly;

        rotationHSpeed = MainManager.Instance.rotationHSpeed;
        rotationVSpeed = MainManager.Instance.rotationVSpeed;

        minFOV = MainManager.Instance.minFOV_Fly;
        maxFOV = MainManager.Instance.maxFOV_Fly;
        FOVSpeed = MainManager.Instance.FOVSpeed_Fly;

        //位置旋转视距等改变
        camera.fieldOfView = maxFOV;
        transform.position = new Vector3(transform.position.x, yHeight, transform.position.z);
        FOVReset();
    }
    void Start()
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
            if (camera.fieldOfView < minFOV + 1f)
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
            if (camera.fieldOfView > maxFOV - 1f)
            {
                camera.fieldOfView = maxFOV;
            }
            rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        }
    }
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Direction();
            if (Input.GetMouseButton(1))
            {
                Rotation();
            }
            FOVChange(Input.GetAxis("Mouse ScrollWheel"));
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
        if (x != 0)
        {
			transform.Rotate(transform.up, x * rotationHSpeed * rate, Space.World);
        }
        if (y != 0)
        {
			cameraTransform.Rotate(cameraTransform.right, -y * rotationVSpeed * rate, Space.World);
        }
    }
    void FOVChange(float w)
    {
        if (w == 0f) return;
        if(w > 0)
        {
            isZoomIn = true;
        }
        else if (w<0)
        {
            isZoomOut = true;
        }
    }
    void LateUpdate()
    {
        if (MainManager.Instance.curView == ViewMode.flyView)
        {
            if (isForward)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
                controller.Move(transform.forward * Time.deltaTime * v * rate);
            }
            else if (isBack)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
                controller.Move(-transform.forward * Time.deltaTime * v * rate);
            }
            if (isLeft)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
                controller.Move(-transform.right * Time.deltaTime * v * rate);
            }
            else if (isRight)
            {
                float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
                controller.Move(transform.right * Time.deltaTime * v * rate);
            }
        }
    }
    public void FOVReset()
    {
        camera.fieldOfView = maxFOV;
        rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        //将摄像机对准正下方位置。
        Quaternion q = cameraTransform.localRotation;
        q.x = 0f;
        q = q * Quaternion.Euler(90, 0, 0);
        cameraTransform.localRotation = q;
    }
}
