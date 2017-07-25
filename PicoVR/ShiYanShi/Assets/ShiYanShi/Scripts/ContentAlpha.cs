using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentAlpha : MonoBehaviour {
    Text text;
    Color color;
    public bool isHide;
    public bool isDisplay;

    bool isShadingHide;
    bool isShadingDisplay;
	// Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
        color = text.color;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SYSManager.Instance.isContentAlphaHide)
        {
			if(isHide)
			{
				isHide = false;
				color.a = 0.9f;
			}
	        isDisplay = true;
            color.a = Mathf.Lerp(color.a, 0f, 0.03f);
            text.color = color;
            if(text.color.a < 0.1f)
            {
                SYSManager.Instance.isContentAlphaHide = false;
            }
        }
		if (SYSManager.Instance.isContentAlphaDisplay)
        {
			if (isDisplay)
			{
				isDisplay = false;
				color.a = 0.1f;
			}
	        isHide = true;
            color.a = Mathf.Lerp(color.a, 1f, 0.03f);
            text.color = color;
            if(text.color.a > 0.9f)
            {
                SYSManager.Instance.isContentAlphaDisplay = false;
            }
        }

        Shading();
	}

    void Shading()
    {
        if(isShadingHide)
        {
            color.a = Mathf.Lerp(color.a, 0f, 0.03f);
            text.color = color;
            if (text.color.a < 0.1f)
            {
                isShadingHide = false;
            }
        }
        if(isShadingDisplay)
        {
            color.a = Mathf.Lerp(color.a, 1f, 0.03f);
            text.color = color;
            if (text.color.a > 0.9f)
            {
                isShadingDisplay = false;
            }
        }
    }

    void SetHide()
    {
        isHide = true;
		color.a = 1f;
		text.color = color;
    }
    void SetDisplay()
    {
        isDisplay = true;
	    color.a = 0f;
	    text.color = color;
    }
    void SetTextColor(Color c)
    {
        text.color = c;
    }
    void SetShadingHide()
    {
        isShadingHide = true;
        isShadingDisplay = false;
        color.a = 0.9f;
        text.color = color;
    }
    void SetShadingDisplay()
    {
        isShadingDisplay = true;
        isShadingHide = false;
        color.a = 0.1f;
        text.color = color;
    }
}
