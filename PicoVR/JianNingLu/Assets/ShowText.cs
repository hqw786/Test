using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowText : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler{
    

    Image image;
	Text text;

    bool isShow;
    bool isHide;

	// Use this for initialization
    void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
		text = image.transform.Find("Text").GetComponent<Text>();
    }
	void Start () {
        
	}
    public void OnEnable()
    {
        //透明，隐藏
        TextDefault();
    }
    void TextDefault()
    {
        Color ci = image.color;
        Color ct = text.color;
        ci.a = 0f;
        ct.a = 0f;
        image.color = ci;
        text.color = ct;
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
		if(isShow)
		{
			Color ci = image.color;
			Color ct = text.color;
			ci.a = Mathf.Lerp(ci.a, 1f, 0.05f);
			ct.a = Mathf.Lerp(ct.a, 1f, 0.05f);
			if(ci.a >= 0.95f)
			{
				ci.a = 1f;
				ct.a = 1f;
				isShow = false;
			}
			image.color = ci;
			text.color = ct;
		}
		if(isHide)
		{
			Color ci = image.color;
			Color ct = text.color;
			ci.a = Mathf.Lerp(ci.a, 0f, 0.05f);
			ct.a = Mathf.Lerp(ct.a, 0f, 0.05f);
			if (ci.a <= 0.05f)
			{
				ci.a = 0f;
				ct.a = 0f;
				image.color = ci;
				text.color = ct;
				isHide = false;
				image.gameObject.SetActive(false);
				text.gameObject.SetActive(false);
			}
			image.color = ci;
			text.color = ct;
		}
	}
    public void FadeShow()
    {
        isShow = true;
        isHide = false;
		image.gameObject.SetActive(true);
		text.gameObject.SetActive(true);
    }
    public void FadeHide()
    {
        isShow = false;
        isHide = true;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
		FadeShow();
		//print("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		FadeHide();
		//print("exit");
    }
}
