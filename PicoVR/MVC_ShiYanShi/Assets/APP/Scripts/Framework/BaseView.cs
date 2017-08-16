﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour //需要挂到游戏物体上
{

    //ID标识
    public abstract int ID { get; set; }
    //名称
    public abstract string Name { get; set; }
    //描述
    public abstract string Des { get; set; }

    //关心事件列表
    public List<string> AttentionEvents = new List<string>();
    //注册关心的事件
    public virtual void RegisterEvents()
    {
        
    }
    //获得M
    protected T GetModel<T>() where T:BaseModel
    {
        return MVC.GetModel<T>() as T;
    }
    //获得V
    protected T GetView<T>() where T:BaseView
    {
        return MVC.GetView<T>() as T;
    }
    //事件的处理
    public abstract void HandleEvent(string eventName, object data);
    //事件的发送
    protected void SendEvent(string eventName,object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
