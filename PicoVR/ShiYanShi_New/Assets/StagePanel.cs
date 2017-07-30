using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StagePanel : MonoBehaviour
{
    List<Button> btnList = new List<Button>();
    List<Image> imgList = new List<Image>();
	// Use this for initialization
	void Start () 
    {
        //DONE:按照数据有的阶段，生成按钮，并初始化（）
        CreateButton();

        //把所有按钮和进度图片保存起来
        foreach(Transform t in transform)
        {
            btnList.Add(t.GetComponent<Button>());
            if (!t.name.Contains("BtnWeiSQ") && !t.name.Contains("BtnEgg"))
            {
                imgList.Add(t.Find("Image").GetComponent<Image>());
            }
        }
    }

    void CreateButton()
    {
        foreach(var v in ConfigData.Instance.dicStage)
        {
            GameObject g = Resources.Load<GameObject>("Prefabs/BtnStage");
            g = Instantiate(g);
            g.transform.parent = transform;
            g.transform.localScale = Vector3.one;
            Vector3 temp = g.transform.localPosition;
            temp.z = 0f;
            g.transform.localPosition = temp;

            StageButton sb = g.GetComponent<StageButton>();
            sb.SetStageState(v.Key);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
    //public void OnBtnFuHQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnFuHQ/Text").GetComponent<Text>().color = Color.red;
    //    SYSManager.Instance.StageStatusSwitch(StageState.fuhq);
    //    SYSManager.Instance.StartShowFlow(StageState.fuhq);
    //}
    //public void OnBtnPoKQClick()
    //{
    //    if (!ConfigData.Instance.dicStage[StageState.chujq].GetData().isLock)
    //    {
    //        ButtonResetDefault();
    //        transform.Find("BtnPoKQ/Text").GetComponent<Text>().color = Color.red;
    //        SYSManager.Instance.StageStatusSwitch(StageState.pokq);
    //        SYSManager.Instance.StartShowFlow(StageState.pokq);
    //    }
    //}
    //public void OnBtnMiaoJQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnMiaoJQ/Text").GetComponent<Text>().color = Color.red;
    //}
    //public void OnBtnChuJQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnChuJQ/Text").GetComponent<Text>().color = Color.red;
    //}
    //public void OnBtnQinNQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnQinNQ/Text").GetComponent<Text>().color = Color.red;
    //}
    //public void OnBtnChengNQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnChengNQ/Text").GetComponent<Text>().color = Color.red;
    //}
    //public void OnBtnChanDQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnChanDQ/Text").GetComponent<Text>().color = Color.red;
    //}
    //public void OnBtnWeiSQClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnWeiSQ/Text").GetComponent<Text>().color = Color.red;
    //}
    //public void OnBtnEggClick()
    //{
    //    ButtonResetDefault();
    //    transform.Find("BtnEgg/Text").GetComponent<Text>().color = Color.red;
    //}

    public void ButtonResetDefault()
    {
        foreach(Button b in btnList)
        {
            bool temp = b.GetComponent<StageButton>().GetStageInfo().isLock;
            b.transform.Find("Text").GetComponent<Text>().color = temp ? Color.gray : Color.white;
        }
        foreach(Image i in imgList)
        {
            i.fillAmount = 0f;
        }
        foreach(StageInfo si in ConfigData.Instance.Data)
        {
            if(si.StartModel != null)
            {
                si.StartModel.SetActive(false);
            }
            if (si.MainModel != null)
            {
                si.MainModel.SetActive(false);
            }
            if (si.EndModel != null)
            {
                si.EndModel.SetActive(false);
            }
        }
    }
}
