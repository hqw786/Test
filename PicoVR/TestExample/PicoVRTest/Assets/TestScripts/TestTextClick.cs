using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestTextClick : ObjectEventListener
{
	// Use this for initialization
	void Start () {
        //对应的事件注册对应的方法
        EventTriggerBind(EventTriggerType.PointerClick, OnPointerClick);
        EventTriggerBind(EventTriggerType.PointerEnter, OnPointerEnter);
	}
	
	// Update is called once per frame
	void Update () {
		
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


    public void OnTextClick()
    {
        print("点击了TEXT");
    }
    public void OnTextEnter()
    {
        print("进入了TEXT");
    }
    public void OnMove()
    {
        print("移动了TEXT");
    }

}
