using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCompanyLogoView : View {

    UIImageEffect uiie;
    UITextEffect uite;

    void Awake()
    {
        uiie = transform.Find("LogoImg").GetComponent<UIImageEffect>();
        uite = transform.Find("LogoText").GetComponent<UITextEffect>();
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
        Games.Instance.MainStatusSwitch(MainGameStatus.title);
    }
    public override void HandleEvent(string eventName, object data)
    {
        uiie.SetAlphaOneWay(0f, 1f, 1.5f);
        uite.SetAlphaOneWay(0f, 1f, 1.5f);
        Invoke("Hide", 6.5f);
    }

    public override void RegisterEvents()
    {
        attentionEvents.Add(Consts.C_ShowCompanyLogo);
    }

    public override string Name
    {
        get { return Consts.V_ShowCompanyLogo; }
    }
}
