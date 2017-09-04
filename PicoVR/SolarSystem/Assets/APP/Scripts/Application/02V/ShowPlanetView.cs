using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlanetView : View
{
    List<Button> buttonList = new List<Button>();

    Text planetContent;
    Text planetName;

    public static event Action CameraReturnToDefaultEvent;
    void Start()
    {
        foreach(Transform temp in transform)
        {
            if(temp.name.Contains("Btn"))
            {
                buttonList.Add(temp.GetComponent<Button>());
            }
        }
        planetName = transform.Find("ShowPlanetInfo/PlanetNameBG/PlanetNameText").GetComponent<Text>();
        planetContent = transform.Find("ShowPlanetInfo/PlanetInfoText").GetComponent<Text>();
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

    public void ButtonToDefault()
    {
        foreach(Button b in buttonList)
        {
            b.image.sprite = b.spriteState.disabledSprite;
        }
    }
    public void DisplayPlanetInfo(string planetName)
    {
        TextAsset txt = Resources.Load<TextAsset>("Config/" + planetName);
        string[] content = txt.text.Split("\n"[0]);
        this.planetName.text = txt.text.Substring(0, txt.text.IndexOf("\n"));
        planetContent.text = txt.text.Substring(txt.text.IndexOf("\n"));
    }

    public void OnBtnReturnClick()
    {
        gameObject.SetActive(false);
        //摄像头还原到星系位置
        if(CameraReturnToDefaultEvent != null)
        {
            CameraReturnToDefaultEvent();
        }
    }
}
