using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float a = Mathf.PingPong(Time.time, 3);
        transform.position = new Vector3(a, 0f, 0f);
	}
}
