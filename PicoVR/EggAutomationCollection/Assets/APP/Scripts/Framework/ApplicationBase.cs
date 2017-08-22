using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ApplicationBase<T> : Singleton<T> where T : MonoBehaviour
{
    //注册控制器
    protected void RegisterControl(string eventName,Type controlType)
    {
        MVC.RegisterControl(eventName, controlType);
    }
    //发送事件
    protected void SendEvent(string eventName,object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
