using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour 
{
    List<Button> btnList = new List<Button>();
    List<Image> imgList = new List<Image>();

    Transform stagePanel;
    public bool isRayUI;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        //if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnSelect.name))
        //{
        //    scaleButton(btnSelect);
        //}
        //else if (Pvr_GazeInputModule.gazeGameObject == null)
        //{
        //    if (isRayUI)
        //    {
        //        isRayUI = false;
        //        scaleButton(btnSelect);
        //    }
        //    else
        //    {
        //        resetScaleButton(btnSelect);
        //    }
        //}
    }

        //if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnSelect.name))
        //{
        //    scaleButton(btnSelect);
        //}
        //else if (Pvr_GazeInputModule.gazeGameObject == null)
        //{
        //    if (isRayUISelect)
        //    {
        //        isRayUISelect = false;
        //        scaleButton(btnSelect);
        //    }
        //    else
        //    {
        //        resetScaleButton(btnSelect);
        //    }
        //}
        //if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnExit.name))
        //{
        //    scaleButton(btnExit);
        //}
        //else if (Pvr_GazeInputModule.gazeGameObject == null)
        //{
        //    if (isRayUIExit)
        //    {
        //        isRayUIExit = false;
        //        scaleButton(btnExit);
        //    }
        //    else
        //    {
        //        resetScaleButton(btnExit);
        //    }
        //}
        //if (Pvr_GazeInputModule.gazeGameObject != null && Pvr_GazeInputModule.gazeGameObject.name.Contains(btnReturn.name))
        //{
        //    scaleButton(btnReturn);
        //}
        //else if (Pvr_GazeInputModule.gazeGameObject == null)
        //{
        //    if (isRayUIReturn)
        //    {
        //        isRayUIReturn = false;
        //        scaleButton(btnReturn);
        //    }
        //    else
        //    {
        //        resetScaleButton(btnReturn);
        //    }
        //}

    public void scaleButton(GameObject g)
    {
        g.transform.localScale = Vector3.one * 1.15f;
        g.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
    public void resetScaleButton(GameObject g)
    {
        g.transform.localScale = Vector3.one;
        g.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
    }


    
    // Use this for initialization
    void Start()
    {
        stagePanel = transform.Find("StagePanel");
        //DONE:按照数据有的阶段，生成按钮，并初始化（）
        CreateButton();

        //把所有按钮和进度图片保存起来
        foreach (Transform t in stagePanel)
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
        foreach (var v in ConfigData.Instance.dicStage)
        {
            GameObject g = Resources.Load<GameObject>("Prefabs/BtnStage");
            g = Instantiate(g);
            g.transform.parent = stagePanel;
            g.transform.localScale = Vector3.one;
            Vector3 temp = g.transform.localPosition;
            temp.z = 0f;
            g.transform.localPosition = temp;

            StageButton sb = g.GetComponent<StageButton>();
            sb.SetStageState(v.Key);
        }
    }
    // Update is called once per frame

    public void ButtonResetDefault()
    {
        foreach (Button b in btnList)
        {
            bool temp = b.GetComponent<StageButton>().GetStageInfo().isLock;
            b.transform.Find("Text").GetComponent<Text>().color = temp ? Color.gray : Color.white;
        }
        foreach (Image i in imgList)
        {
            i.fillAmount = 0f;
        }

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
            if (SYSManager.Instance.curStageStatus == StageState.fuhq)
            {
                SYSManager.Instance.fuhuaxiang.parent.GetComponent<FuHuaXiang>().ResetBox();
            }
        }
        if (SYSManager.Instance.curAppStatus == AppState.FeedingAndEgg)
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
