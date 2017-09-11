using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapPointInfo : MonoBehaviour , IPointerExitHandler,IPointerEnterHandler,IPointerClickHandler
{
    public string skyboxName;//保存的天空盒名称
    public Sprite curImage;
    public Sprite defaultImage;
    UIImageEffect uiie;
    UIImageEffect uiieTips;
    UITextEffect uiteTips;
    Image tipsImage;
    Text tipsName;
    UIManager uimanager;
    Image image;
    MiniMap miniMap;
	// Use this for initialization

	void Awake () {
        if (!transform.name.Contains("Panel"))
        {
            uiie = GetComponent<UIImageEffect>();
            tipsImage = transform.parent.parent.Find("TipsImage").GetComponent<Image>();
            tipsName = transform.parent.parent.Find("TipsImage/TipsName").GetComponent<Text>();
            uiieTips = tipsImage.transform.GetComponent<UIImageEffect>();
            uiteTips = tipsName.transform.GetComponent<UITextEffect>();
            uimanager = transform.parent.parent.parent.GetComponent<UIManager>();
            image = GetComponent<Image>();
            miniMap = transform.parent.parent.GetComponent<MiniMap>();
            tipsImage.gameObject.SetActive(false);
        }
        else
        {
            uimanager = transform.Find("/Canvas").GetComponent<UIManager>();
        }
	}
	
    void Start()
    {

    }
    public void OnEnable()
    {

    }
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        //切换天空盒
        uimanager.ModifySkybox(skyboxName);
        print("更改天空盒");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!transform.name.Contains("Panel"))
        {
            //提示名称,图标变大
            image.sprite = curImage;
            //uiie.SetScaleOneWay(Vector3.one, Vector3.one * 1.2f, 0.5f);
            tipsName.text = uimanager.GetSpotChineseName(skyboxName);
            uiieTips.transform.localPosition = eventData.pointerEnter.transform.localPosition + new Vector3(0f,  48f, 0f);
            uiieTips.gameObject.SetActive(true);
            uiieTips.SetAlphaOneWay(0f, 1f, 0.2f);
            uiteTips.SetAlphaOneWay(0f, 1f, 0.2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!transform.name.Contains("Panel"))
        {//恢复原样，显示当前景点
            image.sprite = defaultImage;
            //uiie.SetScaleOneWay(Vector3.one * 1.2f, Vector3.one, 0.5f);
            uiieTips.SetAlphaOneWay(1f, 0f, 0.2f, true);
            uiteTips.SetAlphaOneWay(1f, 0f, 0.2f);
            //miniMap.DisplayCurSpotColor(defaultImage);
        }
    }
}
