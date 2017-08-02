using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsTextEffect : MonoBehaviour {
    Text text;
    bool isTime;
    Color ct;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isTime)
        {
            ct = text.color;
            ct.a = Mathf.Lerp(ct.a, 1f, Time.fixedDeltaTime);
            if (ct.a >= 0.95f)
            {
                ct.a = 1f;
                isTime = !isTime;
            }
            text.color = ct;
        }
        else
        {
            ct = text.color;
            ct.a = Mathf.Lerp(ct.a, 0f, Time.fixedDeltaTime);
            if (ct.a <= 0.1f)
            {
                ct.a = 0.1f;
                isTime = !isTime;
            }
            text.color = ct;
        }
	}
}
