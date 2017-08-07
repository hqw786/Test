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
    GameObject mineMapPanel;
    GameObject helpPanel;
    GameObject exitPanel;
    GameObject roamPanel;
    GameObject roamViewPanel;
    GameObject showImagePanel;
    GameObject titlePanel;
    List<GameObject> uis = new List<GameObject>();

    bool isFirst = true;
	// Use this for initialization
    void Awake()
    {
        Instance = this;

        menuPanel = transform.Find("MenuPanel").gameObject;
        //weatherPanel = transform.Find("WeatherPanel").gameObject;
        newPositionPanel = transform.Find("NewPositionPanel").gameObject;
        mineMapPanel = transform.Find("MineMapPanel").gameObject;
        helpPanel = transform.Find("HelpPanel").gameObject;
        exitPanel = transform.Find("ExitPanel").gameObject;
        roamPanel = transform.Find("RoamPanel").gameObject;
        roamViewPanel = transform.Find("RoamViewPanel").gameObject;
        showImagePanel = transform.Find("ShowImagePanel").gameObject;
        //titlePanel = transform.Find("TitlePanel").gameObject;

        uis.Add(roamPanel);
        uis.Add(menuPanel);
        //uis.Add(weatherPanel);
        uis.Add(newPositionPanel);
        uis.Add(mineMapPanel);
        uis.Add(helpPanel);
        uis.Add(exitPanel);
        uis.Add(roamViewPanel);
        uis.Add(showImagePanel);
        //uis.Add(titlePanel);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//DONE:简化，先注释掉
		//if(MainManager.Instance.isAutoRoam)
		//{
		//	if (!roamViewPanel.activeInHierarchy)
		//	{
		//		roamViewPanel.SetActive(true);
		//	}
		//}
		//else
		//{
		//	if (roamViewPanel.activeInHierarchy)
		//	{
		//		roamViewPanel.SetActive(false);
		//	}
		//}

        //下面这个功能是提示帮助画面，第一次使用时
        //if (isFirst)
        //{
        //    if (!IsActive(Define.uiPanelTitle))
        //    {
        //        isFirst = false;
        //        ShowUI(Define.uiPanelHelp);
        //    }
        //}
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
    public void ShowUI(string s,bool b = false)
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
                    if (g.name.Contains(Define.uiPanelMineMap))
                    {
                        if (!MainManager.Instance.isShowMineMap)
                        {
                            g.SetActive(false);
                        }
                    }
                    else
                    {
                        if(!b) g.SetActive(false);
                    }
                }
            }
            else
            {
                //g.SetActive(true);
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
                case "ShowImagePanel":
                    {
                        if (g.activeInHierarchy)
                            return g;
                    }
                    break;
            }
            
        }
        return null;
    }
}
