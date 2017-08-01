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
        //foreach(StageInfo si in ConfigData.Instance.Data)
        //{
        //    if(si.StartModel != null)
        //    {
        //        si.StartModel.SetActive(false);
        //    }
        //    if (si.MainModel != null)
        //    {
        //        si.MainModel.SetActive(false);
        //    }
        //    if (si.EndModel != null)
        //    {
        //        si.EndModel.SetActive(false);
        //    }
        //}
        

        if (SYSManager.Instance.curAppStatus == AppState.Show)
        {
            if (ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().StartModel != null)
            {
                ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().StartModel.SetActive(false);
            }
            if (ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().MainModel != null)
            {
                ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().MainModel.SetActive(false);
            }
            if (ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().EndModel != null)
            {
                ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().EndModel.SetActive(false);
            }
            if(SYSManager.Instance.curStageStatus == StageState.fuhq)
            {
                SYSManager.Instance.fuhuaxiang.parent.GetComponent<FuHuaXiang>().ResetBox();
            }
        }
        if(SYSManager.Instance.curAppStatus == AppState.FeedingAndEgg)
        {
            if (SYSManager.Instance.curStageStatus == StageState.weisq)
            {
                foreach (var v in ConfigData.Instance.dicFodder[StageState.weisq])
                {
                    v.SetActive(false);
                }
            }
            if (SYSManager.Instance.curStageStatus == StageState.egg)
            {
                foreach (var v in ConfigData.Instance.dicEgg[StageState.egg])
                {
                    v.SetActive(false);
                }
            }
        }
    }
}
