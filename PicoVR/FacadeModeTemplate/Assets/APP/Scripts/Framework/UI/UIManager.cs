using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singelation<UIManager>
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }
    //注册消息事件
    public static event System.Action RegisterMessageEvent;
    //保存各主要UI界面
    public List<UIBase> uis = new List<UIBase>();
    /// <summary>
    /// 默认构造函数
    /// </summary>
    private UIManager()
    {
        //子模块实例化，并加到保存界面集合中，所有的子模块都要保存
        Test_UIScore score = new Test_UIScore();
        uis.Add(score);

        //全部保存后，注册消息
        RegisterMessage();
    }
    /// <summary>
    /// 注册消息
    /// </summary>
    public void RegisterMessage()
    {
        if(RegisterMessageEvent != null)
        {
            RegisterMessageEvent();
        }
    }
    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    public void HandleMessage(string message)
    {
        foreach(UIBase b in uis)
        {
            b.HandleMessage(message);
        }
    }

    //UI的几种显示方式（全部隐藏，显示某一个，还是用栈来控制）
}
