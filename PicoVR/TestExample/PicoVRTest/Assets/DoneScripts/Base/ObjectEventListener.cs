using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.VR;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//public enum 

public class ObjectEventListener : EventTrigger 
{
    protected void EventTriggerBind(EventTriggerType ETType, UnityAction<BaseEventData> func)
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        if(!eventTrigger)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }
        //实例化delegates
        if(eventTrigger.triggers == null)
            eventTrigger.triggers = new List<EventTrigger.Entry>();

        //foreach (EventTriggerType t in ETType)
        {
            //定义所要绑定的事件类型
            EventTrigger.Entry entry = new EventTrigger.Entry();
            //设置事件类型
            entry.eventID = ETType;//EventTriggerType.PointerClick;
            //设置回调函数
            entry.callback = new EventTrigger.TriggerEvent();
            //这就是委托
            UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(func);
            entry.callback.AddListener(callback);
            //添加事件触发记录到GameObject的事件触发组件
            eventTrigger.triggers.Add(entry);
        }
    }
}
