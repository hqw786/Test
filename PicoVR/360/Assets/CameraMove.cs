using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoamMode
{
    Normal,Roam
}
public class CameraMove : MonoBehaviour {
    public float speed;
    RoamMode roamMode;
    bool isRoam;
	// Use this for initialization
	void Start () {
        roamMode = RoamMode.Normal;
        UIManager.SwitchRoamEvent += SwitchRoamMode;	
	}
    void SwitchRoamMode(Image image)
    {
        //image.color = Color.white;
        if(roamMode == RoamMode.Normal)
        {
            roamMode = RoamMode.Roam;
            image.color = new Color(4f / 255f, 158f / 255f, 249f / 255f);
            isRoam = true;
        }
        else
        {
            roamMode = RoamMode.Normal;
            image.color = Color.white;
            isRoam = false;
        }
    }
	// Update is called once per frame
	void Update () {
		if(isRoam)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
	}
    void LateUpdate()
    {

    }
}
