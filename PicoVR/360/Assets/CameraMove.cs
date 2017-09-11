using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoamMode
{
    Normal,Roam
}
public class CameraMove : MonoBehaviour {
    public static event System.Action<float> CameraScaleEvent;
    
    public float speed;
    [HideInInspector]
    public RoamMode roamMode;
    public bool isRoam;

    float FOVOf = 90f;
    

    Transform camera;

    UIManager uimanager;

	// Use this for initialization
	void Start () {
        roamMode = RoamMode.Normal;
        uimanager = transform.Find("/Canvas").GetComponent<UIManager>();
        UIManager.SwitchRoamEvent += SwitchRoamMode;
        if(transform.name.Contains("VRCamera"))
        {
            camera = transform.Find("Head");
        }
        else
        {
            camera = transform.Find("Main Camera");
        }
	}
    void SwitchRoamMode(Image image)
    {
        //image.color = Color.white;
        if(roamMode == RoamMode.Normal)
        {
            roamMode = RoamMode.Roam;
            image.color = new Color(0f, 128f / 255f, 1f);
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
    void Update()
    {
        if (isRoam)
        {
            if(Input.anyKeyDown)
            {
                uimanager.ClickIcon("Roam");
            }
            if (camera.rotation.eulerAngles.x >= 0.001f)
            {
                Quaternion q = camera.localRotation;
                float x = Mathf.LerpAngle(q.eulerAngles.x, 0f, Time.deltaTime * 5f);
                Quaternion end = Quaternion.Euler(new Vector3(x, 0, 0));
                camera.localRotation = end;
            }
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                float y = -Input.GetAxis("Mouse X");
                //print(y);
                if (y != 0 && (y >= 0.1f || y <= 0.1f))
                {
                    transform.Rotate(Vector3.up * Time.deltaTime * 100f * y);
                }
                float x = Input.GetAxis("Mouse Y");
                //print(x);
                if (x != 0)
                {
                    camera.Rotate(Vector3.right * Time.deltaTime * 100f * x);
                }
            }
            else
            {
                float y = Input.GetAxis("Horizontal");
                //print(y);
                if (y != 0)
                {
                    transform.Rotate(Vector3.up * Time.deltaTime * 10f * y);
                }
                float x = -Input.GetAxis("Vertical");
                //print(x);
                if (x != 0)
                {
                    camera.Rotate(Vector3.right * Time.deltaTime * 10f * x);
                }
            }
        }
        float w = Input.GetAxis("Mouse ScrollWheel");
        if(w != 0)
        {
            FOVOf = Mathf.Clamp(FOVOf - w * 10f, 20, 90);
            if(CameraScaleEvent != null)
            {
                CameraScaleEvent(FOVOf);
            }
        }
    }
	void LateUpdate () {
		
	}
}
