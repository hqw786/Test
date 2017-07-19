﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoamPanel : MonoBehaviour ,IPointerClickHandler
{
    // Use this for initialization
    void Awake()
    {

    }
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnBtnWholeClick()
	{
		MainManager.Instance.roamView = RoamView.fix;

		if (MainManager.Instance.curView == ViewMode.firstView)
		{
			MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
		}
		else
		{
			MainManager.Instance.flyController.SetAutoRoamStartAndEndPoint(0, ConfigData.Instance.roamPath.Count - 1);
		}

        if (!UIManager.Instance.IsActive(Define.uiPanelRoamView))
        {
            UIManager.Instance.ShowUI(Define.uiPanelRoamView);
        }
		gameObject.SetActive(false);
	}
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
