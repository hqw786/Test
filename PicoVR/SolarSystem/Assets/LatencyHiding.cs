using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatencyHiding : MonoBehaviour {
    UIImageEffect uiie;
    UITextEffect uite;

	// Use this for initialization
	void Start () {
        uiie = GetComponent<UIImageEffect>();
        uite = transform.Find("Text").GetComponent<UITextEffect>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Hide()
    {
        Invoke("InvokeHide", 3f);
    }

    void InvokeHide()
    {
        uiie.SetAlphaOneWay(1f, 0f, 1.5f, true);
        uite.SetAlphaOneWay(1f, 0f, 1.5f);
    }
}
