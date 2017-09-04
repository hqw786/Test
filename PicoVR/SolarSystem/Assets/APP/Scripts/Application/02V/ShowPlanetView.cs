using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlanetView : View
{
    List<Button> buttonList = new List<Button>();
    void Start()
    {
        foreach(Transform temp in transform)
        {
            if(temp.name.Contains("Btn"))
            {
                buttonList.Add(temp.GetComponent<Button>());
            }
        }
    }
    public override string Name
    {
        get { return Consts.V_ShowPlanet; }
    }
    public override void RegisterEvents()
    {
        attentionEvents.Add("Consts.C_ShowPlanet");
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.C_ShowPlanet:
                print("好像没什么用啊");
                break;
        }
    }
}
