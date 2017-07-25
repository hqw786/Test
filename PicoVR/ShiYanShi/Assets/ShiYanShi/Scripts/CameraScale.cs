using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour {
    public GameObject pause;
    Camera camera;
    Vector3 originPosition;
    bool isForward;
    bool isFast;
	// Use this for initialization
	void Start () {
        originPosition = transform.parent.position;
        camera = GetComponent<Camera>();
        pause.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideUp)||Input.GetKeyDown(KeyCode.W))
        {
            //往前移一点
            if (!isForward)
            {
                isForward = true;
                camera.transform.parent.position = camera.transform.parent.position + camera.transform.forward;
            }
        }
        if (Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideDown) ||Input.GetKeyDown(KeyCode.S))
        {
            //回原点
            camera.transform.parent.position = originPosition;
            isForward = false;
        }
        if(Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideLeft) || Input.GetKeyDown(KeyCode.A))
        {
            Time.timeScale = 1f;
            isFast = false;
            pause.SetActive(false);
        }
        if(Controller.UPvr_GetSlipDirection(Pvr_SlipDirection.SlideRight) || Input.GetKeyDown(KeyCode.D))
        {
            if(!isFast)
            {
                isFast = true;
                Time.timeScale = 0f;
                pause.SetActive(true);
            }
        }
	}
    public void ReturnOriginPosition()
    {
        camera.transform.parent.position = originPosition;
    }
}
