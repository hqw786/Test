using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {
    bool isFadeIn;
    bool isFadeOut;

    Image image;

    float timer;
    float constTimer = 1.5f;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        SetFadeIn();
	}
	
	// Update is called once per frame
	void Update () {
		if(isFadeIn)
        {
            ProcessFadeIn();
        }
        if(isFadeOut)
        {
            ProcessFadeOut();
        }
	}
    public void SetFadeIn()
    {
        isFadeIn = true;
    }
    public void SetFadeOut()
    {
        isFadeOut = true;
    }
    void ProcessFadeOut()
    {
        Color c = image.color;
        timer += Time.deltaTime;
        c.a = Mathf.Lerp(c.a, 1f, timer / constTimer);
        if (c.a >= 0.95f)
        {
            c.a = 1f;
            image.color = c;
            isFadeOut = false;
            timer = 0f;
        }
        image.color = c;
    }
    void ProcessFadeIn()
    {
        Color c = image.color;
        timer += Time.deltaTime;
        c.a = Mathf.Lerp(c.a, 0f, timer / constTimer);
        if (c.a <= 0.05f)
        {
            c.a = 0f;
            image.color = c;
            isFadeIn = false;
            timer = 0f;
            gameObject.SetActive(false);
        }
        image.color = c;
    }
}
