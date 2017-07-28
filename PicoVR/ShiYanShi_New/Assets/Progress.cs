using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour {
    float timer;
    float totalTimer;
    bool isStart;
    Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            timer += Time.deltaTime;
            float f = timer / totalTimer;
            image.fillAmount = f;
            if (f >= 1f)
            {
                isStart = false;
            }
        }
	}
    void SetStart(float time)
    {
        //image.fillAmount = 0f;
        isStart = true;
        totalTimer = time;
        timer = 0f;
    }
}
