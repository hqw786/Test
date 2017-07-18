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
                MainManager.Instance.isAutoRoam = false;
                UIManager.Instance.HideUI(Define.uiPanelRoamView);
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
	}
    private void OnBtnPersonViewClick()
    {
        if (MainManager.Instance.curView != ViewMode.firstView)
        {
            ButtonRestoreDefault(viewKind);
            MainManager.Instance.ViewModeSwitch();
            btnPersonView.transform.Find("Image").gameObject.SetActive(true);
        }
    }
    void OnBtnFlyViewClick()
    {
        if (MainManager.Instance.curView != ViewMode.flyView)
        {
            ButtonRestoreDefault(viewKind);
            MainManager.Instance.ViewModeSwitch();
            btnFlyView.transform.Find("Image").gameObject.SetActive(true);
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
        ButtonRestoreDefault(functionKind);
        UIManager.Instance.ShowUI(Define.uiPanelRoam);
        btnAutoRoam.transform.Find("Image").gameObject.SetActive(true);
    }
    private void OnBtnViewSwitchClick()
    {
        MainManager.Instance.ViewModeSwitch();
    }
    void OnBtnWeatherEffectClick()
    {
        //if (UIManager.Instance.ActiveUI(Define.uiPanelWeather))
        //{
        //    UIManager.Instance.HideUI(Define.uiPanelWeather);
        //}
        //else
        //{
        //    UIManager.Instance.ShowUI(Define.uiPanelWeather);
        //}
    }
    void OnBtnSelectNewPositionClick()
    {
		//CloseAutoRoam();
        ButtonRestoreDefault(functionKind);
        UIManager.Instance.ShowUI(Define.uiPanelNewPosition);
        btnSelectNewPosition.transform.Find("Image").gameObject.SetActive(true);
    }
    void OnBtnMineMapClick()
    {
        //UIManager.Instance.ShowUI(Define.uiPanelMineMap);
    }
    void OnBtnHelpClick()
    {
		//CloseAutoRoam();
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
