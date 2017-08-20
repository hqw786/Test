using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Control
{
    //注册M
    protected void RegisterModel(BaseModel model)
    {
        Debug.Log("Control.RegisterModel:  " + model);
        MVC.RegisterModel(model);
    }
    //注册V
    protected void RegisterView(BaseView view)
    {
        Debug.Log("Control.RegisterView:  " + view);
        MVC.RegisterView(view);
    }
    //注册C
    protected void RegisterController(string eventName, ICommand c)
    {
        Debug.Log("Control.RegisterController:  " + eventName);
        MVC.RegisterCommand(eventName, c);
    }
    //获得M
    public T GetModel<T>() where T : BaseModel
    {
        Debug.Log("Control.GetModel:  ");
        return MVC.GetModel<T>() as T;
    }
    //获得V
    public T GetView<T>() where T:BaseView
    {
        Debug.Log("Control.GetModel:  ");
        return MVC.GetView<T>() as T;
    }
    ////执行
    //public abstract void Execute(object data);
}
