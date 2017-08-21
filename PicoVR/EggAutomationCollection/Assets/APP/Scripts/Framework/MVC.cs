using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//中间类（存储）
public static class MVC
{
    private static Dictionary<string, Model> dicModels = new Dictionary<string, Model>();
    private static Dictionary<string, View> dicViews = new Dictionary<string, View>();
    private static Dictionary<List<string>, List<Control>> dicControls = new Dictionary<List<string>, List<Control>>();
    //获得M
    public static Model GetModel<T>() where T : Model
    {
        foreach(Model m in dicModels.Values)
        {
            if(m is T)
            {
                return m;
            }
        }
        return null;
    }
    //获得V
    public static View GetView<T>() where T : View
    {
        foreach(View v in dicViews.Values)
        {
            if(v is T)
            {
                return v;
            }
        }
        return null;
    }
    //注册M
    public static void RegisterModel(Model model)
    {
        dicModels[model.Name] = model;
    }
    //注册V
    public static void RegisterView(View view)
    {
        if(dicViews.ContainsKey(view.Name))
        {
            dicViews.Remove(view.name);
        }

    }
    //注册C
    public static void RegisterControl(string eventName, Control c)
    {
        foreach(Control control in dicControls)
        {
            if()
        }
    }
    //执行C或V
}
