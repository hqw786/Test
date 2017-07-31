using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject btnExit;
    public GameObject btnSelect;
    public GameObject btnReturn;
    public GameObject stageMenu;
    //public GameObject btnRestart;

    public bool isRayUISelect;
    public bool isRayUIReturn;
    public bool isRayUIExit;
    //public bool isRayUIRestart;
    //public GameObject 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnSelect.name))
        {
            scaleButton(btnSelect);
        }
        else if (Pvr_GazeInputModule.gazeGameObject == null)
        {
            if (isRayUISelect)
            {
                isRayUISelect = false;
                scaleButton(btnSelect);
            }
            else
            {
                resetScaleButton(btnSelect);
            }
        }
        if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnExit.name))
        {
            scaleButton(btnExit);
        }
        else if (Pvr_GazeInputModule.gazeGameObject == null)
        {
            if (isRayUIExit)
            {
                isRayUIExit = false;
                scaleButton(btnExit);
            }
            else
            {
                resetScaleButton(btnExit);
            }
        }
        if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnReturn.name))
        {
            scaleButton(btnReturn);
        }
        else if (Pvr_GazeInputModule.gazeGameObject == null)
        {
            if (isRayUIReturn)
            {
                isRayUIReturn = false;
                scaleButton(btnReturn);
            }
            else
            {
                resetScaleButton(btnReturn);
            }
        }
	}

    public void scaleButton(GameObject g)
    {
        g.transform.localScale = Vector3.one * 1.15f;
        g.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
    public void resetScaleButton(GameObject g)
    {
        g.transform.localScale = Vector3.one;
        g.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
    }
}
