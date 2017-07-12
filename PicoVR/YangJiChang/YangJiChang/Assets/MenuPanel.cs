using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {
    Button btnViewSwitch;
    Button btnWeatherEffect;
    Button bttnSelectNewPosition;
    Button btnMineMap;
    Button btnHelp;
    Button btnExit;
	// Use this for initialization
	void Start () {
        btnViewSwitch = transform.Find("BtnViewSwitch").GetComponent<Button>();
        btnViewSwitch.onClick.AddListener(OnBtnViewSwitchClick);

        btnWeatherEffect = transform.Find("BtnWeatherEffect").GetComponent<Button>();
        btnWeatherEffect.onClick.AddListener(OnBtnWeatherEffectClick);

        bttnSelectNewPosition = transform.Find("BtnSelectNewPosition").GetComponent<Button>();
        bttnSelectNewPosition.onClick.AddListener(OnBtnSelectNewPositionClick);

        btnMineMap = transform.Find("BtnMineMap").GetComponent<Button>();
        btnMineMap.onClick.AddListener(OnBtnMineMapClick);

        btnHelp = transform.Find("BtnHelp").GetComponent<Button>();
        btnHelp.onClick.AddListener(OnBtnHelpClick);

        btnExit = transform.Find("BtnExit").GetComponent<Button>();
        btnExit.onClick.AddListener(OnBtnExitClick);
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
        if (UIManager.Instance.ActiveUI(Define.uiPanelWeather))
        {
            UIManager.Instance.HideUI(Define.uiPanelWeather);
        }
        else
        {
            UIManager.Instance.ShowUI(Define.uiPanelWeather);
        }
    }
    void OnBtnSelectNewPositionClick()
    {
        UIManager.Instance.ShowUI(Define.uiPanelNewPosition);
    }
    void OnBtnMineMapClick()
    {
        UIManager.Instance.ShowUI(Define.uiPanelMineMap);
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
