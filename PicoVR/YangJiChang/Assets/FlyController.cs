using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyController : MonoBehaviour {
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

    public float minFov;
    public float maxFov;
    bool isZoomIn;
    bool isZoomOut;
    
    Camera camera;
    CharacterController controller;
    Transform cameraTransform;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Main Camera");
        camera = cameraTransform.GetComponent<Camera>();
        //Cursor.visible = false;//隐藏鼠标
		camera.fieldOfView = maxFov;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isZoomIn)
        {
			isZoomIn = false;
            if (maxFov == minFov) return;
            camera.fieldOfView = Mathf.Lerp((camera.fieldOfView - minFov) / (maxFov - minFov), 0f, Time.fixedDeltaTime*10);
			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView * (maxFov - minFov), minFov, maxFov);
            //换种插值方法
            //camera.fieldOfView = Mathf.SmoothStep()
        }
        else if(isZoomOut)
        {
			isZoomOut = false;
            if (maxFov == minFov) return;
            camera.fieldOfView = Mathf.Lerp((camera.fieldOfView - minFov) / (maxFov - minFov), 1f, Time.fixedDeltaTime*10);
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView*(maxFov - minFov), minFov, maxFov);
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
        }
        
        FOVChange(Input.GetAxis("Mouse ScrollWheel"));

        
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
			transform.Rotate(transform.up, x * rotationHSpeed * (camera.fieldOfView - minFov + 1) / (maxFov - minFov), Space.World);
        }
        if (y != 0)
        {
			cameraTransform.Rotate(cameraTransform.right, -y * rotationVSpeed * (camera.fieldOfView - minFov + 1) / (maxFov - minFov), Space.World);
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
    }
}
