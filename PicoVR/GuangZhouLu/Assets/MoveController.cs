using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour {
    [HideInInspector]
    public bool isInput;
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

    [HideInInspector]
    public Transform autoRoamStart;
    [HideInInspector]
    public Transform autoRoamEnd;
    [HideInInspector]
    public Vector3 autoRoamDir;
    [HideInInspector]
    public Quaternion endHRotation;
    [HideInInspector]
    public Quaternion endVRotation;
    [HideInInspector]
    public int startNum;
    [HideInInspector]
    public int endNum;
    [HideInInspector]
    public bool isHRotation;
	[HideInInspector]
	public bool isVRotation;
    EventSystem es;
    StandaloneInputModule sim;
    BaseInput bi;
	// Use this for initialization
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Main Camera");
        camera = cameraTransform.GetComponent<Camera>();
        es = transform.Find("/EventSystem").GetComponent<EventSystem>();
        sim = transform.Find("/EventSystem").GetComponent<StandaloneInputModule>();
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
        Quaternion q = Quaternion.identity;
        transform.rotation = q * Quaternion.Euler(MainManager.Instance.initialDir_firstPerson);

        FOVReset();
    }
    public void Initial(Vector3 position, Quaternion rotation,Quaternion cameraRotation)
    {
        walkSpeed = MainManager.Instance.walkSpeed;
        runSpeed = MainManager.Instance.runSpeed;

        rotationHSpeed = MainManager.Instance.rotationHSpeed;
        rotationVSpeed = MainManager.Instance.rotationVSpeed;

        minFOV = MainManager.Instance.minFOV_firstPerson;
        maxFOV = MainManager.Instance.maxFOV_firstPerson;

        FOVSpeed = MainManager.Instance.FOVSpeed_firstPerson;
        //第一次初始化位置，旋转和视距
        transform.position = position;
        transform.rotation = rotation;

        //FOVReset();
        camera.fieldOfView = maxFOV;
        rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        
        cameraTransform.localRotation = cameraRotation;
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
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //print("移动");
            if (!MainManager.Instance.isAutoRoam)
            {
                Direction();
            }
            if (Input.GetMouseButton(1))
            {
                if (MainManager.Instance.roamView == RoamView.custom)
                {
                    Rotation();
                }
            }
            FOVChange(Input.GetAxis("Mouse ScrollWheel"));
        }
        else
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            if (ped != null && ped.selectedObject != null)
            {
                if (ped.selectedObject.transform.parent.name.Contains("MenuPanel") || ped.selectedObject.transform.parent.name.Contains("ShowImagePanel"))
                {
                    //print("ui移动");
                    if (!MainManager.Instance.isAutoRoam)
                    {
                        Direction();
                    }
                    if (Input.GetMouseButton(1))
                    {
                        if (MainManager.Instance.roamView == RoamView.custom)
                        {
                            Rotation();
                        }
                    }
                    FOVChange(Input.GetAxis("Mouse ScrollWheel"));
                }
            }
        }

        if (MainManager.Instance.isAutoRoam)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)
                || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isInput = true;
            }
            AutoRoaming();
            if (MainManager.Instance.roamView == RoamView.fix)
            {
                //DONE:简化，先注释掉
                if (isVRotation)
                {
                    cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, endVRotation, Time.deltaTime * 5f);
                    if (cameraTransform.localRotation == endVRotation)
                    {
                        isVRotation = false;
                        isHRotation = true;
                    }
                }
                if (isHRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, endHRotation, Time.deltaTime * 2f);
                    if (transform.rotation == endHRotation)
                    {
                        isHRotation = false;
                    }
                }
            }
            if (isInput)
            {
                transform.Find("/Canvas/MenuPanel").GetComponent<MenuPanel>().ExitRoam();
                isInput = false;
                MainManager.Instance.roamStatus = RoamStatus.pause;
            }
        }
    }
    void Direction()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            float v = Input.GetKey(KeyCode.Space) ? runSpeed : walkSpeed;
            print("W: " + Time.fixedDeltaTime * v);
            controller.SimpleMove(transform.forward * Time.fixedDeltaTime * v);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            float v = Input.GetKey(KeyCode.Space) ? runSpeed : walkSpeed;
            print("S: " + Time.fixedDeltaTime * v);
            controller.SimpleMove(-transform.forward * Time.fixedDeltaTime * v);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            float v = Input.GetKey(KeyCode.Space) ? runSpeed : walkSpeed;
            print("A: " + Time.fixedDeltaTime * v);
            controller.SimpleMove(-transform.right * Time.fixedDeltaTime * v);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            float v = Input.GetKey(KeyCode.Space) ? runSpeed : walkSpeed;
            print("D: " + Time.fixedDeltaTime * v);
            controller.SimpleMove(transform.right * Time.fixedDeltaTime * v);
        }

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), transform.position.y, Input.GetAxis("Vertical"));
        //航拍模式不能跳
        //if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        //{
        //    isJump = true;
        //}
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
		transform.Translate(autoRoamDir * Time.deltaTime * MainManager.Instance.roamSpeed, Space.World);

        if(Vector3.Distance(transform.position,autoRoamEnd.position) <= 1f)
        {

            if (!HasNextPosition())
            {
                //DONE:如果要循环漫游这边就要改一下。
                //MainManager.Instance.isAutoRoam = false;
                //UIManager.Instance.HideUI(Define.uiPanelRoamView);
                //transform.Find("/Canvas/MenuPanel/BtnAutoRoam/Image").gameObject.SetActive(false);
                //MainManager.Instance.roamView = RoamView.custom;
				//TODO: 显示一个结束提示

                //TODO:改成循环漫游
                SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
            }
        }
    }
    public void FOVReset()
    {
        camera.fieldOfView = maxFOV;
        rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        //将摄像机放平。
        Quaternion q = cameraTransform.localRotation;
        q.x = 0;
        cameraTransform.localRotation = q;
    }
    public void SetAutoRoamStartAndEndPoint(Transform person, int e)
    {
        isHRotation = false;
        isVRotation = true;
        //单段的漫游起点和终点
        autoRoamStart = ConfigData.Instance.roamPath[e-1];
        autoRoamEnd = ConfigData.Instance.roamPath[e];
        //人物视角转成上一个漫游点的方向
        endHRotation = autoRoamStart.rotation;
        endVRotation = Quaternion.identity;

        RoamInfo ri = autoRoamStart.GetComponent<RoamInfo>();
        if (ri.nodeInfo == RoamNodeInfo.speed)
        {
            MainManager.Instance.roamSpeed = MainManager.Instance.fastRoamSpeed;
        }
        else
        {
            MainManager.Instance.roamSpeed = MainManager.Instance.normalRoamSpeed;
        }

        autoRoamStart.position = new Vector3(person.position.x, MainManager.Instance.fpYHeight, person.position.z);
        autoRoamEnd.position = new Vector3(autoRoamEnd.position.x, MainManager.Instance.fpYHeight, autoRoamEnd.position.z);

        MainManager.Instance.isAutoRoam = true;

        autoRoamDir = autoRoamEnd.position - transform.position;
        autoRoamDir = autoRoamDir.normalized;
    }
    public void SetAutoRoamStartAndEndPoint(int s, int e)
    {
        if (s >= 0)
        {
            startNum = s;
            endNum = e;
			isHRotation = false;
			isVRotation = true;
            HasNextPosition();
        }
    }
    
    public bool HasNextPosition()
    {
        if(endNum <= startNum)
        {
            return false;
        }

        autoRoamStart = ConfigData.Instance.roamPath[startNum];
		autoRoamEnd = ConfigData.Instance.roamPath[startNum+1];

        //是不是要快速漫游
        RoamInfo ri = autoRoamStart.GetComponent<RoamInfo>();
        if (ri.nodeInfo == RoamNodeInfo.speed)
        {
            MainManager.Instance.roamSpeed = MainManager.Instance.fastRoamSpeed;
        }
        else
        {
            MainManager.Instance.roamSpeed = MainManager.Instance.normalRoamSpeed;
        }

        autoRoamStart.position = new Vector3(autoRoamStart.position.x, MainManager.Instance.fpYHeight, autoRoamStart.position.z);
        autoRoamEnd.position = new Vector3(autoRoamEnd.position.x, MainManager.Instance.fpYHeight, autoRoamEnd.position.z);

        MainManager.Instance.isAutoRoam = true;
        //不要跃传过去
        transform.position = autoRoamStart.position;
		autoRoamDir = autoRoamEnd.position - autoRoamStart.position;
        //要移动过去
        autoRoamDir = autoRoamEnd.position - transform.position;
        autoRoamDir = autoRoamDir.normalized;
		//DONE:简化，先注释掉
		#region 简化，先注释掉
        //transform.rotation = Quaternion.identity;//不加这行，会变成增量旋转
        //Quaternion q = transform.rotation;
        //float a = Vector3.Angle(Vector3.forward, autoRoamDir);
        //q = q * Quaternion.Euler(0, 90 - a, 0);
        //endHRotation = q;
        //endVRotation = GetEndVRotation();

        //isVRotation = true;
        //isHRotation = false;
		#endregion

		//TODO:简化，如果要启用固定和可控视角。这一段就注释掉
		#region 简化，如果要启用固定和可控视角。这一段就注释掉
        endHRotation = autoRoamStart.rotation;
        endVRotation = GetEndVRotation();
        isHRotation = true;
		#endregion

		startNum++;
        MainManager.Instance.roamPauseNum = startNum;
        if (MainManager.Instance.roamPauseNum >= ConfigData.Instance.roamPath.Count - 1)
        {
            MainManager.Instance.roamPauseNum = 0;
        }
        return true;
    }
	Quaternion GetEndVRotation()
	{
        return Quaternion.identity;
	}
    internal void SwitchToFly()
    {
        //MainManager.Instance.ViewModeSwitch();
        MainManager.Instance.flyController.autoRoamStart = autoRoamStart;
        MainManager.Instance.flyController.autoRoamStart.position = new Vector3(
            autoRoamStart.position.x,
            MainManager.Instance.flyYHeight,
            autoRoamStart.position.z);
        MainManager.Instance.flyController.autoRoamEnd = autoRoamEnd;
        MainManager.Instance.flyController.autoRoamEnd.position = new Vector3(
            autoRoamEnd.position.x,
            MainManager.Instance.flyYHeight,
            autoRoamEnd.position.z);
        transform.position = new Vector3(transform.position.x, MainManager.Instance.flyYHeight, transform.position.z);
        SwitchModeModifyRotation();
        
        //以下不用修改
        MainManager.Instance.flyController.autoRoamDir = autoRoamDir;
        MainManager.Instance.flyController.startNum = startNum;
        MainManager.Instance.flyController.endNum = endNum;
        MainManager.Instance.flyController.isHRotation = false;
        MainManager.Instance.flyController.isVRotation = true;
    }
    public void SwitchModeModifyRotation()
    {
        //DONE:简化，先注释掉
        //transform.rotation = Quaternion.identity;
        //Quaternion q = transform.rotation;
        //float a = Vector3.Angle(Vector3.forward, autoRoamDir);
        //q = q * Quaternion.Euler(0, 90 - a, 0);
        //MainManager.Instance.flyController.endHRotation = q;

        //DONE:简化，如果要改过来请注释掉下面一段代码
        MainManager.Instance.flyController.endHRotation = ConfigData.Instance.roamPath[startNum - 1].rotation;

        Quaternion q = Quaternion.identity;
        q = q * Quaternion.Euler(MainManager.Instance.cameraRoamAngle, 0, 0);
        MainManager.Instance.flyController.endVRotation = q;
    }
}
