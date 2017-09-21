using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCubeClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Controller.UPvr_GetControllerState() == ControllerState.Connected)
        {
            if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.APP))
                print("  APP IS PRESS  ");
        }
        if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideLeft))
        {
        }
        if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideRight))
        {
            Pvr_UnitySDKManager.pvr_UnitySDKSensor.ResetUnitySDKSensor();
        }


        if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.APP))
            print("  APP IS PRESS  ");
        if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.HOME))
            print("  HOME IS PRESS  ");
        if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0))
        {
            print("  TOUCHPAD IS PRESS  ");
            //PicoVRManager.SDK.ResetHeadTrack();//以当前方向设置为Y轴0度位，Pvr_UnitySDK和Head的Rotation的Y值为0，画面抖动厉害，应该换个方式
            //
            if (Pvr_UnitySDKManager.SDK != null)
            {
                //以当前方向初始化为Y轴0度位（原始位），其实是以Head的方向为准，将Pvr_UnitySDK的方向调整到Head方向，Head的Y轴角度变为0度。
                Pvr_UnitySDKManager.pvr_UnitySDKSensor.ResetUnitySDKSensor();
            }
        }
        if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.VOLUMEDOWN))
            print("  Vdown IS PRESS  ");
        if (Controller.UPvr_GetKeyDown(Pvr_KeyCode.VOLUMEUP))
            print("  Vup IS PRESS  ");
	}
    public void OnCubeClick()
    {
        print("点击了CUBE");
    }
}
