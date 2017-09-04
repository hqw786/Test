using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetButton : MonoBehaviour {
    ShowPlanetView parent;
	// Use this for initialization
	void Start () {
        parent = transform.parent.GetComponent<ShowPlanetView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
