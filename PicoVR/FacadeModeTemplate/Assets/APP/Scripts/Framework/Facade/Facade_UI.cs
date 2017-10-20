using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//完成内部
public partial class Facade     //UI
{
    UIManager uimanager;
	// Use this for initialization
    partial void UIInit()
    {
        
    }
    partial void HandleUI(string message, object arg)
    {
        if (message.Contains("Msg_UI_Score"))
        {
            uimanager.HandleMessage(message, null);
        }
    }
}
