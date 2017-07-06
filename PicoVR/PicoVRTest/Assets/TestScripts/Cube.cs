using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;


//这个是挂在能引起射线反应的物体上，继承自ObjectEventListener（继承自EventTrigger，增加一个绑定方法），通过在Start方法中注册事件触发对应的方法
public class Cube : ObjectEventListener
{
    void Start()
    {
        //对应的事件注册对应的方法
        EventTriggerBind(EventTriggerType.PointerClick, OnPointerClick);
        EventTriggerBind(EventTriggerType.PointerEnter, OnPointerEnter);
    }
    public void OnPointerClick(BaseEventData eventData)
    {
        //base.OnPointerClick(arg0);
        print("点击了，这是派生类中发出的消息");
    }
    public void OnPointerEnter(BaseEventData eventData)
    {
        print("移入了，这是覆盖方法中发出的消息");
    }
}