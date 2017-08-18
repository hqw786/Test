using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Control
{
    //注册M
    protected void RegisterModel(BaseModel model)
    {
        MVC.RegisterModel(model);
    }
    //注册V
    protected void RegisterView(BaseView view)
    {
        MVC.RegisterView(view);
    }
    //注册C
    protected void RegisterController(string eventName, ICommand c)
    {
        MVC.RegisterCommand(eventName, c);
    }
    //获得M
    public T GetModel<T>() where T : BaseModel
    {
        return MVC.GetModel<T>() as T;
    }
    //获得V
    public T GetView<T>() where T:BaseView
    {
        return MVC.GetView<T>() as T;
    }
    ////执行
    //public abstract void Execute(object data);
}
