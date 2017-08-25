using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    float x;
    float z;
    public float y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(z, y, -x);
        transform.position = transform.position + dir;
	}
}
