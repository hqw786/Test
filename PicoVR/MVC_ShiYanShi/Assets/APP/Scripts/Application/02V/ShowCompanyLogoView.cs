using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEngine;


public class ShowCompanyLogoView : BaseView
{
    UIImageEffect uiie;
    UITextEffect uite;

    void Awake()
    {
        uiie = transform.Find("LogoImg").GetComponent<UIImageEffect>();
        uite = transform.Find("LogoText").GetComponent<UITextEffect>();
    }
    void Start()
    {
        SendEvent(Consts.C_ShowCompanyLogo);
    }
    void Hide()
    {
        uiie.SetAlphaOneWay(1f, 0f, 1.5f);
        uite.SetAlphaOneWay(1f, 0f, 1.5f);
        Invoke("NextScene",1.5f);
    }
    void NextScene()
    {
        int i = Game.Instance.NextScene();
        Game.Instance.LoadScene(++i);
    }
    public override void HandleEvent(string eventName, object data)
    {
        uiie.SetAlphaOneWay(0f, 1f, 1.5f);
        uite.SetAlphaOneWay(0f, 1f, 1.5f);
        Invoke("Hide",6.5f);
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Name);
    }

    public override string Name
    {
        get { return  Consts.V_ShowCompanyLogo; }
    }
}
