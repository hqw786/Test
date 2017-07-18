using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum ViewMode
{
    firstView,
    flyView,
    autoRoam
}

public class MainManager : MonoBehaviour 
{
    public static MainManager Instance;
	Transform person;//人物
    [HideInInspector]
    public MoveController firstPerson;//第一人称脚本
    [HideInInspector]
    public FlyController flyController;//飞行脚本
    [HideInInspector]
	public ViewMode curView;
    [HideInInspector]
    public ViewMode lastView;
    //Rigidbody rb;

    [Header("地面移动数值")]
    public Vector3 initialPosition_firstPerson;
    public Quaternion initialDir_firstPerson;
    public float walkSpeed;
    public float runSpeed;
    public float minFOV_firstPerson;
    public float maxFOV_firstPerson;
    [Range(2,10)]
    public float FOVSpeed_firstPerson;
    
    [Header("飞行移动数值")]
    public Quaternion initialDir_Fly;
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
    //[Header("前往目的地地图背景")]
    // Use this for initialization
	void Awake()
	{
        Instance = this;
        roamView = RoamView.fix;
        person = transform.Find("/Person");
        //rb = person.GetComponent<Rigidbody>();
        flyController = person.GetComponent<FlyController>();
        firstPerson = person.GetComponent<MoveController>();
        curView = ViewMode.firstView;
        InitialMap();
    }
    void InitialMap()
    {
        //Transform t1 = transform.Find("/BoundaryPoints/LeftDown");
        //Transform t2 = transform.Find("/BoundaryPoints/RightUp");
        //float w = Mathf.Abs(t2.position.z - t1.position.z);
        //float h = Mathf.Abs(t2.position.x - t1.position.x);
        //if(w >= h)
        //{
        //    //前往目的地的背景图片大小设置
        //    //小地图的背景图片大小设置
        //}
        //else
        //{
        //    //前往目的地的背景图片大小设置
        //    //小地图的背景图片大小设置
        //}
    }
	void Start () {
        flyController.Initial();
		flyController.enabled = false;
        firstPerson.Initial();
		firstPerson.enabled = true;
                
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// 视角切换
    /// </summary>
	public void ViewModeSwitch()
	{
        lastView = curView;
        curView = lastView == ViewMode.firstView ? ViewMode.flyView : ViewMode.firstView;
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
        person.position = new Vector3(person.position.x, 1f, person.position.z);
    }
    public void WarpToNewPosition(Transform point)
    {
        person.position = point.position;
        person.rotation = point.rotation;
    }
}
