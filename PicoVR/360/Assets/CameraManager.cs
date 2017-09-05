using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CameraMode
{
    Normal,VR
}
public class CameraManager : MonoBehaviour {
    CameraMode mode;
    GameObject normalCamera;
    GameObject vrCamera;

	// Use this for initialization
	void Start () {
        mode = CameraMode.Normal;
        normalCamera = transform.Find("Main Camera").gameObject;
        vrCamera = transform.Find("Head").gameObject;
        UIManager.SwitchCameraEvent += SwitchCameraMode;
	}
    //切换摄像机
    private void SwitchCameraMode()
    {
        if(mode == CameraMode.Normal)
        {
            mode = CameraMode.VR;
            SwitchCameraToVR();
        }
        else
        {
            mode = CameraMode.Normal;
            SwitchCameraToNormal();
        }
    }
    void SwitchCameraToNormal()
    {
        normalCamera.SetActive(true);
        vrCamera.SetActive(false);
    }
	void SwitchCameraToVR()
    {
        normalCamera.SetActive(false);
        vrCamera.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}

}
