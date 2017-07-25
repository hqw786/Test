using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInOut : MonoBehaviour {
    bool isFadeOut;
    bool isFadeIn;

    Image image;
    Color color;
	// Use this for initialization
    void Awake()
    {
        image = GetComponent<Image>();
    }
	void Start () {
        image.color = Color.black;
        color = image.color;
        color.a = 0f;
        image.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if(isFadeOut)
        {
            color.a = Mathf.Lerp(color.a, 1f, 0.03f);
            image.color = color;
            if(color.a > 0.9f)
            {
                isFadeOut = false;
                //isFadeIn = true;
            }
        }
        if(isFadeIn)
        {
            color.a = Mathf.Lerp(color.a, 0f, 0.03f);
            image.color = color;
            if(color.a < 0.1f)
            {
                isFadeIn = false;
                color.a = 0f;
                image.color = color;
                //this.gameObject.SetActive(false);
            }
        }
	}
    public void StartFadeOut()
    {
        isFadeOut = true;
    }
    public void StartFadeIn()
    {
        isFadeIn = true;
        isFadeOut = false;
    }
}
