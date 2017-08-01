﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlowEgg : FlowBase
{
    List<GameObject> eggList = new List<GameObject>();
    
    public override void Enter()
    {
        //屏蔽基类的功能
        //base.Enter();
        
        //新的功能
        SYSManager.Instance.modelPoint.transform.rotation = Quaternion.identity;
        //蛋种类Panel渐渐显示,鸡模型隐藏，三种蛋显示出来
        int index = -1;
        foreach (GameObject g in ConfigData.Instance.dicEgg[(StageState)data.ID])
        {
            g.SetActive(true);
            g.transform.parent = SYSManager.Instance.modelPoint.transform;
            g.transform.rotation = Quaternion.identity;
            g.transform.localPosition = Vector3.zero + new Vector3(0.15f * index, 0f, 0f);
            g.transform.localScale = SYSManager.Instance.modelPoint.transform.localScale;
            if(g.GetComponent<BoxCollider>() == null)
                g.AddComponent<BoxCollider>();
            index++;
        }
        SYSManager.Instance.modelPoint.BroadcastMessage("SetObject", SendMessageOptions.DontRequireReceiver);
        SYSManager.Instance.modelPoint.BroadcastMessage("SetShow", SendMessageOptions.DontRequireReceiver);

        SYSManager.Instance.eggLaying.transform.Find("Egg").gameObject.SetActive(true);
    }
    public override void Exec()
    {
        //屏蔽基类的功能
        //base.Exec();

        //新的功能

    }
    public override void Exit()
    {
        //屏蔽基类的功能
        //base.Exit();

        //新的功能
        foreach (GameObject g in ConfigData.Instance.dicEgg[(StageState)data.ID])
        {
            g.SetActive(false);
        }
        SYSManager.Instance.eggLaying.transform.Find("Egg").gameObject.SetActive(false);
    }
}
