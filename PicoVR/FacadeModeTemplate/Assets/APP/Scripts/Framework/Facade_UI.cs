using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//完成内部
public partial class Facade
{
    UIManager uimanager;
	// Use this for initialization

    partial void Handle(string message)
    {
        if (message.Contains("Msg_UI_"))
        {
            uimanager.HandleMessage(message);
        }

        //对象池不用事件，这里改成其他功能的消息
        if (message.Contains("Msg_Pool_"))
        {
            //
        }
    }
}
