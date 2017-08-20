using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//模型和视图都可以发送事件，模型的视图来接收，视图的控制器来接收（优先执行控制器再执行视图）
public static class MVC  //中间者（用于存储和事件的发送）
{
    //存储
    public static Dictionary<string, BaseModel> dicModels = new Dictionary<string, BaseModel>();
    public static Dictionary<string, BaseView> dicViews = new Dictionary<string, BaseView>();
    public static Dictionary<string, ICommand> dicCommandMap = new Dictionary<string, ICommand>();
    //注册M
    public static void RegisterModel(BaseModel model)
    {
        dicModels[model.Name] = model;
    }
    //注册V
    public static void RegisterView(BaseView view)
    {
        Debug.Log("MVC.RegisterView:  " + view);
        if(dicViews.ContainsKey(view.Name))
        {
            dicViews.Remove(view.Name);
        }
        view.RegisterEvents();
        dicViews[view.Name] = view;
    }
    //注册C
    public static void RegisterCommand(string eventName,ICommand c)
    {
        Debug.Log("MVC.RegisterCommand:  " + eventName);
        dicCommandMap[eventName] = c;
    }
    //获得M
    public static BaseModel GetModel<T>() where T:BaseModel
    {
        Debug.Log("MVC.GetModel:  ");
        foreach(BaseModel m in dicModels.Values)
        {
            if(m is T)
            {
                return m;
            }
        }
        return null;
    }
    //获得V
    public static BaseView GetView<T>() where T:BaseView
    {
        Debug.Log("MVC.GetModel:  ");
        foreach(BaseView v in dicViews.Values)
        {
            if(v is T)
            {
                return v;
            }
        }
        return null;
    }
    //执行C
    public static void SendEvent(string eventName, object data = null)
    {
        Debug.Log("MVC.SendEvent:  " + eventName);
        //先执行C
        if(dicCommandMap.ContainsKey(eventName))
        {
            ICommand c = dicCommandMap[eventName];
            c.Execute(data);
        }
        //后执行V
        foreach(BaseView v in dicViews.Values)
        {
            if(v.AttentionEvents.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }
    //
}
