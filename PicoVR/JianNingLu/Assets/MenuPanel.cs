using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {
    //Button btnViewSwitch;
    //Button btnWeatherEffect;
    Button btnSelectNewPosition;
    //Button btnMineMap;
    Button btnHelp;
    Button btnExit;
    Button btnPersonView;
    Button btnFlyView;
    Button btnAutoRoam;

    List<Button> viewKind = new List<Button>();
    // Use this for initialization
	void Start () {
		GetComponent<RectTransform>().rect.size.Set(GetComponent<RectTransform>().rect.width*0.1f,GetComponent<RectTransform>().rect.height * Screen.width / 1920);

        //不改了。把这个隐藏起来
        //btnViewSwitch = transform.Find("BtnViewSwitch").GetComponent<Button>();
        //btnViewSwitch.onClick.AddListener(OnBtnViewSwitchClick);

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

		btnExit = transform.Find("BtnExit").GetComponent<Button>();
		btnExit.onClick.AddListener(OnBtnExitClick);

		btnPersonView = transform.Find("BtnPersonView").GetComponent<Button>();
		btnPersonView.onClick.AddListener(OnBtnPersonViewClick);

		btnFlyView = transform.Find("BtnFlyView").GetComponent<Button>();
		btnFlyView.onClick.AddListener(OnBtnFlyViewClick);

		btnAutoRoam = transform.Find("BtnAutoRoam").GetComponent<Button>();
		btnAutoRoam.onClick.AddListener(OnBtnAutoRoamClick);

		viewKind.Add(btnPersonView);
		viewKind.Add(btnFlyView);
		viewKind.Add(btnAutoRoam);
	}

    private void OnBtnPersonViewClick()
    {
        if (MainManager.Instance.curView != ViewMode.firstView)
        {
            MainManager.Instance.ViewModeSwitch();
        }
    }
    void OnBtnFlyViewClick()
    {
        if (MainManager.Instance.curView != ViewMode.flyView)
        {
            MainManager.Instance.ViewModeSwitch();
        }
    }
    void OnBtnAutoRoamClick()
    {
        UIManager.Instance.ShowUI(Define.uiPanelRoam);
    }
    // Update is called once per frame
	void Update () 
    {
		
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
        UIManager.Instance.ShowUI(Define.uiPanelNewPosition);
    }
    void OnBtnMineMapClick()
    {
        //UIManager.Instance.ShowUI(Define.uiPanelMineMap);
    }
    void OnBtnHelpClick()
    {
        UIManager.Instance.ShowUI(Define.uiPanelHelp);
    }
    void OnBtnExitClick()
    {
        UIManager.Instance.ShowUI(Define.uiPanelExit);
        UIManager.Instance.HideUI(Define.uiPanelMenu);
    }
}
