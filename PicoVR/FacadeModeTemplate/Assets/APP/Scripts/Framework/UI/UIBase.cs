using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase
{
    //标识名称
    public abstract string Name{get;}
    //关联消息
    public List<UIMessageArgs> RelateMessage = new List<UIMessageArgs>();
    //注册关联消息
    public abstract void RegisterRelateMessage(string message);
    //处理关联消息
    public abstract void HandleMessage(string message);
}
