using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StagePanel : MonoBehaviour , IPointerClickHandler
{
    List<Button> btnList = new List<Button>();
    List<Image> imgList = new List<Image>();
	// Use this for initialization
	void Start () {
        foreach(Transform t in transform)
        {
            btnList.Add(t.GetComponent<Button>());
            if (!t.name.Contains("BtnWeiSQ") && !t.name.Contains("BtnEgg"))
            {
                imgList.Add(t.Find("Image").GetComponent<Image>());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnBtnFuHQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnFuHQ/Text").GetComponent<Text>().color = Color.red;
        SYSManager.Instance.StageStatusSwitch(StageState.fuhq);
        SYSManager.Instance.StartShowFlow();
    }
    public void OnBtnPoKQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnPoKQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnMiaoJQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnMiaoJQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnChuJQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnChuJQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnQinNQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnQinNQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnChengNQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnChengNQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnChanDQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnChanDQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnWeiSQClick()
    {
        ButtonResetDefault();
        transform.Find("BtnWeiSQ/Text").GetComponent<Text>().color = Color.red;
    }
    public void OnBtnEggClick()
    {
        ButtonResetDefault();
        transform.Find("BtnEgg/Text").GetComponent<Text>().color = Color.red;
    }

    public void ButtonResetDefault()
    {
        foreach(Button b in btnList)
        {
            b.transform.Find("Text").GetComponent<Text>().color = Color.white;
        }
        foreach(Image i in imgList)
        {
            i.fillAmount = 0f;
        }
    }
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    ButtonResetDefault();
    //    eventData.pointerEnter.transform.Find("Text").GetComponent<Text>().color = Color.red;
    //}
}
