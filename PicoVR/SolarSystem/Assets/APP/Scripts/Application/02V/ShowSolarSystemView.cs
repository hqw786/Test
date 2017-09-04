using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ShowSolarSystemView : View
{
    UIImageEffect uiie;
    UITextEffect uite;

    void Start () {
        uiie = GetComponent<UIImageEffect>();
        uite = transform.Find("Text").GetComponent<UITextEffect>();
        
	}
    public override string Name
    {
        get { return Consts.V_ShowSolarSystem; }
    }
    public override void RegisterEvents()
    {
        attentionEvents.Add(Consts.C_LatencyHiding);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.C_LatencyHiding:
                Hide();
                break;
        }
    }
    public void Hide()
    {
        Invoke("InvokeHide", 3f);
    }

    void InvokeHide()
    {
        uiie.SetAlphaOneWay(1f, 0f, 1.5f, true);
        uite.SetAlphaOneWay(1f, 0f, 1.5f);
    }
}
