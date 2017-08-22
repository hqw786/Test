using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//中间类（存储）
public static class MVC
{
    private static Dictionary<string, Model> dicModels = new Dictionary<string, Model>();
    private static Dictionary<string, View> dicViews = new Dictionary<string, View>();
    private static Dictionary<List<string>, List<System.Type>> dicControls = new Dictionary<List<string>, List<System.Type>>();
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
    public static void RegisterControl(string eventName, System.Type controlType)
    {
        foreach (KeyValuePair<List<string>, List<System.Type>> c in dicControls)
        {
            if(c.Value.Contains(controlType))
            {
                c.Key.Add(eventName);
                break;
            }
            if(c.Key.Contains(eventName))
            {
                c.Value.Add(controlType);
                break;
            }
        }
        ////如果上面的遍历字典并修改出错，就用下面这段代码
        //Dictionary<List<string>, List<System.Type>> temp = new Dictionary<List<string>, List<System.Type>>(dicControls);
        //foreach(KeyValuePair<List<string>, List<System.Type>> kvp in temp)
        //{
        //    if(kvp.Value.Contains(controlType))
        //    {
        //        kvp.Key.Add(eventName);
        //        break;
        //    }
        //    if(kvp.Key.Contains(eventName))
        //    {
        //        kvp.Value.Add(controlType);
        //        break;
        //    }
        //}
    }
    //执行C或V
    public static void SendEvent(string eventName, object data = null)
    {
        foreach(KeyValuePair<List<string>,List<System.Type>> kvp in dicControls)
        {
            if(kvp.Key.Contains(eventName))
            {
                foreach(System.Type type in kvp.Value)
                {
                    Control c = System.Activator.CreateInstance(type) as Control;
                    c.Execute(data);
                }
            }
        }
        foreach(View v in dicViews.Values)
        {
            if(v.attentionEvents.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }
}
