using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//完成内部
public partial class Facade     //Help
{
    partial void HelpInit()//这个没什么用哦，先留着吧
    {
        
    }
    partial void HandleHelp(string message, object arg)
    {
        if(message.Contains("Msg_UI_Help_"))
        {
            uimanager.HandleMessage(message, arg);
        }
    }

}
