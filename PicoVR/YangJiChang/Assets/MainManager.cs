using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum ViewMode
{
    firstView,
    //thirdView,
    flyView
}

public class MainManager : MonoBehaviour {
	Transform person;

    FirstPersonController firstPerson;
	FlyController flyController;

	ViewMode view;
	// Use this for initialization
	void Awake()
	{
		person = transform.Find("/FPSController");
		firstPerson = person.GetComponent<FirstPersonController>();
		flyController = person.GetComponent<FlyController>();
		view = ViewMode.firstView;
	}
	void Start () {
		flyController.enabled = false;
		firstPerson.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ModeSwitch()
	{
		view = view == ViewMode.firstView ? ViewMode.flyView : ViewMode.firstView;
		if(view == ViewMode.firstView)
		{
			firstPerson.enabled = true;
			flyController.enabled = false;
			positionSwitch(view);
		}
		else
		{
			firstPerson.enabled = false;
			flyController.enabled = true;
			positionSwitch(view);
		}
	}
	void positionSwitch(ViewMode view)
	{
		if(view == ViewMode.firstView)
		{
			person.position = new Vector3(person.position.x, 50f, person.position.z);
		}
		else
		{
			person.position = new Vector3(person.position.x, 0f, person.position.z);
		}
	}
}
