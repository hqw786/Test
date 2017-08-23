using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTitleView : View {
    UIImageEffect uiie;
    UITextEffect uite;

    void Awake()
    {
        uiie = transform.Find("TitleImage").GetComponent<UIImageEffect>();
        uite = transform.Find("TitleText").GetComponent<UITextEffect>();
    }
    void Start()
    {
    }
    void Hide()
    {
        uiie.SetAlphaOneWay(1f, 0f, 1.5f);
        uite.SetAlphaOneWay(1f, 0f, 1.5f);
        Invoke("NextScene", 1.5f);
    }
    void NextScene()
    {
        Games.Instance.MainStatusSwitch(MainGameStatus.menu);
    }
    public override void HandleEvent(string eventName, object data)
    {
        print("ShowCompanyLogoView.HandleEvent:  " + eventName);
        uiie.SetAlphaOneWay(0f, 1f, 1.5f);
        uite.SetAlphaOneWay(0f, 1f, 1.5f);
        Invoke("Hide", 6.5f);
    }

    public override void RegisterEvents()
    {
        print("ShowCompanyLogoView.RegisterEvents:  " + Consts.C_ShowTitle);
        attentionEvents.Add(Consts.C_ShowTitle);
    }

    public override string Name
    {
        get { return Consts.V_ShowTitle; }
    }
}
