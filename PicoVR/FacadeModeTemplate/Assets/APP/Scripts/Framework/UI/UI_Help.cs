using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


class UI_Help : UIBase
{
    GameObject topHintPrefab;
    Transform parent;

    public UI_Help()
    {
        topHintPrefab = Resources.Load<GameObject>("Prefabs/UI/Help/TopHintPanel");
        parent = GameObject.Find("/Canvas/HelpPanel").transform;
    }

    public override string Name
    {
        get { return Consts.UI_Help; }
    }

    public override void RegisterRelateMessage()
    {
        UIMessageArgs ma = new UIMessageArgs();
        ma.Name = Consts.Msg_UI_Help_TopHint;
        ma.CallBack += DisplayTopHint;
        RelateMessage.Add(ma);
    }

    public override void HandleMessage(string message, object arg)
    {
        foreach (UIMessageArgs ma in RelateMessage)
        {
            if (ma.Name.Contains(message))
            {
                ma.CallBack(arg);
            }
        }
    }
    void DisplayTopHint(object arg)
    {
        GameObject g = GameObject.Instantiate(topHintPrefab) as GameObject;

    }
}