using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	//栈
	public static Stack<Transform> showStack = new Stack<Transform>();
    public static UIManager Instance;

    GameObject menuPanel;
    //GameObject weatherPanel;
    GameObject newPositionPanel;
    //GameObject mineMapPanel;
    GameObject helpPanel;
    GameObject exitPanel;
    GameObject roamPanel;
    GameObject roamViewPanel;
    List<GameObject> uis = new List<GameObject>();

	// Use this for initialization
    void Awake()
    {
        Instance = this;

        menuPanel = transform.Find("MenuPanel").gameObject;
        //weatherPanel = transform.Find("WeatherPanel").gameObject;
        newPositionPanel = transform.Find("NewPositionPanel").gameObject;
        //mineMapPanel = transform.Find("MineMapPanel").gameObject;
        helpPanel = transform.Find("HelpPanel").gameObject;
        exitPanel = transform.Find("ExitPanel").gameObject;
        roamPanel = transform.Find("RoamPanel").gameObject;
        roamViewPanel = transform.Find("RoamViewPanel").gameObject;
        uis.Add(roamPanel);
        uis.Add(menuPanel);
        //uis.Add(weatherPanel);
        uis.Add(newPositionPanel);
        //uis.Add(mineMapPanel);
        uis.Add(helpPanel);
        uis.Add(exitPanel);
        uis.Add(roamViewPanel);
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
    public bool IsActive(string s)
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
    public GameObject IsActive()
    {
        foreach(GameObject g in uis)
        {
            switch(g.name)
            {
                case "RoamPanel":
                    {
                        if (g.activeInHierarchy)
                        {
                            return g;
                        }
                    }
                    break;
                case "NewPositionPanel":
                    {
                        if (g.activeInHierarchy)
                        {
                            return g;
                        }
                    }
                    break;
                case "HelpPanel":
                    {
                        if (g.activeInHierarchy)
                        {
                            return g;
                        }
                    }
                    break;
            }
            
        }
        return null;
    }
}
