using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetButton : MonoBehaviour {
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
        parent.ButtonToDefault();
        button.image.sprite = button.spriteState.pressedSprite;
        string planetName = GetString(this.name);
        //读取或显示星球信息
        parent.DisplayPlanetInfo(planetName);
        //摄像机向所在星球移动
        cameraMove.CameraPositionMove(planet);
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

}
