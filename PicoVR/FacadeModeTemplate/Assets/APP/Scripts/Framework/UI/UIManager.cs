using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  说明：
 *  1  在构造函数中实例化子功能对象，并保存起来，最后注册消息
 *  2  处理消息，转发到子功能的处理消息方法处理，子功能还可以再转发到具体作用类的处理消息处理。
 *  3  
 */
public class UIManager : SingelationT<UIManager>
{
    //注册消息事件
    public static event System.Action RegisterMessageEvent;//各子功能类的默认构造函数中添加方法
    //保存各主要UI界面
    public List<UIBase> uis = new List<UIBase>();//把继承自UIBase的主要UI界面都保存下来
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public UIManager()
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
