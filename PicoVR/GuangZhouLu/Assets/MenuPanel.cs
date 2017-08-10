using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler 
{
    
    //Button btnWeatherEffect;
    Button btnSelectNewPosition;
    //Button btnMineMap;
    Button btnHelp;
    Button btnPersonView;
    Button btnFlyView;
    Button btnAutoRoam;
    Button btnTransition;

    List<Button> viewKind = new List<Button>();
    List<Button> functionKind = new List<Button>();

    bool continuePressRoam;
    float timeContinuePressRoam;
    // Use this for initialization
	void Start () {
		GetComponent<RectTransform>().rect.size.Set(GetComponent<RectTransform>().rect.width*0.1f,GetComponent<RectTransform>().rect.height * Screen.width / 1920);

        //不改了。把这个隐藏起来
        //btnWeatherEffect = transform.Find("BtnWeatherEffect").GetComponent<Button>();
        //btnWeatherEffect.onClick.AddListener(OnBtnWeatherEffectClick);

        //不改了。把这个隐藏起来
        //btnMineMap = transform.Find("BtnMineMap").GetComponent<Button>();
        //btnMineMap.onClick.AddListener(OnBtnMineMapClick);

		btnPersonView = transform.Find("BtnPersonView").GetComponent<Button>();
		btnPersonView.onClick.AddListener(OnBtnPersonViewClick);

		btnFlyView = transform.Find("BtnFlyView").GetComponent<Button>();
		btnFlyView.onClick.AddListener(OnBtnFlyViewClick);

		btnAutoRoam = transform.Find("BtnAutoRoam").GetComponent<Button>();
		btnAutoRoam.onClick.AddListener(OnBtnAutoRoamClick);

        btnTransition = transform.Find("BtnTransition").GetComponent<Button>();
        btnTransition.onClick.AddListener(OnBtnTransitionClick);

		btnSelectNewPosition = transform.Find("BtnSelectNewPosition").GetComponent<Button>();
		btnSelectNewPosition.onClick.AddListener(OnBtnSelectNewPositionClick);

		btnHelp = transform.Find("BtnHelp").GetComponent<Button>();
		btnHelp.onClick.AddListener(OnBtnHelpClick);

		viewKind.Add(btnPersonView);
		viewKind.Add(btnFlyView);

        functionKind.Add(btnAutoRoam);
        functionKind.Add(btnSelectNewPosition);
        functionKind.Add(btnHelp);
        functionKind.Add(btnTransition);
        btnTransition.transform.Find("Image").gameObject.SetActive(false);

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
        //MainManager.Instance.roamPauseNum++;
        //if(MainManager.Instance.roamPauseNum >= ConfigData.Instance.roamPath.Count)
        //{
        //    MainManager.Instance.roamPauseNum = 0;
        //}

        MainManager.Instance.isAutoRoam = false;
        UIManager.Instance.HideUI(Define.uiPanelRoamView);
        MainManager.Instance.roamView = RoamView.custom;
        transform.Find("BtnAutoRoam/Image").gameObject.SetActive(false);
        MainManager.Instance.roamStatus = RoamStatus.pause;
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
        //if(MainManager.Instance.roamStatus == RoamStatus.pause)
        //{
        //    ButtonRestoreDefault(functionKind);
        //    MainManager.Instance.roamView = RoamView.fix;
        //    MainManager.Instance.roamStatus = RoamStatus.roaming;
        //    //中断漫游后，继续漫游
        //    if (MainManager.Instance.curView == ViewMode.firstView)
        //    {

        //        MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(MainManager.Instance.person.transform, MainManager.Instance.roamPauseNum);
        //    }
        //    else
        //    {

        //        MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(MainManager.Instance.person.transform, MainManager.Instance.roamPauseNum);
        //    }
        //    btnAutoRoam.transform.Find("Image").gameObject.SetActive(true);
        //}
        //else
            if (!MainManager.Instance.isAutoRoam)
        {
            //
            ButtonRestoreDefault(functionKind);
            //DONE:简化，先注释掉
            //UIManager.Instance.ShowUI(Define.uiPanelRoam);
            //DONE：简化，如果上面一行启用。这一段就注释掉
            #region 简化，如果要启用固定和可控视角。这一段就注释掉
            //MainManager.Instance.isAutoRoam = true;
            MainManager.Instance.roamView = RoamView.fix;
            MainManager.Instance.roamStatus = RoamStatus.roaming;
            if (MainManager.Instance.curView == ViewMode.firstView)
            {
                //从起点漫游
                //MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
                //匹配到最近的点开始漫游
                //MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(MateLateRoamPoint(MainManager.Instance.person.position), ConfigData.Instance.roamPath.Count - 1);
                //断点重游
                //MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(MainManager.Instance.roamPauseNum, ConfigData.Instance.roamPath.Count - 1);
                //Demo临时用
                MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(MainManager.Instance.roamPauseNum, ConfigData.Instance.roamPath.Count - 1);
            }
            else
            {
                //从起点漫游
                // MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
                //匹配到最近的点开始漫游
                //MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(MateLateRoamPoint(MainManager.Instance.person.position), ConfigData.Instance.roamPath.Count - 1);
                //断点重游
                //MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(MainManager.Instance.roamPauseNum, ConfigData.Instance.roamPath.Count - 1);
                //Demo临时用
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
    {//匹配最小距离的点作为起点漫游
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
    public void OnBtnTransitionClick()
    {
        btnTransition.transform.Find("Image").gameObject.SetActive(true);

        Transform t = transform.parent.Find("BG");
        t.gameObject.SetActive(true);
        t.GetComponent<Fade>().SetFadeOut();

        int a = SceneManager.GetActiveScene().buildIndex;
        if (a == 1)
        {
            ////保存位置和旋转信息
            MainManager.Instance.SavePositionAndRotation(MainManager.Instance.person.position,
                MainManager.Instance.person.rotation,
                MainManager.Instance.person.Find("Main Camera").localRotation,
                (int)MainManager.Instance.curView);
            
            Invoke("TransitionDayToNight", 1.5f);
        }
        else if (a == 2)
        {
            //保存位置和旋转信息
            MainManager.Instance.SavePositionAndRotation(MainManager.Instance.person.position,
                MainManager.Instance.person.rotation,
                MainManager.Instance.person.Find("Main Camera").localRotation,
                (int)MainManager.Instance.curView);

            Invoke("TransitionNightToDay", 1.5f);
        }
    }
    void TransitionDayToNight()
    {
        SceneManager.LoadScene(2);
    }
    void TransitionNightToDay()
    {
        SceneManager.LoadScene(1);
    }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(eventData.pointerEnter);
        //print("鼠标悬停在按钮上");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
