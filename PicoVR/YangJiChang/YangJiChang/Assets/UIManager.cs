using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance;

    GameObject menuPanel;
    GameObject weatherPanel;
    GameObject newPositionPanel;
    GameObject mineMapPanel;
    GameObject helpPanel;
    GameObject exitPanel;
    List<GameObject> uis = new List<GameObject>();

	// Use this for initialization
    void Awake()
    {
        Instance = this;

        menuPanel = transform.Find("MenuPanel").gameObject;
        weatherPanel = transform.Find("WeatherPanel").gameObject;
        newPositionPanel = transform.Find("NewPositionPanel").gameObject;
        mineMapPanel = transform.Find("MineMapPanel").gameObject;
        helpPanel = transform.Find("HelpPanel").gameObject;
        exitPanel = transform.Find("ExitPanel").gameObject;
        uis.Add(menuPanel);
        uis.Add(weatherPanel);
        uis.Add(newPositionPanel);
        uis.Add(mineMapPanel);
        uis.Add(helpPanel);
        uis.Add(exitPanel);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool ActiveUI(string s)
    {
        foreach(GameObject g in uis)
        {
            if(g.name.Contains(s))
            {
                return g.activeInHierarchy;
            }
        }
        return false;
    }
    public void ShowUI(string s)
    {
        foreach(GameObject g in uis)
        {
            if (!g.name.Contains("Menu"))
            {
                if (g.name.Contains(s))
                {
                    g.SetActive(true);
                }
                else
                {
                    g.SetActive(false);
                }
            }
            else
            {
                g.SetActive(true);
            }
        }
    }
    public void HideUI(string s)
    {
        foreach (GameObject g in uis)
        {
            if (g.name.Contains(s))
            {
                g.SetActive(false);
            }
        }
    }
}
