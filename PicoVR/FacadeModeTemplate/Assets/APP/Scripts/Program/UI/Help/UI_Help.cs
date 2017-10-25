using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


class UI_Help : UIBase
{
    GameObject topHintPrefab;
    GameObject toolTipPrefab;
    GameObject toolTipToTaskPrefab;

    Transform parent;

    public UI_Help()
    {
        UIManager.RegisterMessageEvent += RegisterRelateMessage;

        topHintPrefab = Resources.Load<GameObject>("Prefabs/UI/Help/TopHintPanel");
        toolTipPrefab = Resources.Load<GameObject>("Prefabs/UI/Help/ToolTipPanel");
        toolTipToTaskPrefab = Resources.Load<GameObject>("Prefabs/UI/Help/ToolTipToTaskPanel");

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

        UIMessageArgs ma1 = new UIMessageArgs();
        ma1.Name = Consts.Msg_UI_Help_ToolTip;
        ma1.CallBack += DisplayToolTip;
        RelateMessage.Add(ma1);
    }

    public override void HandleMessage(string message, object arg)
    {
        foreach (UIMessageArgs ma in RelateMessage)
        {
            if (ma.Name.Contains(message))
            {
                ma.CallBack(message, arg);
                break;
            }
        }
    }
    void DisplayTopHint(string message, object arg)
    {
        if (message.Contains(Consts.Msg_UI_Help_TopHint))
        {
            GameObject g = GameObject.Instantiate(topHintPrefab) as GameObject;
            g.transform.parent = parent;
            g.transform.localScale = Vector3.one;
            g.transform.localPosition = new Vector3(0, 1080 * 0.35f, 0);

            HelpArgs ha = arg as HelpArgs;
            Text text = g.GetComponentInChildren<Text>();
            text.text = ha.TopContext;
        }
    }
    void DisplayToolTip(string message, object arg)
    {
        if (message.Contains(Consts.Msg_UI_Help_ToolTip))
        {
            GameObject g = GameObject.Instantiate(toolTipPrefab) as GameObject;
            g.transform.parent = parent;
            g.transform.localScale = Vector3.one;
            g.transform.localPosition = new Vector3(1920 * 0.2f, 1080 * 0.25f, 0);

            HelpArgs ha = arg as HelpArgs;
            Text text = g.GetComponentInChildren<Text>();
            text.text = ha.context;
        }
    }
    void DisplayToolTipToTask(string message, object arg)
    {
        GameObject g = GameObject.Instantiate(toolTipToTaskPrefab) as GameObject;
        g.transform.parent = parent;
        g.transform.localScale = Vector3.one;
        g.transform.localPosition = new Vector3(0, 1080 * 0.35f, 0);
    }
}