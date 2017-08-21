using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject prefab;
    GameObject g;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            g = Instantiate(prefab) as GameObject;
            g.transform.parent = transform;
            g.transform.localPosition = Vector3.zero;
            g.transform.localScale = Vector3.one;
        }
        if(Input.GetMouseButtonDown(1))
        {
            g = Instantiate(prefab) as GameObject;
            g.transform.position = new Vector3(0, 4.18f, 10.68f);
            g.transform.localScale = Vector3.one;
        }
	}
}
