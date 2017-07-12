using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour {
    Button btnExit;
    Button btnReturn;

	// Use this for initialization
	void Start () {
        btnExit = transform.Find("BtnExit").GetComponent<Button>();
        btnReturn = transform.Find("BtnReturn").GetComponent<Button>();
        btnExit.onClick.AddListener(OnBtnExitClick);
        btnReturn.onClick.AddListener(OnBtnReturnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnBtnExitClick()
    {
        Application.Quit();
    }
    void OnBtnReturnClick()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.ShowUI(Define.uiPanelMenu);
    }
}
