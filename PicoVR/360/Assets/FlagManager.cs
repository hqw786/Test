using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {
    UIManager uimanager;
    List<Canvas> flagCanvas = new List<Canvas>();
	// Use this for initialization
    void Awake()
    {
       foreach(Transform t in transform)
       {
           flagCanvas.Add(t.GetComponent<Canvas>());
       }
    }
	void Start () {
		uimanager = transform.Find("/Canvas").GetComponent<UIManager>();
        CameraManager.CameraModeSwitchEvent += CanvasSwitchEventCamera;
	}
	
	// Update is called once per frame
	void Update () {
		if(!uimanager.curSkyboxName.Contains("cubemap11M"))
        {
            if(gameObject.activeInHierarchy)
                this.gameObject.SetActive(false);
        }
	}
    void CanvasSwitchEventCamera(CameraMode mode, Camera mc, Camera rc)
    {
        if(mode == CameraMode.Normal)
        {
            foreach(Canvas c in flagCanvas)
            {
                c.worldCamera = mc;
            }
        }
        else
        {
            foreach (Canvas c in flagCanvas)
            {
                c.worldCamera = rc;
            }
        }
    }
}
