using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlpha : MonoBehaviour {
    [HideInInspector]
    public bool isRayUIEggLaying;
    
    bool isShadingHide;
    bool isShadingDisplay;


    Color color;
    //Color textColor;
    //Image btnImage;
    Text text;

    bool isFirstDisplay;
    bool isFirstHide;
	// Use this for initialization
	void Start () {
        //btnImage = GetComponent<Image>();
        Transform t = transform.Find("Text");
        if (t)
        {
            text = t.GetComponent<Text>();
        }
        color = text.color;
	}
	
	// Update is called once per frame
	void Update () {
        Shading();
	}
    void Shading()
    {
        if(isShadingHide)
        {
            color.a = Mathf.Lerp(color.a, 0f, 0.03f);
            //btnImage.color = btnColor;
            if(text != null) text.color = color;
            if(color.a < 0.1f)
            {
                isShadingHide = false;
                transform.parent.gameObject.SetActive(false);
            }
        }
        if(isShadingDisplay)
        {
            color.a = Mathf.Lerp(color.a, 1f, 0.03f);
            //btnImage.color = btnColor;
            if(text != null) text.color = color;
            if(color.a > 0.95f)
            {
                isShadingDisplay = false;
            }
        }
        if(isRayUIEggLaying)
        {
            isRayUIEggLaying = false;
            transform.localScale = Vector3.one * 1.2f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
    void SetShadingHide()
    {
        if (!isFirstHide)
        {
            isFirstHide = true;
            isShadingDisplay = false;
            isShadingHide = true;
            color.a = 0.95f;
            //btnImage.color = btnColor;
            if (text != null) text.color = color;
        }
    }
    void SetShadingDisplay()
    {
        if (!isFirstDisplay)
        {
            isFirstDisplay = true;
            isShadingHide = false;
            isShadingDisplay = true;
            color.a = 0.05f;
            //btnImage.color = btnColor;
            if (text != null) text.color = color;
        }
    }
}
