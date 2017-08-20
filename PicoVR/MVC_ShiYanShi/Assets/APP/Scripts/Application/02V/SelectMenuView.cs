using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


class SelectMenuView : BaseView
{
    GameObject selectPanel;
    
    public override string Name
    {
        get { return Consts.V_SelectMenu; }
    }

    void Awake()
    {
        selectPanel = transform.Find("Select").gameObject;

        selectPanel.transform.Find("btnSelect1").GetComponent<Button>().onClick.AddListener(OnBtnSelect1Click);
        selectPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(OnBtnExitClick);
    }


    public override void HandleEvent(string eventName, object data)
    {
        print("SelectMenuView.HandleEvent:  " + eventName);
        switch(eventName)
        {
            case Consts.C_Rotation90:
                {
                    //恢复方向，朝向书架
                    Quaternion q = Quaternion.identity;
                    q = Quaternion.Euler(new Vector3(0, -90, 0));
                    GameObject.Find("Pvr_UnitySDK").transform.rotation = q;
                    //重置摄像头

                }
                break;
        }
    }

    public override void RegisterEvents()
    {
        print("SelectMenuView.RegisterEvents:  " + Consts.C_SelectMenu);
        AttentionEvents.Add(Consts.C_Rotation90);

    }

    void OnBtnSelect1Click()
    {
        print("蛋鸡成长过程");
        Quaternion q = Quaternion.identity;
        GameObject.Find("Pvr_UnitySDK").transform.rotation = q;
    }

    void OnBtnExitClick()
    {
        print("退出程序");
        Application.Quit();
    }
}
