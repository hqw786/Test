using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BaseApplication<T> : Singleton<T> where T:MonoBehaviour
{
    //注册控制器
    protected void RegisterController(string eventName, ICommand c)
    {
        print("BaseApplication.RegisterController:  " + eventName);
        MVC.RegisterCommand(eventName, c);
    }
    //发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        print("BaseApplication.SendEvent:  " + eventName);
        MVC.SendEvent(eventName, data);
    }
}