using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Video;
using UnityEngine.SceneManagement;

public enum ViewMode
{
    firstView,
    flyView
}

public class MainManager : MonoBehaviour 
{
    public void SavePositionAndRotation(Vector3 p,Quaternion r, Quaternion cr, int s)
    {
        PlayerPrefs.SetFloat("px", p.x);
        PlayerPrefs.SetFloat("py", p.y);
        PlayerPrefs.SetFloat("pz", p.z);

        PlayerPrefs.SetFloat("rx", r.x);
        PlayerPrefs.SetFloat("ry", r.y);
        PlayerPrefs.SetFloat("rz", r.z);
        PlayerPrefs.SetFloat("rw", r.w);

        PlayerPrefs.SetFloat("crx", cr.x);
        PlayerPrefs.SetFloat("cry", cr.y);
        PlayerPrefs.SetFloat("crz", cr.z);
        PlayerPrefs.SetFloat("crw", cr.w);

        PlayerPrefs.SetInt("curView", s);
    }
    public Vector3 GetPosition()
    {
        return new Vector3(PlayerPrefs.GetFloat("px"), PlayerPrefs.GetFloat("py"), PlayerPrefs.GetFloat("pz"));
    }
    public Quaternion GetRotation()
    {
        return new Quaternion(PlayerPrefs.GetFloat("rx"), PlayerPrefs.GetFloat("ry"), PlayerPrefs.GetFloat("rz"), PlayerPrefs.GetFloat("rw"));
    }
    public Quaternion GetCameraRotation()
    {
        return new Quaternion(PlayerPrefs.GetFloat("crx"), PlayerPrefs.GetFloat("cry"), PlayerPrefs.GetFloat("crz"), PlayerPrefs.GetFloat("crw"));
    }
    public ViewMode GetViewMode()
    {
        return (ViewMode)(PlayerPrefs.GetInt("curView"));
    }

    public static MainManager Instance;
    [HideInInspector]
	public Transform person;//人物
    [HideInInspector]
    public MoveController firstPerson;//第一人称脚本
    [HideInInspector]
    public FlyController flyController;//飞行脚本
    [HideInInspector]
	public ViewMode curView;
    [HideInInspector]
    public ViewMode lastView;

    [Header("地面移动数值")]
    public Vector3 initialPosition_firstPerson;
    public Vector3 initialDir_firstPerson;
    public float fpYHeight;
    public float walkSpeed;
    public float runSpeed;
    public float minFOV_firstPerson;
    public float maxFOV_firstPerson;
    [Range(2,10)]
    public float FOVSpeed_firstPerson;
    
    [Header("飞行移动数值")]
    public Quaternion initialDir_Fly;
    public float cameraAngle;
    public float cameraRoamAngle;
    public float flyYHeight;
    public float walkSpeed_Fly;
    public float runSpeed_Fly;
    public float minFOV_Fly;
    public float maxFOV_Fly;
    [Range(2, 10)]
    public float FOVSpeed_Fly;
    [Range(0, 5)]
    public float rotationHSpeed;
    [Range(0, 5)]
    public float rotationVSpeed;

    [HideInInspector]
    public bool isAutoRoam;
    [HideInInspector]
    public RoamView roamView;
    [HideInInspector]
    public bool isShowMineMap;
    [HideInInspector]
    public float rate;
    [HideInInspector]
    public float mineRate;
    [HideInInspector]
    public int roamPauseNum;
    [HideInInspector]
    public Transform roamPauseEnd;//自动漫游中断后，把每段的终点保存下来
    [Header("自动漫游数值")]
    public float roamSpeed;
    public float normalRoamSpeed;
    public float fastRoamSpeed;
    // Use this for initialization
	void Awake()
	{
        Instance = this;
        roamView = RoamView.custom;
        person = transform.Find("/Person");
        //rb = person.GetComponent<Rigidbody>();
        flyController = person.GetComponent<FlyController>();
        firstPerson = person.GetComponent<MoveController>();
        //curView = ViewMode.firstView;
        //InitialMap();
        //cameraAngle = 0f;
        roamPauseNum = 0;
    }

	void Start () {
        //if (PlayerPrefs.GetFloat("px") == 0 && PlayerPrefs.GetFloat("py") == 0 && PlayerPrefs.GetFloat("pz") == 0)
        {
            flyController.Initial();
            firstPerson.Initial();
            flyController.enabled = false;
            firstPerson.enabled = true;
            transform.Find("/Canvas/MenuPanel/BtnPersonView").transform.Find("Image").gameObject.SetActive(true);
        }
        //以下是昼夜切换要用到的。昼夜切换主角位置不变
        //else
        //{
        //    flyController.Initial(GetPosition(), GetRotation(), GetCameraRotation());
        //    firstPerson.Initial(GetPosition(), GetRotation(), GetCameraRotation());

        //    curView = GetViewMode();

        //    if (curView == ViewMode.firstView)
        //    {
        //        flyController.enabled = false;
        //        firstPerson.enabled = true;
        //        //菜单按钮
        //        transform.Find("/Canvas/MenuPanel/BtnPersonView").transform.Find("Image").gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        flyController.enabled = true;
        //        firstPerson.enabled = false;
        //        //菜单按钮
        //        transform.Find("/Canvas/MenuPanel/BtnFlyView").transform.Find("Image").gameObject.SetActive(true);
        //    }
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// 视角切换
    /// </summary>
	public void ViewModeSwitch()
	{
        //lastView = curView;
        //curView = lastView == ViewMode.firstView ? ViewMode.flyView : ViewMode.firstView;
        curView = curView == ViewMode.firstView ? ViewMode.flyView : ViewMode.firstView;
        if (curView == ViewMode.firstView)
		{
			firstPerson.enabled = true;
			flyController.enabled = false;
            positionSwitch_FirstPerson();
            firstPerson.FOVReset();//FOV设置一下。
		}
        else if (curView == ViewMode.flyView)
        {
            firstPerson.enabled = false;
            flyController.enabled = true;
            positionSwitch_Fly();
            flyController.FOVReset();//FOV设置一下。
        }
	}
    public void SetAutoRoam()
    {
        //切换到自动漫游，先移到第一个位置然后自动向后面前进
        isAutoRoam = true;
    }
    /// <summary>
    /// 位置切换
    /// </summary>
    /// <param name="view">视角模式</param>
	void positionSwitch_Fly()
	{
		person.position = new Vector3(person.position.x, flyYHeight, person.position.z);
	}
    void positionSwitch_FirstPerson()
    {
        //这边落地：是落到相近的预置点，还是物体所在的正下方，物体向下发射一个射线，落到碰撞体上
        person.position = new Vector3(person.position.x, fpYHeight, person.position.z);
    }
    public void WarpToNewPosition(Transform point)
    {
        //TODO:根据视角决定高度
        if (curView == ViewMode.firstView)
        {
            person.position = point.position;
            person.rotation = point.rotation;
        }
        else
        {
            person.position = new Vector3(point.position.x, flyYHeight, point.position.z);
            person.rotation = point.rotation;
        }
    }
	public void CloseAutoRoam()
	{
		if (MainManager.Instance.isAutoRoam)
		{
			//自动漫游的参数恢复到默认值
			MainManager.Instance.isAutoRoam = false;
			UIManager.Instance.HideUI(Define.uiPanelRoamView);
			//TODO:这个到底是fix还是custom
			MainManager.Instance.roamView = RoamView.custom;
		}
	}
}
