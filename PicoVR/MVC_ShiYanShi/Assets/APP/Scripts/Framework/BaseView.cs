using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour //需要挂到游戏物体上
{

    //ID标识
    //public abstract int ID { get; set; }
    protected int ID { get; set; }
    //名称
    //public abstract string Name { get; set; }
    public abstract string Name { get; }
    //描述
    //public abstract string Des { get; set; }
    protected string Des { get; set; }

    //关心事件列表
    public List<string> AttentionEvents = new List<string>();
    //注册关心的事件
    public virtual void RegisterEvents()
    {
        print("BaseView.RegisterEvent:  " + "空");
    }
    //获得M
    protected T GetModel<T>() where T:BaseModel
    {
        print("BaseView.GetModel:  ");
        return MVC.GetModel<T>() as T;
    }
    //获得V
    protected T GetView<T>() where T:BaseView
    {
        print("BaseView.GetView:  ");
        return MVC.GetView<T>() as T;
    }
    //事件的处理
    public abstract void HandleEvent(string eventName, object data);
    //事件的发送
    public void SendEvent(string eventName,object data = null)
    {
        print("BaseView.SendEvent:  " + eventName);
        MVC.SendEvent(eventName, data);
    }
}
