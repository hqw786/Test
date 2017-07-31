using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pvr_UnitySDKAPI;

public class RayUI : MonoBehaviour {
    public Transform dot;

    EventSystem eventSystem;
    GraphicRaycaster graphicRaycaster;

    public Camera camera;

    bool isUITarget;
    bool isEggTarget; 
    GameObject targetUI;
    GameObject targetEgg;

    Ray ray;

    List<RaycastResult> result = new List<RaycastResult>();
	// Use this for initialization
	void Start () {
        //camera = GetComponent<Camera>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
        result = CheckGuiRaycastObject();
		if(result.Count > 0)
        {
            foreach(RaycastResult rr in result)
            {
                GameObject g = rr.gameObject;
                SetBool(g);                
                print("射线检测到UI:"+g.name);
                isUITarget = true;
                targetUI = g;
            }
        }
        else 
            isUITarget = false;
        if(isUITarget)
        {
            if(Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
            {
                SetButton(targetUI);
            }
        }
        else
        {
            ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(dot.position));
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,10f))
            {
                if(hit.collider.tag.Contains("Egg"))
                {
                    print(hit.collider.name);
                    dot.position = hit.point;
                    
                    isEggTarget = true;
                    targetEgg = hit.collider.gameObject;

                    targetEgg.GetComponent<EggScale>().SetScale();
                }
            }
            if(isEggTarget)
            {
                if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
                {
                    SetEggPress(targetEgg);
                }
            }
        }

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
    }
    List<RaycastResult> CheckGuiRaycastObject()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = camera.WorldToScreenPoint(dot.position);
        eventData.position = camera.WorldToScreenPoint(dot.position);

        List<RaycastResult> list = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, list);
        return list;//list.Count > 0;
    }
    void SetBool(GameObject g)
    {
        MenuController mc = g.transform.parent.parent.GetComponent<MenuController>();
        switch(g.name)
        {
            case "btnSelect":
                mc.isRayUISelect = true;
                break;
            case "Return":
                mc.isRayUIReturn = true;
                break;
            case "BtnEggLaying":
                //g.transform.localScale = Vector3.one * 1.2f;
                g.GetComponent<UIAlpha>().isRayUIEggLaying = true;
                break;
            case "Exit":
                mc.isRayUIExit = true;
                break;
        }
    }
    void SetButton(GameObject g)
    {
        switch (g.name)
        {
            case "btnSelect":
                SYSManager.Instance.ClickBook();
                break;
            case "Return":
                SYSManager.Instance.OnBtnReturnClick();
                break;
            case "BtnEggLaying":
                SYSManager.Instance.OnBtnEggLayingClick();
                break;
            case "Exit":
                SYSManager.Instance.OnBtnExitClick();
                break;
        }
    }
    public void SetEggPress(GameObject g)
    {
        if(g.name.Contains("jidan_02"))
        {
            PressEgg(g);
            SYSManager.Instance.OnBtnRedClick();
        }
        else if(g.name.Contains("jidan_03"))
        {
            PressEgg(g);
            SYSManager.Instance.OnBtnGreenClick();
        }
        else if(g.name.Contains("jidan_01"))
        {
            PressEgg(g);
            SYSManager.Instance.OnBtnPinkClick();
        }
    }
    void PressEgg(GameObject g)
    {
        foreach( Transform t in g.transform.parent)
        {
            if(t.gameObject.name == g.name)
            {
                t.GetComponent<EggScale>().keepScale();
            }
            else
            {
                if(t.gameObject.activeInHierarchy)
                    t.GetComponent<EggScale>().resetKeeyScale();
            }
        }
    }

    public void SetFodderPress(GameObject g)
    {
        if (g.name.Contains("fodder1"))
        {
            PressFodder(g);
            SYSManager.Instance.OnBtnFodder1Click();
        }
        else if (g.name.Contains("fodder2"))
        {
            PressFodder(g);
            SYSManager.Instance.OnBtnFodder2Click();
        }
        else if (g.name.Contains("fodder3"))
        {
            PressFodder(g);
            SYSManager.Instance.OnBtnFodder3Click();
        }
    }
    void PressFodder(GameObject g)
    {
        foreach (Transform t in g.transform.parent)
        {
            if (t.gameObject.name == g.name)
            {
                t.GetComponent<EggScale>().keepScale();
            }
            else
            {
                if (t.gameObject.activeInHierarchy)
                    t.GetComponent<EggScale>().resetKeeyScale();
            }
        }
    }
}
