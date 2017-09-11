using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeFOV : MonoBehaviour {
    Camera camera;
	// Use this for initialization
	void Start () {
        CameraMove.CameraScaleEvent += OnChangeFOV;
        camera = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnChangeFOV(float fov)
    {
        camera.fieldOfView = fov;
    }
}
