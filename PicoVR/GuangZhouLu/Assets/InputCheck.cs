using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputCheck : BaseInputModule
{
    public static InputCheck Instance;
    public static bool isUI;
    void Awake()
    {
        Instance = this;
    }

    public override void Process()
    {
        List<RaycastResult> list = base.m_RaycastResultCache;
        if(list.Count > 0)
        {
            print("LIST.COUNT > 0");
        }
        foreach(RaycastResult rr in list)
        {
            if(rr.gameObject.transform.parent.name.Contains("MenuPanel"))
            {
                isUI = true;
                print("MenuPanel");
            }
        }
    }
    public static bool ProcessUI()
    {
        Instance.Process();
        return isUI;
    }
}