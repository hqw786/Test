using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {
    float y, z;
    float y1, z1;
	// Use this for initialization
	void Start () {
        y = transform.position.y;
        z = transform.position.z;

        float x = transform.GetChild(0).position.x;
        y1 = transform.GetChild(0).position.y;
        z1 = transform.GetChild(0).position.z;

        float f = 0.6f;
        int index = 0;

        foreach(Transform t in transform)
        {
            t.position = new Vector3(x - f * index, y1, z1);
            index++;
        }
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
       float a = Mathf.PingPong(Time.fixedTime * 0.5f, 21f);
        transform.position = new Vector3(a - 4f, y, z);
    }
	void Update () {
        
	}
}
