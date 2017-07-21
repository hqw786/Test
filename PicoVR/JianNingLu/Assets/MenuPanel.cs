using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {
    //Button btnWeatherEffect;
    Button btnSelectNewPosition;
    //Button btnMineMap;
    Button btnHelp;
    Button btnPersonView;
    Button btnFlyView;
    Button btnAutoRoam;

    List<Button> viewKind = new List<Button>();
    List<Button> functionKind = new List<Button>();
    // Use this for initialization
	void Start () {
		GetComponent<RectTransform>().rect.size.Set(GetComponent<RectTransform>().rect.width*0.1f,GetComponent<RectTransform>().rect.height * Screen.width / 1920);

        //不改了。把这个隐藏起来
        //btnWeatherEffect = transform.Find("BtnWeatherEffect").GetComponent<Button>();
        //btnWeatherEffect.onClick.AddListener(OnBtnWeatherEffectClick);

        //不改了。把这个隐藏起来
        //btnMineMap = transform.Find("BtnMineMap").GetComponent<Button>();
        //btnMineMap.onClick.AddListener(OnBtnMineMapClick);

		btnSelectNewPosition = transform.Find("BtnSelectNewPosition").GetComponent<Button>();
		btnSelectNewPosition.onClick.AddListener(OnBtnSelectNewPositionClick);

		btnHelp = transform.Find("BtnHelp").GetComponent<Button>();
		btnHelp.onClick.AddListener(OnBtnHelpClick);

		btnPersonView = transform.Find("BtnPersonView").GetComponent<Button>();
		btnPersonView.onClick.AddListener(OnBtnPersonViewClick);

		btnFlyView = transform.Find("BtnFlyView").GetComponent<Button>();
		btnFlyView.onClick.AddListener(OnBtnFlyViewClick);

		btnAutoRoam = transform.Find("BtnAutoRoam").GetComponent<Button>();
		btnAutoRoam.onClick.AddListener(OnBtnAutoRoamClick);

		viewKind.Add(btnPersonView);
		viewKind.Add(btnFlyView);

        functionKind.Add(btnAutoRoam);
        functionKind.Add(btnSelectNewPosition);
        functionKind.Add(btnHelp);
        btnPersonView.transform.Find("Image").gameObject.SetActive(true);
	}
    // Update is called once per frame
    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //按下ESC键
            //判断是否漫游状态
            if (MainManager.Instance.isAutoRoam)
            {
                ExitRoam();
            }
            else//判断是否有UI打开
            {
                GameObject temp = UIManager.Instance.IsActive();
                if (temp != null)
                {
                    temp.SetActive(false);
                }
                else//打开退出画面
                {
                    OnBtnExitClick();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(MainManager.Instance.curView == ViewMode.firstView)
            {
                OnBtnFlyViewClick();
            }
            else
            {
                OnBtnPersonViewClick();
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            OnBtnAutoRoamClick();
        }
	}
    public void ExitRoam()
    {
        MainManager.Instance.isAutoRoam = false;
        UIManager.Instance.HideUI(Define.uiPanelRoamView);
        MainManager.Instance.roamView = RoamView.custom;
        transform.Find("BtnAutoRoam/Image").gameObject.SetActive(false);
    }
    private void OnBtnPersonViewClick()
    {
        if (MainManager.Instance.curView != ViewMode.firstView)
        {
            ButtonRestoreDefault(viewKind);
            MainManager.Instance.ViewModeSwitch();
            btnPersonView.transform.Find("Image").gameObject.SetActive(true);
            if(MainManager.Instance.isAutoRoam)
            {
                MainManager.Instance.flyController.SwitchToPerson();
                //MainManager下面有些参数要设置一下，如：固定视角等
            }
        }
    }
    void OnBtnFlyViewClick()
    {
        if (MainManager.Instance.curView != ViewMode.flyView)
        {
            ButtonRestoreDefault(viewKind);
            MainManager.Instance.ViewModeSwitch();
            btnFlyView.transform.Find("Image").gameObject.SetActive(true);
            if (MainManager.Instance.isAutoRoam)
            {
                MainManager.Instance.firstPerson.SwitchToFly();
            }
        }
    }
    void ButtonRestoreDefault(List<Button> kind)
    {
        foreach(Button b in kind)
        {
            b.transform.Find("Image").gameObject.SetActive(false);
        }
    }
    void OnBtnAutoRoamClick()
    {
        if (!MainManager.Instance.isAutoRoam)
        {
            ButtonRestoreDefault(functionKind);
            //DONE:简化，先注释掉
            //UIManager.Instance.ShowUI(Define.uiPanelRoam);
            //DONE：简化，如果上面一行启用。这一段就注释掉
            #region 简化，如果要启用固定和可控视角。这一段就注释掉
            //MainManager.Instance.isAutoRoam = true;
            MainManager.Instance.roamView = RoamView.fix;
            if (MainManager.Instance.curView == ViewMode.firstView)
            {
                //从起点漫游
                //MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
                //匹配到最近的点开始漫游
                //MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(MateLateRoamPoint(MainManager.Instance.person.position), ConfigData.Instance.roamPath.Count - 1);
                //断点重游
                MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(MainManager.Instance.roamPauseNum, ConfigData.Instance.roamPath.Count - 1);
            }
            else
            {
                //从起点漫游
                // MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
                //匹配到最近的点开始漫游
                //MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(MateLateRoamPoint(MainManager.Instance.person.position), ConfigData.Instance.roamPath.Count - 1);
                //断点重游
                MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(MainManager.Instance.roamPauseNum, ConfigData.Instance.roamPath.Count - 1);
            }
            #endregion

            btnAutoRoam.transform.Find("Image").gameObject.SetActive(true);
        }
        else
        {
            ExitRoam();
        }
    }
    int MateLateRoamPoint(Vector3 position)
    {
        float dis = 0f;
        float minDis = 0f;
        int temp = 0;
        for (int i = 0; i < ConfigData.Instance.roamPath.Count; i++)
        {
            dis = Vector3.Distance(position, ConfigData.Instance.roamPath[i].position);
            if(i == 0)
            {
                minDis = dis;
            }
            if (minDis > dis)
            {
                minDis = dis;
                temp = i;
            }
        }
        if(temp == ConfigData.Instance.roamPath.Count - 1)
        {
            temp = 0;
        }
        return temp;
    }
    //private void OnBtnViewSwitchClick()
    //{
    //    MainManager.Instance.ViewModeSwitch();
    //}
    //void OnBtnWeatherEffectClick()
    //{
    //    //if (UIManager.Instance.ActiveUI(Define.uiPanelWeather))
    //    //{
    //    //    UIManager.Instance.HideUI(Define.uiPanelWeather);
    //    //}
    //    //else
    //    //{
    //    //    UIManager.Instance.ShowUI(Define.uiPanelWeather);
    //    //}
    //}
    void OnBtnSelectNewPositionClick()
    {
        ButtonRestoreDefault(functionKind);
        UIManager.Instance.ShowUI(Define.uiPanelNewPosition);
        btnSelectNewPosition.transform.Find("Image").gameObject.SetActive(true);
    }
    //void OnBtnMineMapClick()
    //{
    //    //UIManager.Instance.ShowUI(Define.uiPanelMineMap);
    //}
    void OnBtnHelpClick()
    {
        ButtonRestoreDefault(functionKind);
        UIManager.Instance.ShowUI(Define.uiPanelHelp);
        btnHelp.transform.Find("Image").gameObject.SetActive(true);
    }
    void OnBtnExitClick()
    {
        if (!UIManager.Instance.IsActive(Define.uiPanelExit))
        {
            UIManager.Instance.ShowUI(Define.uiPanelExit);
        }
        else
        {
            UIManager.Instance.HideUI(Define.uiPanelExit);
        }
    }
}
