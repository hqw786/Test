using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Pvr_ControllerRayCheck : MonoBehaviour 
{
    public static event System.EventHandler RayCheckEvent;
    public static event System.EventHandler RayPressEvent;
    //手柄射出射线的终点标志
    public Transform dot;
    //对射线检测有反应的标志
    public Transform flag;
    //手柄射出射线的方向
    public Transform direction;
    //发出射线的摄像机
    public Camera camera;
    //定义事件管理对象
    EventSystem eventSystem;
    //图形化射线，对UI使用
    GraphicRaycaster graphicRaycaster;
    //射线对UI检测结果    
    List<RaycastResult> result = new List<RaycastResult>();
    //射线是否检测到物体
    bool isDetectedObject;
    //射线检测到的物体目标
    Transform checkingObject;
    //按下按键之后目标固定
    Transform targetObject;
    //射线
    Ray ray;
	// Use this for initialization
    void Awake()
    {
        //获取摄像头
        Transform t = transform.parent.Find("Head");
        camera = t.GetComponent<Camera>();
        //终点标志
        dot = transform.Find("dot");
        //检测到标识
        flag = transform.Find("point");
        //射线方向
        direction = transform.Find("direction");
        //可检测UI的射线
        graphicRaycaster = transform.Find("/Canvas").GetComponent<GraphicRaycaster>();
    }
	void Start () {
		ray = new Ray();
		ray.origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //射线检测到UI
        if(CheckRaycastUI())
        {
            //对结果进行分析
            UIChecking();
            //射线检测到UI，按下确认键，执行点击事件
            if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
            {
                //UI被按下
                UIPressDown();
            }
        }
        else //射线没有检测到UI
        {
            //射线检测到物体
            if(CheckRaycastObject())
            {
                //
                ObjectChecking();
                if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
                {
                    ObjectPressDown();
                }
            }
        }
        if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(1))
        {
            //PicoVRManager.SDK.ResetHeadTrack();
        }
	}

    /// <summary>
    /// 射线是否检测到UI
    /// </summary>
    /// <returns></returns>
    bool CheckRaycastUI()
    {
        //点击事件产生的数据
        PointerEventData eventData = new PointerEventData(eventSystem);
        //按下的位置
        eventData.pressPosition = camera.WorldToScreenPoint(dot.position);
        //当前点击（指针）位置
        eventData.position = camera.WorldToScreenPoint(dot.position);
        //清空上次检测UI结果
        //List<RaycastResult> list = new List<RaycastResult>();
        result.Clear();
        //图形射线发射线检测
        graphicRaycaster.Raycast(eventData, result);
        //返回一个bool值，表示有没有检测到UI
		//把这个抽出来做成一个主法，（参数：hit.point）
		if(result.Count> 0)
		{
			flag.gameObject.SetActive(true);
			//只能在UI的中心位置生成一个圆圈
			flag.transform.position = result[0].gameObject.transform.position + new Vector3(0, 0, -0.1f);
			flag.DOKill();
			flag.DOScale(0.025f, 0.5f);
			dot.gameObject.SetActive(false);
		}
		else
		{
			flag.DOScale(0.0f, 0.2f);
			flag.gameObject.SetActive(false);
			dot.gameObject.SetActive(true);
		}
        return result.Count > 0;
    }

    /// <summary>
    /// 对结果进行分析
    /// </summary>
    void UIChecking()
    {
        //遍历射线检测到的UI
        if (result.Count > 1)
        {
            Debug.LogError("这里需要修改，可以同时扫到两个UI");
        }
        else
        {
            print("射线检测到UI:" + result[0].gameObject.name);

            RayCheckArgs e = new RayCheckArgs();
            e.checkObject = result[0].gameObject;
            OnRayCheckEvent(e);

            //TODO: 对检测到的UI进行焦点效果操作（缩放，变颜色，换材质等）
            //switch (result[0].gameObject.tag)
            //{
            //    case "aaa":
            //        ScaleObject(result[0].gameObject);
            //        break;
            //    case "bbb":
            //        ColorObject(result[0].gameObject);
            //        break;
            //    case "ccc":
            //        MaterialObject(result[0].gameObject);
            //        break;
            //}
        }
    }

    void OnRayCheckEvent(RayCheckArgs e)
    {
        if(RayCheckEvent != null)
        {
            RayCheckEvent(this, e);
        }
    }

    void OnRayPressEvent(RayCheckArgs e)
    {
        if(RayPressEvent != null)
        {
            RayPressEvent(this, e);
        }
    }
    void UIPressDown()
    {
        //TODO: 对检测到的UI进行焦点效果操作（缩放，变颜色，换材质等）
        switch (result[0].gameObject.name)
        {
            case "aaa":
                ScaleObject(result[0].gameObject);
                break;
            case "bbb":
                ColorObject(result[0].gameObject);
                break;
            case "ccc":
                MaterialObject(result[0].gameObject);
                break;
        }
    }
    void ScaleObject(GameObject g)
    {
        g.BroadcastMessage("",SendMessageOptions.DontRequireReceiver);
    }
    void ColorObject(GameObject g)
    {
        g.BroadcastMessage("", SendMessageOptions.DontRequireReceiver);
    }
    void MaterialObject(GameObject g)
    {
        g.BroadcastMessage("", SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// 射线检测2D，3D物体
    /// </summary>
    /// <returns></returns>
    bool CheckRaycastObject()
    {
        //ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(dot.position));//这个应该是凝视的射线
        ray.direction = direction.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            isDetectedObject = true;
            checkingObject = hit.collider.transform;

            flag.gameObject.SetActive(true);
            flag.transform.position = hit.point + new Vector3(0, 0, -0.1f);
            flag.DOKill();
            flag.DOScale(0.025f, 0.5f);
            dot.gameObject.SetActive(false);

            return true;
        }
        isDetectedObject = false;
        checkingObject = null;

        flag.DOScale(0.0f, 0.2f);
        flag.gameObject.SetActive(false);
        dot.gameObject.SetActive(true);

        return false;
    }
    /// <summary>
    /// 射线检测某层中的2D，3D物体
    /// </summary>
    /// <param name="lm"></param>
    /// <returns></returns>
    bool CheckRaycastObject(LayerMask lm)
    {
        ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(dot.position));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, lm))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 射线检测2D，3D物体的效果
    /// </summary>
    void ObjectChecking()
    {
        RayCheckArgs e = new RayCheckArgs();
        e.checkObject = checkingObject.gameObject;
        OnRayCheckEvent(e);

        //产生效果
        if(isDetectedObject)
        {
            switch(checkingObject.name)
            {
                case "aaa":
                    break;
                case"bbb":
                    break;
                case "ccc":
                    break;
            }
        }
    }
    /// <summary>
    /// 物体被点击的操作
    /// </summary>
    void ObjectPressDown()
    {
        RayCheckArgs e = new RayCheckArgs();
        e.checkObject = checkingObject.gameObject;
        OnRayPressEvent(e);
        //pointer.
        //checkingObject.GetComponent<>().OnPointerClick();
    }
    /// <summary>
    /// 物体被选择的操作
    /// </summary>
    /// <param name="g"></param>
    void ObjectPressDown(GameObject g)
    {
        targetObject = checkingObject.transform;
    }
}
