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
        MainManager.Instance.firstPerson.isRotation = true;
    }
    public void OnToggleCustomChange()
    {
        MainManager.Instance.roamView = RoamView.custom;
    }
}
