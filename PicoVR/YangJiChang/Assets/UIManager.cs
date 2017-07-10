using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameObject menuPanel;
    GameObject weatherPanel;
    GameObject newPositionPanel;
    GameObject mineMapPanel;
    GameObject helpPanel;
    GameObject exitPanel;



	// Use this for initialization
    void Awake()
    {
        menuPanel = transform.Find("MenuPanel").gameObject;
        weatherPanel = transform.Find("WeatherPanel").gameObject;
        newPositionPanel = transform.Find("NewPositionPanel").gameObject;
        mineMapPanel = transform.Find("MineMapPanel").gameObject;
        helpPanel = transform.Find("HelpPanel").gameObject;
        exitPanel = transform.Find("ExitPanel").gameObject;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
