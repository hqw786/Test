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
        view.RegisterEvents();
        dicViews[view.Name] = view;
    }
    //注册C
    public static void RegisterControl(string eventName, System.Type controlType)
    {
        foreach (KeyValuePair<List<string>, List<System.Type>> c in dicControls)
        {
            if (c.Value.Contains(controlType))
            {
                c.Key.Add(eventName);
                return;
            }
            if (c.Key.Contains(eventName))
            {
                c.Value.Add(controlType);
                return;
            }
        }
        List<string> s = new List<string>();
        s.Add(eventName);
        List<System.Type> t = new List<System.Type>();
        t.Add(controlType);
        dicControls.Add(s, t);
    }
    //执行C或V
    public static void SendEvent(string eventName, object data = null)
    {
        Dictionary<List<string>, List<System.Type>> temp = new Dictionary<List<string>, List<System.Type>>(dicControls);
        foreach(KeyValuePair<List<string>,List<System.Type>> kvp in temp)
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
