using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamViewPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnToggleFixChange()
    {
        MainManager.Instance.roamView = RoamView.fix;
        if (MainManager.Instance.curView == ViewMode.firstView)
        {
            MainManager.Instance.firstPerson.isVRotation = true;
        }
        else
        {
            MainManager.Instance.flyController.isVRotation = true;
        }
    }
    public void OnToggleCustomChange()
    {
        MainManager.Instance.roamView = RoamView.custom;
    }
}
