using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowText : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler{
    public Transform point;

    Image image;
	Text text;

    bool isShow;
    bool isHide;
    bool isScaleBig;
    bool isScaleSmall;

	// Use this for initialization
    void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
		text = image.transform.Find("Text").GetComponent<Text>();
    }
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnBtnPointClick);
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
        transform.localScale = Vector3.one;
    }
	// Update is called once per frame
	void Update () {
		if(isShow)
		{
			Color ci = image.color;
			Color ct = text.color;
			ci.a = Mathf.Lerp(ci.a, 1f, 0.1f);
			ct.a = Mathf.Lerp(ct.a, 1f, 0.1f);
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
			ci.a = Mathf.Lerp(ci.a, 0f, 0.15f);
			ct.a = Mathf.Lerp(ct.a, 0f, 0.15f);
			if (ci.a <= 0.05f)
			{
				ci.a = 0f;
				ct.a = 0f;
				image.color = ci;
				text.color = ct;
				isHide = false;
				image.gameObject.SetActive(false);
                //print("Image隐藏");
				text.gameObject.SetActive(false);
			}
			image.color = ci;
			text.color = ct;
		}
        if(isScaleBig)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 5f);
            if(transform.localScale.x >= 1.15f)
            {
                transform.localScale = Vector3.one * 1.2f;
                isScaleBig = false;
                isScaleSmall = true;
            }
            //DONE:如果要提示内容不缩放则启用下面的代码
            image.transform.localScale = Vector3.one;
            text.transform.localScale = Vector3.one;
        }
        if(isScaleSmall)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 10f);
            if(transform.localScale.x < 1.05f)
            {
                transform.localScale = Vector3.one;
                isScaleSmall = false;
                //Button b = GetComponent<Button>();
                //b.image.sprite = b.spriteState.disabledSprite;
            }
            //DONE:如果要提示内容不缩放则启用下面的代码
            image.transform.localScale = Vector3.one;
            text.transform.localScale = Vector3.one;
        }
	}
    public void FadeShow()
    {
        isShow = true;
        isHide = false;
        
		image.gameObject.SetActive(true);
		text.gameObject.SetActive(true);

        Button b = GetComponent<Button>();
        b.image.sprite = b.spriteState.pressedSprite;
    }
    public void FadeHide()
    {
        isShow = false;
        isHide = true;

        Button b = GetComponent<Button>();
        b.image.sprite = b.spriteState.disabledSprite;
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
    public void SetContext(string s)
    {
        string ss = s.Substring(s.IndexOf(".") + 1);
        text.text = ss;
    }
    public void OnBtnPointClick()
    {
        MainManager.Instance.CloseAutoRoam();
        MainManager.Instance.WarpToNewPosition(point);
        isScaleBig = true;
        //其他按钮恢复默认
        //transform.parent.GetComponent<NewPositionPanel>().ButtonsDefault();

        //点击的按钮变成红色
        //btnPoint1.image.sprite = btnPoint1.spriteState.pressedSprite;
        //GetComponent<Button>().image.color = Color.red;

        //DONE:这个隐不隐藏，按需求来
        //transform.parent.gameObject.SetActive(false);
    }
}
