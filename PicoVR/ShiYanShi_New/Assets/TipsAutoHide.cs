using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsAutoHide : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        Invoke("Hide", 3f);
	}
	
	// Update is called once per frame
	void Update () {
	}
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
