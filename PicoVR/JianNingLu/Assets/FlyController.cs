using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyController : MonoBehaviour {
    public static bool isForward;
    public static bool isBack;
    public static bool isLeft;
    public static bool isRight;
    [HideInInspector]
    public bool isInput;
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
    public void Initial(Vector3 position, Quaternion rotation, Quaternion cameraRotation)
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
        transform.position = position;
        transform.rotation = rotation;

        //FOVReset();
        camera.fieldOfView = maxFOV;
        rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);

        cameraTransform.localRotation = cameraRotation;

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
                if (isVRotation)
                {
                    cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, endVRotation, Time.deltaTime);
                    if (cameraTransform.localRotation == endVRotation)
                    {
                        isVRotation = false;
                        isHRotation = true;
                    }
                }
                if (isHRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, endHRotation, Time.deltaTime);
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
            }
        }
    }
    void Direction()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //isForward = true;
            //isInput = true;
            float v = Input.GetKey(KeyCode.Space) ? fastSpeed : normalSpeed;
            controller.Move(transform.forward * Time.deltaTime * v * rate);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //isBack = true;
            //isInput = true;
            float v = Input.GetKey(KeyCode.Space) ? fastSpeed : normalSpeed;
            controller.Move(-transform.forward * Time.deltaTime * v * rate);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //isLeft = true;
            //isInput = true;
            float v = Input.GetKey(KeyCode.Space) ? fastSpeed : normalSpeed;
            controller.Move(-transform.right * Time.deltaTime * v * rate);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //isRight = true;
            //isInput = true;
            float v = Input.GetKey(KeyCode.Space) ? fastSpeed : normalSpeed;
            controller.Move(transform.right * Time.deltaTime * v * rate);
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
	void AutoRoaming()
	{
		transform.Translate(autoRoamDir * Time.deltaTime * MainManager.Instance.roamSpeed * 3f, Space.World);
		if(Vector3.Distance(transform.position,autoRoamEnd.position) <= 1f)
		{
			if(!HasNextPosition())
			{
				MainManager.Instance.isAutoRoam = false;
				UIManager.Instance.HideUI(Define.uiPanelRoamView);
			}
		}
	}
    //void LateUpdate()
    //{
    //    if (MainManager.Instance.curView == ViewMode.flyView)
    //    {
    //        if (isForward)
    //        {
    //            float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
    //            controller.Move(transform.forward * Time.deltaTime * v * rate);
    //        }
    //        else if (isBack)
    //        {
    //            float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
    //            controller.Move(-transform.forward * Time.deltaTime * v * rate);
    //        }
    //        if (isLeft)
    //        {
    //            float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
    //            controller.Move(-transform.right * Time.deltaTime * v * rate);
    //        }
    //        else if (isRight)
    //        {
    //            float v = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
    //            controller.Move(transform.right * Time.deltaTime * v * rate);
    //        }
    //    }
    //}
    public void FOVReset()
    {
        camera.fieldOfView = maxFOV;
        rate = (camera.fieldOfView - minFOV + 1) / (maxFOV - minFOV);
        //将摄像机对准正下方位置。
        Quaternion q = cameraTransform.localRotation;
        q.x = 0f;
        q = q * Quaternion.Euler(MainManager.Instance.cameraAngle, 0, 0);
        cameraTransform.localRotation = q;
    }
	public void SetAutoRoamStartAndEndPoint(int s, int e)
	{
		if(s>=0)
		{
			startNum = s;
			endNum = e;
            isHRotation = false;
            isVRotation = true;
            MainManager.Instance.cameraAngle = 40f;
			HasNextPosition();
		}
	}
	public bool HasNextPosition()
	{
		if (endNum == startNum) return false;

		autoRoamStart = ConfigData.Instance.roamPath[startNum];
		Vector3 temp = autoRoamStart.position;
		autoRoamStart.position = new Vector3(temp.x, yHeight, temp.z);
		autoRoamEnd = ConfigData.Instance.roamPath[startNum + 1];
		temp = autoRoamEnd.position;
		autoRoamEnd.position = new Vector3(temp.x, yHeight, temp.z);
		MainManager.Instance.isAutoRoam = true;
		transform.position = autoRoamStart.position;

		autoRoamDir = autoRoamEnd.position - autoRoamStart.position;
		autoRoamDir = autoRoamDir.normalized;
        //DONE:简化，先注释掉
        #region 简化，先注释掉
        //transform.rotation = Quaternion.identity;
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
        if (MainManager.Instance.roamPauseNum == ConfigData.Instance.roamPath.Count - 1)
        {
            MainManager.Instance.roamPauseNum = 0;
        }
		return true;
	}
	Quaternion GetEndVRotation()
	{
        Quaternion q = Quaternion.identity;
        q = q * Quaternion.Euler(MainManager.Instance.cameraAngle, 0, 0);
		return q;
	}
      internal void SwitchToPerson()
    {
        //MainManager.Instance.ViewModeSwitch();
        MainManager.Instance.firstPerson.autoRoamStart = autoRoamStart;
        MainManager.Instance.firstPerson.autoRoamStart.position = new Vector3(
            autoRoamStart.position.x,
            MainManager.Instance.fpYHeight,
            autoRoamStart.position.z);
        MainManager.Instance.firstPerson.autoRoamEnd = autoRoamEnd;
        MainManager.Instance.firstPerson.autoRoamEnd.position = new Vector3(
            autoRoamEnd.position.x,
            MainManager.Instance.fpYHeight,
            autoRoamEnd.position.z);
        transform.position = new Vector3(transform.position.x, MainManager.Instance.fpYHeight, transform.position.z);
        SwitchModeModifyRotation();

        //以下不用修改
        MainManager.Instance.firstPerson.autoRoamDir = autoRoamDir;
        MainManager.Instance.firstPerson.startNum = startNum;
        MainManager.Instance.firstPerson.endNum = endNum;
        MainManager.Instance.firstPerson.isHRotation = false;
        MainManager.Instance.firstPerson.isVRotation = true;
    }
      public void SwitchModeModifyRotation()
      {
          //DONE:简化，先注释掉
          //transform.rotation = Quaternion.identity;
          //Quaternion q = transform.rotation;
          //float a = Vector3.Angle(Vector3.forward, autoRoamDir);
          //q = q * Quaternion.Euler(0, 90 - a, 0);
          //MainManager.Instance.firstPerson.endHRotation = q;

          //DONE:简化，要恢复请注释下面一段代码
          MainManager.Instance.firstPerson.endHRotation = ConfigData.Instance.roamPath[startNum - 1].rotation;

          MainManager.Instance.firstPerson.endVRotation = Quaternion.identity;
      }
}
