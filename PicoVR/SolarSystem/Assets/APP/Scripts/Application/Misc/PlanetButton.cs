using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlanetButton : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    ShowPlanetView parent;
    Button button;

    Transform planet;
    CameraMove cameraMove;
    // Use this for initialization
	void Start () {
        parent = transform.parent.GetComponent<ShowPlanetView>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        planet = transform.Find("/SolarSystem/" + GetString(this.name));
        cameraMove = Camera.main.GetComponent<CameraMove>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnButtonClick()
    {
        //TODO:这边可以改成只发磅行星名称。（所有挂在物体上的脚本去继承View)
        parent.ButtonToDefault();
        button.image.sprite = button.spriteState.pressedSprite;
        //string planetName = GetString(this.name);
        //读取或显示星球信息
        parent.DisplayPlanetInfo(planet);
        //摄像机向所在星球移动
        cameraMove.CameraPositionMove(planet);
        //检查有没有卫星
        //有的话显示检查卫星，没有就不显示
        PlanetInfo pi = planet.GetComponent<PlanetInfo>();
        if(pi.isHaveSatellite)
        {
            Transform temp = parent.transform.Find("ShowPlanetInfo");
            Transform btnCheck = temp.Find("BtnCheckSatellite");
            btnCheck.gameObject.SetActive(true);
            //TODO:这边可以根据多个卫星生成多个按钮
        }
        else
        {
            Transform temp = parent.transform.Find("ShowPlanetInfo");
            Transform btnCheck = temp.Find("BtnCheckSatellite");
            btnCheck.gameObject.SetActive(false);
        }
    }
    string GetString(string name)
    {
        string temp = string.Empty;
        switch(name)
        {
            case "BtnSun":
                temp = "Sun";
                break;
            case "BtnMercury":
                temp = "Mercury";
                break;
            case "BtnVenus":
                temp = "Venus";
                break;
            case "BtnMoon":
                temp = "Moon";
                break;
            case "BtnEarth":
                temp = "Earth";
                break;
            case "BtnMars":
                temp = "Mars";
                break;
            case "BtnJupiter":
                temp = "Jupiter";
                break;
            case "BtnSaturn":
                temp = "Saturn";
                break;
            case "BtnUranus":
                temp = "Uranus";
                break;
            case "BtnNeptune":
                temp = "Neptune";
                break;
        }
        return temp;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name.Contains("Btn"))
        {
            eventData.pointerEnter.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (eventData.pointerEnter.transform.parent.name.Contains("Btn"))
        {
            eventData.pointerEnter.transform.parent.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name.Contains("Btn"))
        {
            eventData.pointerEnter.transform.localScale = new Vector3(1.2f, 1f, 1.2f);
        }
        if(eventData.pointerEnter.transform.parent.name.Contains("Btn"))
        {
            eventData.pointerEnter.transform.parent.localScale = new Vector3(1.2f, 1f, 1.2f);
        }
    }
}
