using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagEffect : MonoBehaviour {
    float timer = 1.5f;
    float time;
    Image image;
    bool isTime;
    Color ci;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if(isTime)
        {
            //time += Time.fixedDeltaTime;
            //if (time >= timer)
            //{
            //    isTime = !isTime;
            //    time = 0f;
            //}
            ci = image.color;
            ci.a = Mathf.Lerp(ci.a, 1f, Time.fixedDeltaTime*3f);
            if(ci.a >= 0.95f)
            {
                ci.a = 1f;
                isTime = !isTime;
            }
            image.color = ci;
        }
        else
        {
            //time += Time.fixedDeltaTime;
            //if(time >= timer)
            //{
            //    isTime = !isTime;
            //    time = 0f;
            //}
            ci = image.color;
            ci.a = Mathf.Lerp(ci.a, 0f, Time.fixedDeltaTime);
            if (ci.a <= 0.3f)
            {
                ci.a = 0.3f;
                isTime = !isTime;
            }
            image.color = ci;
        }
    }
	void Update () {
		
	}
}
