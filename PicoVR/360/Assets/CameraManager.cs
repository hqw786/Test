using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CameraMode
{
    Normal,VR
}
public class CameraManager : MonoBehaviour {
    public static event System.Action<CameraMode, Camera, Camera> CameraModeSwitchEvent;
    public Transform vrModeText;
    public CameraMode mode;
    GameObject normalGameObject;
    GameObject vrGameObject;

    Transform normalCamera;
    Transform vrCamera;

    Camera mainCamera;
    Camera leftCamera;
    Camera rightCamera;
	// Use this for initialization
	void Start () {
        mode = CameraMode.Normal;
        normalGameObject = transform.Find("MainCamera").gameObject;
        vrGameObject = transform.Find("VRCamera").gameObject;
        UIManager.SwitchCameraEvent += SwitchCameraMode;

        normalCamera = normalGameObject.transform.Find("Main Camera");
        vrCamera = vrGameObject.transform.Find("Head");

        mainCamera = normalCamera.GetComponent<Camera>();
        leftCamera = vrCamera.Find("LeftCamera").GetComponent<Camera>();
        rightCamera = vrCamera.Find("RightCamera").GetComponent<Camera>();
	}
    //切换摄像机
    public void SwitchCameraMode()
    {
        if(mode == CameraMode.Normal)
        {
            mode = CameraMode.VR;
            vrModeText.gameObject.SetActive(true);
            SwitchCameraToVR();
        }
        else
        {
            mode = CameraMode.Normal;
            vrModeText.gameObject.SetActive(false);
            SwitchCameraToNormal();
        }
        if(CameraModeSwitchEvent != null)
        {
            CameraModeSwitchEvent(mode, mainCamera, rightCamera);
        }
    }
    void SwitchCameraToNormal()
    {
        normalGameObject.SetActive(true);
        vrGameObject.SetActive(false);
        //缩放，旋转，跟随上一个状态
        normalGameObject.transform.rotation = vrGameObject.transform.rotation;
        normalCamera.transform.rotation = vrCamera.transform.rotation;
        mainCamera.fieldOfView = leftCamera.fieldOfView;
    }
	void SwitchCameraToVR()
    {
        normalGameObject.SetActive(false);
        vrGameObject.SetActive(true);
        //缩放，旋转，跟随上一个状态
        vrGameObject.transform.rotation = normalGameObject.transform.rotation;
        vrCamera.transform.rotation = normalCamera.transform.rotation;
        leftCamera.fieldOfView = mainCamera.fieldOfView;
        rightCamera.fieldOfView = mainCamera.fieldOfView;
    }

	// Update is called once per frame
	void Update () {
		
	}

}
