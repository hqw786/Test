using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pvr_UnitySDKAPI;

public class RayUIStage : MonoBehaviour {
    public Transform dot;

    EventSystem eventSystem;
    GraphicRaycaster graphicRaycaster;

    StageSelect stageSelect;

    public Camera camera;

    public bool isUITarget;
    bool isStageTarget; 
    GameObject targetUI;
    GameObject targetStage;

    Ray ray;

    List<RaycastResult> result = new List<RaycastResult>();

    public event System.Action<GameObject> SelectedStageEvent;

	// Use this for initialization
	void Start () {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        stageSelect = GetComponent<StageSelect>();
	}
	
	// Update is called once per frame
    void Update()
    {
        result = CheckGuiRaycastObject();
        if (result.Count > 0)
        {
            foreach (RaycastResult rr in result)
            {
                GameObject g = rr.gameObject;
                print("射线检测到UI:" + g.name);
                isUITarget = true;
                targetUI = g;
                ExecuteSelectedEvent(targetUI);
            }
        }
        else
        {
            isUITarget = false;
        }
        if (isUITarget)
        {
            if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
            {
                SetStagePress(targetUI);
            }
        }
    }
            //ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(dot.position));
            //RaycastHit hit;
            //if(Physics.Raycast(ray,out hit,10f))
            //{
            //    if (hit.collider.name.Contains("BtnStage"))
            //    {
            //        //print(hit.collider.name);
            //        dot.position = hit.point;

            //        isStageTarget = true;
            //        targetStage = hit.collider.gameObject;
            //        //过时，使物体缩放
            //        //targetEgg.GetComponent<EggScale>().SetScale();
            //        //新
            //        scaleButton(targetStage);
            //    }
            //}
            //if(isStageTarget)
            //{
            //    if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
            //    {
            //        SetStagePress(targetStage);
            //    }
            //}
        //}

        #region 手柄控制例子
        //if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideLeft))
        //{

        //}
        //if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideRight))
        //{

        //}
        //if (Controller.UPvr_GetControllerState() == ControllerState.Connected)
        //{
        //    text.text = "手柄已连接";
        //    text.text += Controller.UPvr_GetControllerQUA();
        //    text.text += Controller.UPvr_IsTouching();
        //    text.text += Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideUp);
        //    text.text += Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideDown);
        //    text.text += Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideLeft);
        //    text.text += Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideRight);
        //    if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.APP))
        //        text.text += "  APP IS PRESS  ";
        //}
        //if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideLeft))
        //{
        //    text.text += ("left is press");
        //}
        //if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.APP))
        //    text.text += "  APP IS PRESS  ";
        //if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.HOME))
        //    text.text += "  HOME IS PRESS  ";
        //if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD))
        //    text.text += "  TOUCHPAD IS PRESS  ";
        //if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.VOLUMEDOWN))
        //    text.text += "  Vdown IS PRESS  ";
        //if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.VOLUMEUP))
        //    text.text += "  Vup IS PRESS  ";
        #endregion
    //}
    List<RaycastResult> CheckGuiRaycastObject()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = camera.WorldToScreenPoint(dot.position);
        eventData.position = camera.WorldToScreenPoint(dot.position);

        List<RaycastResult> list = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, list);
        return list;//list.Count > 0;
    }

    public void SetStagePress(GameObject g)
    {
        StageButton sb = g.GetComponent<StageButton>();
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.pointerEnter = g;
        sb.OnPointerClick(ped);
    }
    public void ExecuteSelectedEvent(GameObject g)
    {
        if(SelectedStageEvent != null)
        {
            SelectedStageEvent(g);
        }
    }
}
