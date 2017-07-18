using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowPathInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    NodeInfo ni;
    bool isShow;//点击自动漫游后出现自动漫游画面的渐显效果
    bool isImageShow;
    bool isTextShow;
    bool isPointerEnter;//鼠标移入标志点的渐显效果
    bool isImageEnter;
    bool isTextEnter;
    bool isScaleEnter;
    bool isPointerExit;
    bool isImageExit;
    bool isTextExit;


    Image image;
    Text text;
    Color ciOrigin;
    Color ctOrigin;
    // Use this for initialization
    void Awake()
    {

    }
	void Start () {
        image = transform.Find("Image").GetComponent<Image>();
        text = image.transform.Find("Text").GetComponent<Text>();
        ciOrigin = image.color;
        ctOrigin = text.color;
	}
	public void OnEnable()
    {
        if (image != null)
        {
            Color ci = image.color;
            ci.a = 0f;
            image.color = ci;
        }
        if (image != null)
        {
            Color ct = text.color;
            ct.a = 0f;
            text.color = ct;
        }
        isShow = true;
        isImageShow = false;
        isTextShow = false;
    }
	// Update is called once per frame
	void Update () {
		if(isShow)
        {
            if(image != null && text != null)
            {
                #region 
                Color ci = image.color;
                Color ct = text.color;
                ci.a = Mathf.Lerp(ci.a, 1f, 0.05f);
                if (ci.a >= ciOrigin.a)
                {
                    ci.a = ciOrigin.a; 
                    isImageShow = true;
                }
                ct.a = Mathf.Lerp(ct.a, 1f, 0.05f);
                if (ct.a >= ctOrigin.a)
                {
                    ct.a = ctOrigin.a;
                    isTextShow = true;
                }
                if(isImageShow && isTextShow)
                {
                    isShow = false;
                    isImageShow = false;
                    isTextShow = false;
                }
                image.color = ci;
                text.color = ct;
                #endregion
            }
        }
        if(isPointerEnter)
        {
            if(image != null && text != null)
            {
                #region
                Color ci = image.color;
                Color ct = text.color;
                ci.a = Mathf.Lerp(ci.a, 1f, 0.05f);
                if (ci.a >= 0.95f)
                {
                    ci.a = 1f;
                    isImageEnter = true;
                }
                ct.a = Mathf.Lerp(ct.a, 1f, 0.05f);
                if (ct.a >= 0.95f)
                {
                    ct.a = 1f;
                    isTextEnter = true;
                }
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.2f,0.15f);
                if (isImageEnter && isTextEnter)
                {
                    isPointerEnter = false;
                    isImageEnter = false;
                    isTextEnter = false;
                    transform.localScale = Vector3.one * 1.2f;
                }
                image.color = ci;
                text.color = ct;
                #endregion
            }
        }
        if(isPointerExit)
        {
            if(image != null && text != null)
            {
                #region
                Color ci = image.color;
                Color ct = text.color;
                ci.a = Mathf.Lerp(ci.a, 0f, 0.05f);
                if (ci.a <= ciOrigin.a)
                {
                    ci.a = ciOrigin.a;
                    isImageExit = true;
                }
                ct.a = Mathf.Lerp(ct.a, 0f, 0.05f);
                if (ct.a <= ctOrigin.a)
                {
                    ct.a = ctOrigin.a;
                    isTextExit = true;
                    
                }
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.15f);
                if (isImageExit && isTextExit)
                {
                    isPointerExit = false;
                    isImageExit = false;
                    isTextExit = false;
                    ct = new Color(0f, 0f, 0f, ct.a);
                    transform.localScale = Vector3.one;

                }
                image.color = ci;
                text.color = ct;
                #endregion
            }
        }
	}
    void RestoreDefault()
    {
        transform.localScale = Vector3.one;
        image.color = ciOrigin;
        text.color = ctOrigin;
        isShow = false;
        isImageShow = false;
        isTextShow = false;
        isPointerEnter = false;
        isImageEnter = false;
        isTextEnter = false;
        isScaleEnter = false;
        isPointerExit = false;
        isImageExit = false;
        isTextExit = false;
    }
    public void SetPathNodeInfo(NodeInfo ni)
    {
        this.ni = ni;
        Show();
    }
    public void SetPointerEnter()
    {
        if (!isShow)
        {
            isPointerEnter = true;
            isPointerExit = false;
            isImageEnter = false;
            isTextEnter = false;
            if(text != null)
            {
                text.color = new Color(0.03f, 0.54f, 0.9f, text.color.a);
            }
        }
    }
    public void SetPointerExit()
    {
        if (!isShow)
        {
            isPointerEnter = false;
            isPointerExit = true;
            isImageExit = false;
            isTextExit = false;
        }
    }
    void Show()
    {
        if(text == null)
        {
            text = transform.Find("Image/Text").GetComponent<Text>();
        }
        text.text = ni.showContext;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetPointerExit();
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		ShowPathInfo spi = null;
		BtnColorDefault();
		Button b = GetComponent<Button>();
		b.image.color = Color.red;
		//将图标和文字等恢复默认状态
		RestoreDefault();

		spi = GetComponent<ShowPathInfo>();

		if (MainManager.Instance.curView == ViewMode.firstView)
		{
			MainManager.Instance.firstPerson.SetAutoRoamStartAndEndPoint(spi.ni.startNum, spi.ni.endNum);
		}
		else if (MainManager.Instance.curView == ViewMode.flyView)
		{

		}
        if (!UIManager.Instance.IsActive(Define.uiPanelRoamView))
        {
            UIManager.Instance.ShowUI(Define.uiPanelRoamView);
        }
		transform.parent.gameObject.SetActive(false);
	}
    void BtnColorDefault()
    {
        foreach(Transform t in this.transform.parent)
        {
            if(t.name.Contains("BtnRoamFlagImg"))
            {
                t.GetComponent<Button>().image.color = Color.white;
            }
        }
    }
}
