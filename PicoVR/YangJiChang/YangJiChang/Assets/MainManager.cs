using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;

public enum ViewMode
{
    firstView,
    //thirdView,
    flyView
}

public class MainManager : MonoBehaviour 
{
    public static MainManager Instance;
	Transform person;

    FirstPersonController firstPerson;
    //MoveController firstPerson;
    FlyController flyController;

	public ViewMode curView;
    public ViewMode lastView;
    Rigidbody rb;
    
	// Use this for initialization
	void Awake()
	{
        Instance = this;
        //person = transform.Find("/Person");
        person = transform.Find("/FPSController");
        rb = person.GetComponent<Rigidbody>();
        //rb.useGravity = false;
        //firstPerson = person.GetComponent<MoveController>();
        firstPerson = person.GetComponent<FirstPersonController>();
		//flyController = person.GetComponent<FlyController>();
        curView = ViewMode.firstView;
        
	}
	void Start () {
		//flyController.enabled = false;
		firstPerson.enabled = true;
        //Application.runInBackground = true;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// 视角切换
    /// </summary>
	public void ViewModeSwitch()
	{
        lastView = curView;
        curView = lastView == ViewMode.firstView ? ViewMode.flyView : ViewMode.firstView;
        if (curView == ViewMode.firstView)
		{
			//firstPerson.enabled = true;
			//flyController.enabled = false;
            positionSwitch(curView);
            //rb.useGravity = true;
		}
		else
		{
			//firstPerson.enabled = false;
			//flyController.enabled = true;
            positionSwitch(curView);
            //rb.useGravity = false;
		}
	}
    /// <summary>
    /// 位置切换
    /// </summary>
    /// <param name="view">视角模式</param>
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
